using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Core.Events;

namespace eCommerce.Web.Framework.Subscribers
{
    public abstract class EntityModelSubscriber<T> : ISubscriber<EntityEvent<T>>
        where T : EntityBase, new()
    {
        public virtual void Handle(EntityEvent<T> target)
        {
            switch (target.Status)
            {
                case EntityStatus.Update:
                    { Update(); break; }
                case EntityStatus.Insert:
                    { Insert(); break; }
                case EntityStatus.Delete:
                    { Delete(); break; }
            }
        }

        public abstract void Update();
        public abstract void Insert();
        public abstract void Delete();
    }
}
