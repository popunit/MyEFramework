using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Exception
{
    public interface IHandler
    {
        void Handle(object obj);
    }

    public interface IErrorHandler<T> : IHandler
        where T : System.Exception
    {
        Type GetExceptionType();
        void Handle(T exception);
    }

    public abstract class ErrorHandlerBase<T> : IErrorHandler<T>
        where T : System.Exception
    {

        public Type GetExceptionType()
        {
            return this.GetType().BaseType.GetGenericArguments()[0];
        }

        public abstract void Handle(T exception);

        public void Handle(object obj)
        {
            var ex = obj as T;
            if (null == ex)
                return;
            Handle(ex);
        }
    }
}
