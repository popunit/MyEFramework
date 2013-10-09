using eCommerce.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Data.Domain.Users
{
    public class PasswordKit
    {
        public string Password { get; set; }

        /// <summary>
        /// Enum supported by EF: http://blogs.msdn.com/b/efdesign/archive/2011/06/29/enumeration-support-in-entity-framework.aspx
        /// </summary>
        /// <remarks>
        /// EF 5 has supported enum type but WCF data service 5.x not yet
        // http://blogs.msdn.com/b/astoriateam/archive/2012/08/29/odata-101-using-the-notmapped-attribute-to-exclude-enum-properties.aspx
        /// </remarks>
        [NotMapped]
        public StringFormatType PasswordFormatType { get; set; }

        public PasswordKit()
        {
            // default set the clear text for password
            this.PasswordFormatType = StringFormatType.ClearText;
        }
    }
}
