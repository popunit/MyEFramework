using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Events
{
    public enum EntityStatus
    {
        Update,
        Insert,
        Delete
    }

    public interface IEntityEvent<T> where T : EntityBase, new()
    {
        T Entity { get; }
        EntityStatus Status { get; }
    }
}
