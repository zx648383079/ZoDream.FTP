using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ZoDream.Shared.Interfaces
{
    public interface IEntryService
    {
        public void Add<T>(T instance);
        public void Add(string key, object? instance);
        public void AddIf<T>();
        public T Get<T>();
        public T Get<T>(string key);

        public bool TryGet<T>([NotNullWhen(true)] out T? instance);
        public bool TryGet<T>(string key, [NotNullWhen(true)] out T? instance);

        public Task<T?> AskAsync<T>();
    }
}
