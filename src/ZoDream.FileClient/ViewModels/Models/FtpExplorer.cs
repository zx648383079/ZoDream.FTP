using FluentFTP;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class FtpExplorer(IEntryService service) : IEntryExplorer
    {

        private AsyncFtpClient? _client;
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
        public async Task<bool> ConnectAsync(IConnectOptions option, CancellationToken token = default)
        {
            _client?.Dispose();
            HomeEntry = new DirectoryEntry(option.RemotePath);
            _client = new AsyncFtpClient(option.RemoteHost, option.RemotePort, null, new FtpLogger())
            {
                Credentials = new System.Net.NetworkCredential(option.RemoteUser, option.RemotePassword)
            };
            var profile = await _client.AutoConnect(token);
            if (profile is null)
            {
                return false;
            }
            return true;
        }

        public ISourceEntry Convert(IConnectEntrance entrance)
        {
            return new DirectoryEntry(entrance.RemotePath);
        }

        public async Task<IEntryStream> OpenAsync(ISourceEntry entry, CancellationToken token = default)
        {
            if (entry.IsDirectory)
            {
                return new DirectoryEntryStream(await GetListAsync(entry.FullPath, token));
            }
            return UnknownEntryStream.Instance;
        }

        private async Task<ISourceEntry[]> GetListAsync(string fullPath, CancellationToken token)
        {
            if (_client is null)
            {
                return [];
            }
            var res = new List<ISourceEntry>();
            foreach (var item in await _client.GetListing(fullPath, FtpListOption.Recursive, token))
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                if (item.Type == FtpObjectType.File)
                {
                    res.Add(new FileEntry(item.FullName, item.Size, false, item.Modified));
                }
                if (item.Type == FtpObjectType.Directory)
                {
                    res.Add(new DirectoryEntry(item.FullName, item.Modified));
                }
            }
            return [.. res];
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
