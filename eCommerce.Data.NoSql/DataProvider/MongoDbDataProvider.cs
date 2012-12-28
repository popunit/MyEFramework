using eCommerce.Core;
using eCommerce.Core.Common;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.NoSql.DataProvider
{
    public class MongoDbDataProvider : INoSqlDataProvider
    {
        public void Init()
        {
            CommandHelper.ExecuteCmd("net start mongodb");
            BsonClassMap.RegisterClassMap<EntityBase>(cm =>
            {
                cm.AutoMap();
                cm.UnmapProperty(c => c.Id); // unmap EntityBase.Id
            });
        }
    }
}
