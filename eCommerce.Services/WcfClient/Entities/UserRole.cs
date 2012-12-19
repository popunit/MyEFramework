using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.WcfClient.Entities
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "UserRole", Namespace = "http://schemas.datacontract.org/2004/07/eCommerce.Data.Domain.Users.Entities")]
    public partial class UserRole : eCommerce.Core.EntityBase
    {

        private bool ActivedField;

        private bool IsSystemRoleField;

        private string RoleNameField;
        
        private string SystemNameField;
        
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
        public bool IsSystemRole
        {
            get
            {
                return this.IsSystemRoleField;
            }
            set
            {
                this.IsSystemRoleField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RoleName
        {
            get
            {
                return this.RoleNameField;
            }
            set
            {
                this.RoleNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SystemName
        {
            get
            {
                return this.SystemNameField;
            }
            set
            {
                this.SystemNameField = value;
            }
        }
    }
}
