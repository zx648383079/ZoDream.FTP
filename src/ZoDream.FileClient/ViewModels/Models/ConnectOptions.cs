using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Models;

namespace ZoDream.FileClient.ViewModels
{
    public class ConnectOptions : IConnectOptions
    {
        public string LocalPath { get; set; } = string.Empty;
        public string RemotePath { get; set; } = string.Empty;
        public string RemoteHost { get; set; } = string.Empty;
        public int RemotePort { get; set; } = 21;
        public AccessType RemoteAccess { get; set; }
        public ProtocolType RemoteProtocol { get; set; }
        public string RemoteUser { get; set; } = string.Empty;
        public string RemotePassword { get; set; } = string.Empty;
        /// <summary>
        /// 开启文件同步功能
        /// </summary>
        public bool EnabledSync { get; set; }
        /// <summary>
        /// 开启文件比较功能
        /// </summary>
        public bool EnabledDiff { get; set; }
        /// <summary>
        /// FTP 的主动被动模式
        /// </summary>
        public bool EnabledPassive { get; set; }
        /// <summary>
        /// 文件编码方式
        /// </summary>
        public string RemoteEncoding { get; set; } = "UTF8";
        /// <summary>
        /// 并非数
        /// </summary>
        public int Concurrency { get; set; }
    }
}
