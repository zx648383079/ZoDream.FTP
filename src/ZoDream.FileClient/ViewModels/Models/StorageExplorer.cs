using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class StorageExplorer(IEntryService service, bool isRemote = false): IEntryExplorer
    {

        public List<ISourceEntry> Items { get; private set; } = [];
        public ISourceEntry HomeEntry { get; private set; } = DirectoryEntry.Empty;

        public bool TryGetPrevious(ISourceEntry entry, out ISourceEntry parent)
        {
            if (!IsSubPathOf(HomeEntry.FullPath, entry.FullPath))
            {
                parent = entry;
                return false;
            }
            parent = new DirectoryEntry(Path.GetDirectoryName(entry.FullPath) ?? HomeEntry.FullPath);
            return true;
        }

        public Task<bool> ConnectAsync(IConnectOptions option, CancellationToken token = default)
        {
            HomeEntry = new DirectoryEntry(isRemote ? option.RemotePath : option.LocalPath);
            return Task.FromResult(true);
        }

        public ISourceEntry Convert(IConnectEntrance entrance)
        {
            if (File.Exists(entrance.LocalPath))
            {
                return new FileEntry(entrance.LocalPath);
            }
            return new DirectoryEntry(entrance.LocalPath);
        }

        public bool Contains(string fileName)
        {
            return Items.FindIndex(i => i.FullPath == fileName) >= 0;
        }

        public void Add(IEnumerable<string> fileNames)
        {
            foreach (var item in fileNames)
            {
                Add(item);
            }
        }

        public void Add(IEnumerable<ISourceEntry> fileNames)
        {
            foreach (var item in fileNames)
            {
                Add(item.FullPath);
            }
        }

        public void Add(ISourceEntry entry)
        {
            Add(entry.FullPath);
        }

        public void Add(string fileName)
        {
            var fullPath = Path.GetFullPath(fileName);
            if (Contains(fullPath))
            {
                return;
            }
            if (Directory.Exists(fullPath))
            {
                Items.Add(new DirectoryEntry(fullPath));
            } else if (File.Exists(fullPath))
            {
                Items.Add(new FileEntry(fullPath));
            }
        }

        public void Remove(string fileName)
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                if (Items[i].FullPath == fileName)
                {
                    Items.RemoveAt(i);
                }
            }
        }

        public async Task<IEntryStream> OpenAsync(ISourceEntry entry, CancellationToken token = default)
        {
            if (entry.IsDirectory)
            {
                return OpenDirectory(entry.FullPath);
            }
            return UnknownEntryStream.Instance;
        }

        private DirectoryEntryStream OpenDirectory(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return new([.. Items]);
            }
            var info = new DirectoryInfo(fileName);
            var top = Contains(fileName) ?
                new TopDirectoryEntry(string.Empty) : 
                new TopDirectoryEntry(info.Parent!.FullName);
            return new([top, 
                .. info.GetDirectories().Select(i => new DirectoryEntry(i)).ToArray(),
                .. info.GetFiles().Select(i => new FileEntry(i)).ToArray()]);
        }

        public void Dispose()
        {
            Items.Clear();
        }

        public static bool IsSubPathOf(string parentPath, string childPath)
        {
            if (string.IsNullOrWhiteSpace(parentPath) || string.IsNullOrWhiteSpace(childPath))
            {
                return false;
            }

            var parentFull = Path.TrimEndingDirectorySeparator(Path.GetFullPath(parentPath));
            var childFull = Path.TrimEndingDirectorySeparator(Path.GetFullPath(childPath));

            var comparison = OperatingSystem.IsWindows()
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;

            if (childFull.Equals(parentFull, comparison))
            {
                return false;
            }

            var parentWithSep = parentFull + Path.DirectorySeparatorChar;
            return childFull.StartsWith(parentWithSep, comparison);
        }
    }
}
