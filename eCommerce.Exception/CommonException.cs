using System;

namespace eCommerce.Exception
{
    [Serializable]
    public class CommonException : System.Exception
    {
        public CommonException() { }
        public CommonException(string message) : base(message) { }
        public CommonException(string message, System.Exception inner) : base(message, inner) { }
        protected CommonException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
