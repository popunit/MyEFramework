
namespace eCommerce.Core.Events
{
    public interface ISubscriber<in T>
        where T : class
    {
        //EntityStatus Status { get; }
        void Handle(T target);
    }
}
