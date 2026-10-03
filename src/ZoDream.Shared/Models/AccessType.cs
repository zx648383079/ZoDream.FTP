namespace ZoDream.Shared.Models
{
    public enum AccessType : byte
    {
        Guest,
        Account,
        Token,
        Key,
        Ask = 127
    }
}
