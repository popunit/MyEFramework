using Autofac;
using Autofac.Integration.Wcf;
using eCommerce.Core;
using eCommerce.Data.Domain.Common;
using eCommerce.Wcf.Services.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Wcf.Services.Common
{
    public class GenericCharacteristicService : IGenericCharacteristicService
    {
        private readonly IRepository<GenericCharacteristic> genericCharacteristicRepository;
        private readonly ICacheManager cacheManager;
        private readonly ILifetimeScope container;

        public GenericCharacteristicService()
        {
            this.container = AutofacHostFactory.Container;
            this.genericCharacteristicRepository = 
                this.container.Resolve<IRepository<GenericCharacteristic>>();
            this.cacheManager = container.Resolve<ICacheManager>();
        }

        public bool InsertCharacteristic(GenericCharacteristic characteristic)
        {
            throw new NotImplementedException();
        }

        public GenericCharacteristic GetCharacteristicById(int characteristicId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenericCharacteristic> GetCharacteristicForEntity(int entityId, string group)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCharacteristic(GenericCharacteristic characteristic)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCharacteristic(GenericCharacteristic characteristic)
        {
            throw new NotImplementedException();
        }

        public bool SaveCharacteristic(Core.EntityBase entity, string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
