
namespace eCommerce.Core.Events
{
    public class EntityEvent<T> : IEntityEvent<T>
        where T : EntityBase, new()
    {
        public T Entity
        {
            get;
            private set;
        }

        public EntityStatus Status
        {
            get;
            private set;
        }

        public EntityEvent(T entity, EntityStatus status)
        {
            this.Entity = entity;
            this.Status = status;
        }
    }
}
