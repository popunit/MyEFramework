using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Events
{
    public interface ISubscriber<in T>
        where T : class
    {
        //EntityStatus Status { get; }
        void Handle(T target);
    }
}
