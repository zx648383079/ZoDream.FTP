using System.Linq;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class DirectoryEntryStream(params ISourceEntry[] items) : IEntryStream
    {

        public ISourceEntry[] Items { get; private set; } = items;

        public bool CanGoBack => Items.Where(i => i is TopDirectoryEntry).Any();

    }
}
