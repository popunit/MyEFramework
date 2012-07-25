using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Events;
using eCommerce.Services.WcfClient.Entities;

namespace eCommerce.Web.Framework.Subscribers
{
    public class UserModelSubscriber : EntityModelSubscriber<User>
    {
        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Insert()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
