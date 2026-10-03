using System;

namespace ZoDream.FileClient.ViewModels
{
    [Flags]
    public enum EntryCompareFlags: byte
    {
        None = 0,
        Name = 0b1,
        Size = 0b10,
        Time = 0b100,
        MD5 = 0b1000,
        CRC32 = 0b10000,
    }
    [Flags]
    public enum EntryCompareStatus : byte
    {
        None,
        Compared = 0b1,
        DiffName = 0b10,
        DiffTime = 0b100,
        DiffContent = 0b1000,
    }
}
