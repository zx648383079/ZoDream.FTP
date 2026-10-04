using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebDav;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class WebDAVExplorer(IEntryService service) : IEntryExplorer
    {
        private IWebDavClient? _client;
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
            _client?.Dispose();
            HomeEntry = new DirectoryEntry(option.RemotePath);
            _client = new WebDavClient(new WebDavClientParams { 
                BaseAddress = new Uri(option.RemoteHost),
                Credentials = new NetworkCredential(option.RemoteUser, option.RemotePassword)
            });
            return Task.FromResult(true);
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
            var result = await _client.Propfind(fullPath, new PropfindParameters()
            {
                CancellationToken = token
            });
            if (!result.IsSuccessful)
            {
                return [];
            }
            var res = new List<ISourceEntry>();
            foreach (var item in result.Resources)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                if (!item.IsCollection)
                {
                    res.Add(new FileEntry(item.DisplayName ?? "[-]", item.ContentLength ?? 0, false, item.LastModifiedDate ?? item.CreationDate));
                } else
                {
                    res.Add(new DirectoryEntry(item.DisplayName ?? "[-]", item.LastModifiedDate ?? item.CreationDate));
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
