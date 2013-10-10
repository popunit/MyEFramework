using eCommerce.Services.WcfClient.Entities;
using System;

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
