using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Diagnosis
{
    public class DebugHelper : IDebugHelper
    {
        private bool enableDebug = false;
        public bool DebugEnabled
        {
            get { return enableDebug; }
            protected set { enableDebug = value; }
        }

        public DebugHelper()
        {
            // TO-DO: NotImplement
            // config provider to indicate if true or false
            this.enableDebug = true;
        }
    }
}
