using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class SocketExplorer(IEntryService service) : IEntryExplorer
    {
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
            HomeEntry = new DirectoryEntry(option.RemotePath);

            return Task.FromResult(true);
        }

        public ISourceEntry Convert(IConnectEntrance entrance)
        {
            return new DirectoryEntry(entrance.RemotePath);
        }

        public Task<IEntryStream> OpenAsync(ISourceEntry entry, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

   

        public void Dispose()
        {
        }
    }
}
