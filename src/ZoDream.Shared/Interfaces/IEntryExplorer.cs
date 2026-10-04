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
        /// 获取根目录
        /// </summary>
        public ISourceEntry HomeEntry { get; }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="option"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<bool> ConnectAsync(IConnectOptions option, CancellationToken token = default);
        
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="entrance"></param>
        /// <returns></returns>
        public ISourceEntry Convert(IConnectEntrance entrance);
        /// <summary>
        /// 根据路径获取上一级
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool TryGetPrevious(ISourceEntry entry, out ISourceEntry parent);

        public Task<IEntryStream> OpenAsync(ISourceEntry entry, CancellationToken token = default);
        
    }
}
