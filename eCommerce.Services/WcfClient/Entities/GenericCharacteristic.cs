using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.WcfClient.Entities
{
    using eCommerce.Core;
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "GenericCharacteristic", Namespace = "http://schemas.datacontract.org/2004/07/eCommerce.Data.Domain.Common")]
    public partial class GenericCharacteristic : EntityBase
    {

        private long EntityIdField;

        private string GroupField;

        private string KeyField;

        private string ValueField;

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long EntityId
        {
            get
            {
                return this.EntityIdField;
            }
            set
            {
                this.EntityIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Group
        {
            get
            {
                return this.GroupField;
            }
            set
            {
                this.GroupField = value;
            }
        }

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
