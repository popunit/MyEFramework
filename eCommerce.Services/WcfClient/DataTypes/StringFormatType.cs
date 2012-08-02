using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.WcfClient.DataTypes
{
    using System.Runtime.Serialization;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "StringFormatType", Namespace = "http://schemas.datacontract.org/2004/07/eCommerce.Data.Common")]
    public enum StringFormatType : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ClearText = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Hashed = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Encrypted = 2,
    }
}
