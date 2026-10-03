using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ZoDream.Shared.Interfaces;

namespace ZoDream.FileClient.ViewModels
{
    public class EntryEqualityComparer(EntryCompareFlags flag) : IEqualityComparer<ISourceEntry>
    {
        public bool Equals(ISourceEntry? x, ISourceEntry? y)
        {
            if (x?.IsDirectory == y?.IsDirectory)
            {
                return false;
            }
            if (flag.HasFlag(EntryCompareFlags.Name) && x?.Name != y?.Name)
            {
                return false;
            }
            if (flag.HasFlag(EntryCompareFlags.Size) && x?.Length != y?.Length)
            {
                return false;
            }
            if (flag.HasFlag(EntryCompareFlags.Time) && x?.CreatedTime != y?.CreatedTime)
            {
                return false;
            }
            return true;
        }

        public int GetHashCode([DisallowNull] ISourceEntry obj)
        {
            var hash = new HashCode();
            hash.Add(obj.IsDirectory);
            if (flag.HasFlag(EntryCompareFlags.Name))
            {
                hash.Add(obj.Name);
            }
            if (flag.HasFlag(EntryCompareFlags.Size))
            {
                hash.Add(obj.Length);
            }
            if (flag.HasFlag(EntryCompareFlags.Time))
            {
                hash.Add(obj.CreatedTime);
            }
            return hash.ToHashCode();
        }
    }
}
