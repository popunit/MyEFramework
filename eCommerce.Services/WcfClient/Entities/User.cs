using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.WcfClient.Entities
{
    using System.Runtime.Serialization;
    using eCommerce.Core;
    using eCommerce.Services.WcfClient.DataTypes;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "User", Namespace = "http://schemas.datacontract.org/2004/07/eCommerce.Data.Domain.Users.Entities")]
    public partial class User : EntityBase
    {

        private bool ActivedField;

        private bool DeletedField;

        private string EmailField;

        private PasswordKit PasswordKitField;

        private System.Guid UserGuidField;

        private string UserNameField;

        private UserRole[] UserRolesField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Actived
        {
            get
            {
                return this.ActivedField;
            }
            set
            {
                this.ActivedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Deleted
        {
            get
            {
                return this.DeletedField;
            }
            set
            {
                this.DeletedField = value;
            }
        }

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
        public PasswordKit PasswordKit
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

        [System.Runtime.Serialization.DataMemberAttribute()]
        public UserRole[] UserRoles
        {
            get
            {
                return this.UserRolesField;
            }
            set
            {
                this.UserRolesField = value;
            }
        }
    }
}
