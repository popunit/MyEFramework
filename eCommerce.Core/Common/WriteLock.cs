using System;
using System.Threading;

namespace eCommerce.Core.Common
{
    public class WriteLock : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;

        public WriteLock(ReaderWriterLockSlim rwLock)
        {
            this._rwLock = rwLock;
            this._rwLock.EnterWriteLock();
        }

        public void Dispose()
        {
            this._rwLock.ExitWriteLock();
        }
    }
}
