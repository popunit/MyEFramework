using Autofac;
using eCommerce.Core;
using eCommerce.Core.Configuration;
using eCommerce.Core.Events;
using eCommerce.Core.Infrastructure;
using eCommerce.Core.Infrastructure.IoC;
using eCommerce.Services;
using eCommerce.Services.WcfClient.Entities;
using eCommerce.Web.Framework.Subscribers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework.Tests.Subscribers
{
    [TestFixture]
    public class ModelSubscriberTests
    {
        private IContainerManager manager;

        [SetUp]
        public void Setup()
        {
            var build = new ContainerBuilder();
            manager = new AutofacContainerManager(build.Build());

            manager.AddComponent<ISubscriber<EntityEvent<User>>, UserModelSubscriber>("user");
            manager.AddComponent<IObserverService, ObserverService>("service");
            EngineContext.Initialize(manager, new ContainerConfig(), false);
        }

        [Test]
        public void Entity_Changes_Trigger()
        {
            User user = new User();
            manager.Resolve<IObserverService>().GetSubscriptionCenter<EntityEvent<User>>().Subscribe(onNext => 
            {
                onNext.Handle(user.Mark(EntityStatus.Update));
            });
        }
    }
}
