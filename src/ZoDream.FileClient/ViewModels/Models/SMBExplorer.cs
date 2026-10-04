using SMBLibrary;
using SMBLibrary.Client;
using SMBLibrary.SMB1;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Models;

namespace ZoDream.FileClient.ViewModels
{
    public class SMBExplorer(IEntryService service) : IEntryExplorer
    {
        private ISMBClient? _client;
        public ISourceEntry HomeEntry { get; private set; } = DirectoryEntry.Empty;

        public bool TryGetPrevious(ISourceEntry entry, out ISourceEntry parent)
        {
            if (!StorageExplorer.IsSubPathOf(HomeEntry.FullPath, entry.FullPath))
            {
                parent = entry;
                return false;
            }
            parent = new DirectoryEntry(Path.GetDirectoryName(entry.FullPath) ?? HomeEntry.FullPath);
            return true;
        }
        public Task<bool> ConnectAsync(IConnectOptions option, CancellationToken token = default)
        {
            _client?.Disconnect();
            HomeEntry = new DirectoryEntry(option.RemotePath);
            _client = option.RemoteProtocol switch
            {
                ProtocolType.SMBv1 => new SMB1Client(),
                _ => new SMB2Client(),
            };
            bool isConnected;
            if (IPAddress.TryParse(option.RemoteHost, out var ip))
            {
                isConnected = _client.Connect(ip, SMBTransportType.DirectTCPTransport);
            } else
            {
                isConnected = _client.Connect(option.RemoteHost, SMBTransportType.DirectTCPTransport);
            }
            if (isConnected)
            {
                var status = _client.Login(string.Empty, option.RemoteUser, option.RemotePassword);
                if (status == NTStatus.STATUS_SUCCESS)
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public ISourceEntry Convert(IConnectEntrance entrance)
        {
            return new DirectoryEntry(entrance.RemotePath);
        }

        public Task<IEntryStream> OpenAsync(ISourceEntry entry, CancellationToken token = default)
        {
            if (_client is null)
            {
                return Task.FromResult<IEntryStream>(UnknownEntryStream.Instance);
            }
            return Task.FromResult<IEntryStream>(new DirectoryEntryStream(
                GetList(entry.FullPath, token)));
        }


        private ISourceEntry[] GetList(string fullPath, CancellationToken token)
        {
            if (_client is null)
            {
                return [];
            }
            NTStatus status;
            if (string.IsNullOrEmpty(fullPath))
            {
                var items = _client.ListShares(out status);
                if (status != NTStatus.STATUS_SUCCESS)
                {
                    return [];
                }
                return [..items.Select(i => new DirectoryEntry(i))];
            }
            var args = fullPath.Split("://", 2); // drive
            var fileStore = _client.TreeConnect(args[0], out status);
            if (status != NTStatus.STATUS_SUCCESS)
            {
                return [];
            }
            var isV1 = _client is SMB1Client;
            var folder = string.IsNullOrEmpty(args[1]) ? 
                (isV1 ? "\\" : string.Empty) : args[1];
            status = fileStore.CreateFile(out var directoryHandle, 
                out var fileStatus,
                folder, 
                AccessMask.GENERIC_READ, 
                SMBLibrary.FileAttributes.Directory, 
                ShareAccess.Read | ShareAccess.Write, 
                CreateDisposition.FILE_OPEN, 
                CreateOptions.FILE_DIRECTORY_FILE, null);
            
            if (status != NTStatus.STATUS_SUCCESS)
            {
                return [];
            }
            ISourceEntry[] res;
            if (isV1)
            {
                status = ((SMB1FileStore)fileStore).QueryDirectory(out var fileList2, folder + "*", FindInformationLevel.SMB_FIND_FILE_DIRECTORY_INFO);
                if (status != NTStatus.STATUS_SUCCESS)
                {
                    return [];
                }
                res = fileList2.Select<FindInformation, ISourceEntry>(i => {
                    return i switch
                    {
                        FindFileDirectoryInfo d => new DirectoryEntry(d.FileName, d.CreationTime),
                        FindFileNamesInfo f => new FileEntry(f.FileName, 0, false, null),
                        _ => throw new NotSupportedException()
                    };
                }).ToArray();
            }
            else
            {
                status = fileStore.QueryDirectory(out var fileList, directoryHandle, "*", FileInformationClass.FileDirectoryInformation);
                if (status != NTStatus.STATUS_SUCCESS)
                {
                    return [];
                }
                res = fileList.Select<QueryDirectoryFileInformation, ISourceEntry>(i => {
                    return i switch
                    {
                        FileDirectoryInformation d => new DirectoryEntry(d.FileName, d.CreationTime),
                        FileNamesInformation f => new FileEntry(f.FileName, f.Length, false, null),
                        _ => throw new NotSupportedException()
                    };
                }).ToArray();
            }
            status = fileStore.CloseFile(directoryHandle);
            return res;
        }

        public void Dispose()
        {
            _client?.Disconnect();
        }
    }
}
