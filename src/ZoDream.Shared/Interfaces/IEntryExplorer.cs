using System;
using System.Threading.Tasks;

namespace ZoDream.Shared.Interfaces
{
    public interface ISourceEntry : IReadOnlyEntry
    {
        public bool IsDirectory { get; }

        public string FullPath { get; }
    }

    public interface IEntryStream
    {

    }

    

    public interface IEntryExplorer : IDisposable
    {

        public bool CanGoBack { get; }

        public Task<IEntryStream> OpenAsync(ISourceEntry entry);
    }
}
