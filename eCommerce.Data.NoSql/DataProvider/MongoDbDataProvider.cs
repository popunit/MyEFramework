using eCommerce.Core.Common;

namespace eCommerce.Data.NoSql.DataProvider
{
    public class MongoDbDataProvider : INoSqlDataProvider
    {
        public void Init()
        {
            CommandHelper.ExecuteCmd("net start mongodb");
        }
    }
}
