using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.WcfClient.Entities
{
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "UserCharacteristic", Namespace = "http://schemas.datacontract.org/2004/07/eCommerce.Data.Domain.Users.Entities")]
    public partial class UserCharacteristic : eCommerce.Core.EntityBase
    {

        private string KeyField;

        private User UserField;

        private long UserIdField;

        private string ValueField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key
        {
            get
            {
                return this.KeyField;
            }
            set
            {
                this.KeyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public User User
        {
            get
            {
                return this.UserField;
            }
            set
            {
                this.UserField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long UserId
        {
            get
            {
                return this.UserIdField;
            }
            set
            {
                this.UserIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this.ValueField;
            }
            set
            {
                this.ValueField = value;
            }
        }
    }
}
