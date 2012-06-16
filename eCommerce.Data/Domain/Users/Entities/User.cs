using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;

namespace eCommerce.Data.Domain.Users.Entities
{
    public partial class User : EntityBase
    {
        public User()
        {
 
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public PasswordKit PasswordKit { get; set; }
        public string Email { get; set; }
    }
}
