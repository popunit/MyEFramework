using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure.NoAOP;

namespace eCommerce.Core
{
    public interface IDataProviderManager
    {
        DatabaseSettings Settings { get; }
        IDataProvider DataProvider();
    }

    public abstract class DataProviderManagerBase : IDataProviderManager
    {
        protected DataProviderManagerBase(DatabaseSettings settings)
        {
            AspectF.Define.MustBeNonNull(settings).Do(() =>
            {
                this.Settings = settings;
            });
        }

        //protected DatabaseSettings Settings { get; private set; }
        public IDataProvider DataProvider()
        {
            throw new NotImplementedException();
        }

        public virtual DatabaseSettings Settings
        {
            get;
            private set;
        }
    }
}
