using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.WcfClient.Entities
{
    using System.Runtime.Serialization;
    using eCommerce.Core;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "User", Namespace = "http://schemas.datacontract.org/2004/07/eCommerce.Data.Domain.Users.Entities")]
    public partial class User : EntityBase
    {

        private string EmailField;

        private eCommerce.Services.WcfClient.DataTypes.PasswordKit PasswordKitField;

        private System.Guid UserGuidField;

        private string UserNameField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email
        {
            get
            {
                return this.EmailField;
            }
            set
            {
                this.EmailField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public eCommerce.Services.WcfClient.DataTypes.PasswordKit PasswordKit
        {
            get
            {
                return this.PasswordKitField;
            }
            set
            {
                this.PasswordKitField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid UserGuid
        {
            get
            {
                return this.UserGuidField;
            }
            set
            {
                this.UserGuidField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName
        {
            get
            {
                return this.UserNameField;
            }
            set
            {
                this.UserNameField = value;
            }
        }

        [DataMember]
        public bool Actived { get; set; }
    }
}
