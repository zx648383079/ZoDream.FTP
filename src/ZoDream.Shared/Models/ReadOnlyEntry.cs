﻿using System;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.Models
{
    public class ReadOnlyEntry : IReadOnlyEntry
    {
        public ReadOnlyEntry(string name)
        {
            Name = name;
        }

        public ReadOnlyEntry(string name, long length)
            :this (name)
        {
            Length = length;
        }

        public ReadOnlyEntry(
            string name, long length, DateTime? createdTime): this(name, length)
        {
            CreatedTime = createdTime;
        }

        public ReadOnlyEntry(
            string name, long length, bool isEncrypted, DateTime? createdTime)
            : this(name, length, createdTime)
        {
            IsEncrypted = isEncrypted;
        }

        public string Name { get; private set; }

        public long Length { get; private set; }

        public bool IsEncrypted { get; private set; }

        public DateTime? CreatedTime { get; private set; }
    }
}
