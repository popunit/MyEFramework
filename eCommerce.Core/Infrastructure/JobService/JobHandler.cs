using Quartz;
using System;
using System.Collections.Generic;

namespace eCommerce.Core.Infrastructure.JobService
{
    public sealed class JobHandler
    {
        public IList<IJob> Jobs
        { get; private set; }

        public void AddJob()
        { throw new NotImplementedException(); }

        public void RemoveJob()
        { throw new NotImplementedException(); }

        public void SetJobStatus(JobStatus status)
        { throw new NotImplementedException(); }
    }

    public enum JobStatus
    {
        Enabled,
        Disabled
    }

    /// <summary>
    /// TO-DO
    /// </summary>
    public interface IJobDescriptor
    {
        string Name { get; }
        string Description { get; }
        void Enable();
        void Disable();
    }
}
