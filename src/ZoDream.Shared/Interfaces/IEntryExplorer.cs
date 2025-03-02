using System;
using System.Threading;
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

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="option"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<bool> ConnectAsync(IConnectOption option, CancellationToken token = default);
        
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="entrance"></param>
        /// <returns></returns>
        public ISourceEntry Convert(IConnectEntrance entrance);

        public Task<IEntryStream> OpenAsync(ISourceEntry entry, CancellationToken token = default);
    }
}
