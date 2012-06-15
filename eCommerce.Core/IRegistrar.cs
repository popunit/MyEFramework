using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Infrastructure;

namespace eCommerce.Core
{
    public interface IOrderable
    {
        int Order { get; }
    }

    /// <summary>
    /// Registrar for traget type
    /// </summary>
    /// <typeparam name="T">IoC type</typeparam>
    public interface IRegistrar : IOrderable
    {
        void Register(dynamic builder, IRoute route);
    }

    public abstract class RegistrarBase<T> : IRegistrar
    {
        public abstract void Register(T builder, IRoute route);

        public abstract int Order { get; }

        public void Register(dynamic builder, IRoute route)
        {
            Register((T)builder, route);
        }      
    }
}
