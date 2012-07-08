using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Data.Common;

namespace eCommerce.Data.Domain.Users
{
    public class PasswordKit
    {
        public string Password { get; set; }

        /// <summary>
        /// Enum supported by EF: http://blogs.msdn.com/b/efdesign/archive/2011/06/29/enumeration-support-in-entity-framework.aspx
        /// </summary>
        public StringFormatType PasswordFormatType { get; set; }

        public PasswordKit()
        {
            // default set the clear text for password
            this.PasswordFormatType = StringFormatType.ClearText;
        }
    }
}
