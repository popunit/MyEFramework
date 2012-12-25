using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{
    public interface ITask : IOrderable
    {
        void Execute();
    }

    /// <summary>
    /// the tasks have nothing to do with database
    /// </summary>
    public interface IWarmupTask : ITask
    { 
    }
}
