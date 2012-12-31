using eCommerce.Core;
using eCommerce.Data.Domain.Common;
using eCommerce.Data.Domain.Users.Entities;
using eCommerce.Extensions.Data.MongoRepository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEntities = eCommerce.Data.Domain.Common.Entities;

namespace eCommerce.Data.NoSql.Domain.Logs.Entities
{
    /// <summary>
    /// TO-DO: need to convet between Data/log type and Data.Nosql/log type
    /// </summary>
    [CollectionName("Log")] // hack here, should set collection name in order to avoid error brought by EntityBase
    public class Log : DataEntities.Log, IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }
    }
}
