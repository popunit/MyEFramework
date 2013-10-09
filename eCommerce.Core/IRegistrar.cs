using eCommerce.Core.Infrastructure;

namespace eCommerce.Core
{
    public interface IOrderable
    {
        int Order { get; }
    }

    public enum Order
    {
        ASC,
        DESC,
    }

    /// <summary>
    /// Registrar for traget type
    /// </summary>
    public interface IRegistrar : IOrderable
    {
        void Register(dynamic builder, ISearcher route);
    }

    public abstract class RegistrarBase<T> : IRegistrar
    {
        public abstract void Register(T builder, ISearcher route);

        public abstract int Order { get; }

        public void Register(dynamic builder, ISearcher route)
        {
            Register((T)builder, route);
        }      
    }
}
