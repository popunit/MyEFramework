
namespace eCommerce.Core.Events
{
    public enum EntityStatus
    {
        Update,
        Insert,
        Delete
    }

    public interface IEntityEvent<out T> where T : EntityBase, new()
    {
        T Entity { get; }
        EntityStatus Status { get; }
    }
}
