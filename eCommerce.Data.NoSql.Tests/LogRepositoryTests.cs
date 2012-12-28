using eCommerce.Data.Domain.Common;
using eCommerce.Data.Domain.Users.Entities;
using eCommerce.Data.NoSql.Domain.Logs.Entities;
using eCommerce.Data.NoSql.Repositories;
using eCommerce.Extensions.Data.MongoRepository.Repository;
using NUnit.Framework;
using System;
using System.Net;
using System.Linq;
using MongoDB.Bson.Serialization;
using eCommerce.Core;
using eCommerce.Data.NoSql.DataProvider;
using eCommerce.Core.Data;
using eCommerce.Core.Common.Web;

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
                logRepository.Clear();

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
