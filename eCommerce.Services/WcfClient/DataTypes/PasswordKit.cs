using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.WcfClient.DataTypes
{
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PasswordKit", Namespace = "http://schemas.datacontract.org/2004/07/eCommerce.Data.Domain.Users")]
    public partial class PasswordKit : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string PasswordField;

        private StringFormatType PasswordFormatTypeField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public StringFormatType PasswordFormatType
        {
            get
            {
                return this.PasswordFormatTypeField;
            }
            set
            {
                this.PasswordFormatTypeField = value;
            }
        }
    }
}
