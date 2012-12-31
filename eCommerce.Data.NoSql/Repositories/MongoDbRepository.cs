using eCommerce.Core;
using eCommerce.Extensions.Data.MongoRepository;
using eCommerce.Extensions.Data.MongoRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.NoSql.Repositories
{
    public class MongoDbRepository<T> : MongoRepository<T>
        where T : EntityBase, IEntity
    {
        
    }
}
