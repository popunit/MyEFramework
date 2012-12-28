using eCommerce.Core;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.NoSql.DataProvider
{
    public class NoSqlDataProviderManager : DataProviderManagerBase
    {
        public NoSqlDataProviderManager(DatabaseSettings settings)
            : base(settings)
        { }

        public override IDataProvider DataProvider()
        {
            return AspectF.Define.MustBeNonNullOrEmpty().Return<IDataProvider>(() =>
                {
                    switch (Settings.DataProvider.ToUpperInvariant())
                    {
                        // don't worry this provider name which is not registered in machine.config,
                        // it will not be used for connectionstring in Web.config
                        case "MONGODB":
                            return new MongoDbDataProvider();
                        default:
                            // to support oracle in future
                            throw new CommonException(string.Format("Not supported nosql data provider : {0}", Settings.DataProvider));
                    }
                });
        }
    }
}
