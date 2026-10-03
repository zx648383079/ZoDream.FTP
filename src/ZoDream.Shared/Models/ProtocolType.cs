namespace ZoDream.Shared.Models
{
    public enum ProtocolType : byte
    {
        FTP,
        SFTP,
        STORJ,
        SMBv1,
        SMBv2,
        SMBv3,
        WebDAV,
        AFP,
        NFS,

        Socket = 120,
        Local = 127
    }
}
