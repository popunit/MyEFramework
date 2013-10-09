using eCommerce.Core;
using eCommerce.Core.Data;
using eCommerce.Core.Infrastructure.NoAOP;
using eCommerce.Exception;

namespace eCommerce.Data.DataProvider
{
    public class EfDataProviderManager : DataProviderManagerBase
    {
        public EfDataProviderManager(DatabaseSettings settings)
            : base(settings)
        {
        }

        public override IDataProvider DataProvider()
        {
            return AspectF.Define.MustBeNonNullOrEmpty(Settings.DataProvider)
                .Return<IDataProvider>(() => 
                {
                    switch (Settings.DataProvider.ToUpperInvariant())
                    {
                        // don't worry this provider name which is not registered in machine.config,
                        // it will not be used for connectionstring in Web.config
                        case "SQLSERVER":
                            return new MssqlServerDataProvider();
                        case "SQLCE":
                            return new MssqlServerCeDataProvider();
                        default:
                            // to support oracle in future
                            throw new CommonException(string.Format("Not supported data provider : {0}", Settings.DataProvider));
                    }
                });
        }
    }
}
