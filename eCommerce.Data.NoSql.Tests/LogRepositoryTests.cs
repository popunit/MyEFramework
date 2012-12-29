using eCommerce.Core;
using eCommerce.Core.Common.Web;
using eCommerce.Core.Data;
using eCommerce.Data.Domain.Common;
using eCommerce.Data.Domain.Users.Entities;
using eCommerce.Data.NoSql.DataProvider;
using eCommerce.Data.NoSql.Domain.Logs.Entities;
using eCommerce.Data.NoSql.Repositories;
using NUnit.Framework;
using System;
using System.Linq;

namespace eCommerce.Data.NoSql.Tests
{
    [TestFixture]
    public class LogRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            var dbSettingsManager = new DatabaseSettingsManager();
            var databaseSettings = dbSettingsManager.LoadSettings();
            IDataProviderManager manager = new NoSqlDataProviderManager(databaseSettings);
            manager.DataProvider().Init();
        }

        [Test]
        public void Add_Log_Item()
        {
            try
            {
                MongoDbRepository<Log> logRepository = new MongoDbRepository<Log>();
                logRepository.DeleteAll();

                User user = new User();
                user.Id = 1;
                user.UserGuid = Guid.NewGuid();
                user.UserName = "Test User";
                Log item = new Log
                {
                    CreatedByTime = DateTime.UtcNow,
                    FullMessage = "This is a test message",
                    //Id = guid,
                    IpAddress = WebExtensions.CurrentMachineIPv4(),
                    LogLevel = LogLevel.Info,
                    ShortMessage = "Test",
                    User = user,
                    UserId = user.Id
                };

                logRepository.Insert(item);

                MongoDbRepository<Log> logRepository2 = new MongoDbRepository<Log>();
                Assert.AreEqual(logRepository2.Table.Count(), 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
