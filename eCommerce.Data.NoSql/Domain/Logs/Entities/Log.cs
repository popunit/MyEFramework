using eCommerce.Data.Domain.Users.Entities;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.NoSql.Domain.Logs.Entities
{
    public class Log : Entity
    {
        public virtual int LogLevelId { get; set; }

        public virtual string ShortMessage { get; set; }

        public virtual string FullMessage { get; set; }

        public virtual string IpAddress { get; set; }

        public virtual long? UserId { get; set; }

        public virtual string PageUrl { get; set; }

        public virtual string ReferrerUrl { get; set; }

        public virtual DateTime CreatedByTime { get; set; }

        /// <summary>
        /// Gets or sets the log level
        /// </summary>
        public virtual LogLevel LogLevel
        {
            get
            {
                return (LogLevel)this.LogLevelId;
            }
            set
            {
                this.LogLevelId = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public virtual User User { get; set; }
    }
}
