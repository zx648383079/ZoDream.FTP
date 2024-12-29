using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class UnknownEntryStream: IEntryStream
    {
        public static readonly UnknownEntryStream Instance = new();
    }
}
