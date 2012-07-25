using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Events
{
    public interface IObserverService
    {
        IObservable<ISubscriber<T>> GetSubscriptionCenter<T>() where T : class;
    }
}
