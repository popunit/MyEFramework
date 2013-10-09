using System.Collections.Concurrent;

namespace eCommerce.Core
{
    public interface IWorkContext
    {
        ConcurrentDictionary<string, EntityBase> Items { get; }

        T GetData<T>(string name = "") where T : EntityBase, new();

        bool SetData<T>(T value, string name = "") where T : EntityBase, new();
    }
}
