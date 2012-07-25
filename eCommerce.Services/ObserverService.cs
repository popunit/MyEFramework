using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Events;
using eCommerce.Core.Infrastructure;
using eCommerce.Services.WcfClient.Entities;

namespace eCommerce.Services
{
    public class ObserverService : IObserverService
    {
        public IObservable<ISubscriber<T>> GetSubscriptionCenter<T>() where T : class
        {
            var subscribers = EngineContext.Current.ResolveAll<ISubscriber<T>>();
            return Observable.Create<ISubscriber<T>>(observer =>
            {
                foreach (var subscriber in subscribers)
                {
                    observer.OnNext(subscriber);
                }
                observer.OnCompleted();

                return Disposable.Empty;
            });
        }
    }
}
