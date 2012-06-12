using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Infrastructure.IoC
{
    public enum LifeStyle
    {
        Singleton = 0, // notice the result in web farm
        Transient = 1,
        Scope = 2
    }
}
