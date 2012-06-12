using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Infrastructure;

namespace eCommerce.Core
{
    /// <summary>
    /// Registrar for traget type
    /// </summary>
    /// <typeparam name="T">IoC type</typeparam>
    public interface IRegistrar
    {
        void Register(dynamic builder, IRoute route);
        int Order { get; }
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
