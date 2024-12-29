﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.IO;

namespace ZoDream.FileClient.ViewModels
{
    public class EntryService : IEntryService, IDisposable
    {
        public EntryService(
            ILogger logger,
            ITemporaryStorage storage)
        {
            
        }
        public EntryService(ILogger logger)
            : this(logger, new TemporaryStorage())
        {
            
        }

        private readonly ConcurrentDictionary<string, object> _instanceItems = [];

        public void Add<T>(T instance)
        {
            Add(typeof(T).Name, instance);
        }

        public void Add(string key, object? instance)
        {
            if (!_instanceItems.TryGetValue(key, out var val))
            {
                _instanceItems.TryAdd(key, instance);
                return;
            }
            if (val == instance)
            {
                return;
            }
            if (val is IDisposable c)
            {
                c.Dispose();
            }
            _instanceItems[key] = instance;
        }

        public void AddIf<T>()
        {
            var key = typeof(T).Name;
            if (_instanceItems.ContainsKey(key))
            {
                return;
            }
            Add(key);
        }

        private bool Has(Type type)
        {
            if (type == typeof(IEntryService))
            {
                return true;
            }
            return _instanceItems.ContainsKey(type.Name);
        }

        public T Get<T>()
        {
            var type = typeof(T);
            return (T)Get(type.Name, type);
        }


        public T Get<T>(string key)
        {
            return (T)Get(key, typeof(T));
        }

        private object Get(Type type)
        {
            return Get(type.Name, type);
        }
        private object Get(string key, Type type)
        {
            if (type == typeof(IEntryService))
            {
                return this;
            }
            if (_instanceItems.TryGetValue(key, out var val))
            {
                return val;
            }
            foreach (var ctor in type.GetConstructors())
            {
                if (!ctor.IsPublic)
                {
                    continue;
                }
                var parameters = ctor.GetParameters();
                if (ctor.GetParameters().Length == 0)
                {
                    return ctor.Invoke(null);
                }
                if (parameters.Where(i => !Has(i.ParameterType)).Any())
                {
                    continue;
                }
                return ctor.Invoke(parameters.Select(i => Get(i.ParameterType)).ToArray()); ;
            }
            throw new NotImplementedException();
        }

        public bool TryGet<T>([NotNullWhen(true)] out T? instance)
        {
            var type = typeof(T);
            if (!Has(type))
            {
                instance = default;
                return false;
            }
            instance = (T)Get(type);
            return true;
        }

        public bool TryGet<T>(string key, [NotNullWhen(true)] out T? instance)
        {
            if (!_instanceItems.TryGetValue(key, out var res))
            {
                instance = default;
                return false;
            }
            instance = (T)res;
            return true;
        }

        public async Task<T?> AskAsync<T>()
        {
            var picker = new PasswordDialog();
            var model = picker.ViewModel;
            var res = await App.ViewModel.OpenFormAsync(picker);
            if (!res)
            {
                return default;
            }
            var type = typeof(T);
            if (type == typeof(string))
            {
                return (T)(object)model.Password;
            }
            if (!type.IsClass)
            {
                return default;
            }
            var instance = Activator.CreateInstance(type);
            var property = type.GetProperty(nameof(model.Password));
            property?.SetValue(instance, model.Password);
            property = type.GetProperty("Dictionary");
            property?.SetValue(instance, model.DictFileName);
            return (T)instance;
        }

        public void Dispose()
        {
            foreach (var item in _instanceItems)
            {
                if (item.Value is IDisposable c)
                {
                    c.Dispose();
                }
            }
        }
    }
}
