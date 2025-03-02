using System.Collections.Generic;

namespace ZoDream.Shared.Interfaces
{
    public interface IConnectRecord : IConnectOption
    {
        public string Name { get; }

        public IList<IConnectEntrance> Items { get; } 
    }

    public interface IConnectOption
    {
        public string Host { get; }

        public int Port { get; }

        public string Account { get; }

        public string Password { get; }

        public string Protocol { get; }
        public string Method { get; }
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
