using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Core.Common
{
    public class WriteLock : IDisposable
    {
        private readonly ReaderWriterLockSlim rwLock;

        public WriteLock(ReaderWriterLockSlim rwLock)
        {
            this.rwLock = rwLock;
            this.rwLock.EnterWriteLock();
        }

        public void Dispose()
        {
            this.rwLock.ExitWriteLock();
        }
    }
}
