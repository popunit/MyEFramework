using eCommerce.Data.Domain.Users.Entities;
using eCommerce.Data.NoSql.Domain.Logs;
using eCommerce.Data.NoSql.Domain.Logs.Entities;
using MongoRepository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Data.NoSql.Tests
{
    [TestFixture]
    public class LogRepositoryTests
    {
        [Test]
        public void Add_Log_Item()
        {
            MongoRepository<Log> logRepository = new MongoRepository<Log>();
            logRepository.DeleteAll();

            string guid = Guid.NewGuid().ToString();
            User user = new User();
            user.Id = 1;
            user.UserGuid = Guid.NewGuid();
            user.UserName = "Test User";
            Log item = new Log
            {
                CreatedByTime = DateTime.UtcNow,
                FullMessage = "This is a test message",
                //Id = guid,
                IpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString(),
                LogLevel = LogLevel.Info,
                ShortMessage = "Test",
                User = user,
                UserId = user.Id
            };

            logRepository.Add(item);

            MongoRepository<Log> logRepository2 = new MongoRepository<Log>();
            Assert.AreEqual(logRepository2.Count(), 1);
        }
    }
}
