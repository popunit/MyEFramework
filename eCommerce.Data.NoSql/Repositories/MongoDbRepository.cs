using eCommerce.Core;
using eCommerce.Extensions.Data.MongoRepository;
using eCommerce.Extensions.Data.MongoRepository.Repository;

namespace eCommerce.Data.NoSql.Repositories
{
    public class MongoDbRepository<T> : MongoRepository<T>
        where T : EntityBase, IEntity
    {
        
    }
}
