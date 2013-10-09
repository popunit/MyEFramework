using System;

namespace eCommerce.Core.Events
{
    public interface IObserverService
    {
        IObservable<ISubscriber<T>> GetSubscriptionCenter<T>() where T : class;
    }
}
