
namespace eCommerce.Core.Diagnosis
{
    public class DebugHelper : IDebugHelper
    {
        public bool DebugEnabled { get; protected set; }

        public DebugHelper()
        {
            // TO-DO: NotImplement
            // config provider to indicate if true or false
            this.DebugEnabled = true;
        }
    }
}
