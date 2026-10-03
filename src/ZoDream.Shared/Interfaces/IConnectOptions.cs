using System.Collections.Generic;
using ZoDream.Shared.Models;

namespace ZoDream.Shared.Interfaces
{
    public interface IConnectRecord : IConnectOptions
    {
        public string Name { get; }

        public IList<IConnectEntrance> Items { get; } 
    }

    public interface IConnectOptions
    {
        public string LocalPath { get; }
        public string RemotePath { get; }
        public string RemoteHost { get; }
        public int RemotePort { get; }
        public AccessType RemoteAccess { get; }
        public ProtocolType RemoteProtocol { get; }
        public string RemoteUser { get; }
        public string RemotePassword { get; }
        /// <summary>
        /// 开启文件同步功能
        /// </summary>
        public bool EnabledSync { get; }
        /// <summary>
        /// 开启文件比较功能
        /// </summary>
        public bool EnabledDiff { get; }
        /// <summary>
        /// FTP 的主动被动模式
        /// </summary>
        public bool EnabledPassive { get; }
        /// <summary>
        /// 文件编码方式
        /// </summary>
        public string RemoteEncoding { get; }
        /// <summary>
        /// 并非数
        /// </summary>
        public int Concurrency { get; }
    }

    public interface IConnectEntrance
    {
        public string Name { get; }

        public string LocalPath { get; }
        public string RemotePath { get; }

        public bool OpenSync { get; }

        public bool OpenDiff { get; }
    }
}
