using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace eCommerce.Wcf.Framework.Context
{
    /// <summary>
    /// Wrap and transit wcf context via message headers
    /// </summary>
    [DataContract]
    public class GenericContext<T>
    {
        private static readonly string typeName;
        private static readonly string nameSpace;

        static GenericContext()
        {
            Type t = typeof(T);
            if (!t.IsSerializable() && !t.IsDataContract())
                throw new TypeAccessException();
            typeName = "GenericContext";
            nameSpace = "net.clr:" + t.FullName;
        }

        public static GenericContext<T> Current
        {
            get 
            {
                var context = OperationContext.Current;
                if (context.IsNull())
                    return null;

                try
                {
                    return context.IncomingMessageHeaders.GetHeader<GenericContext<T>>(typeName, nameSpace);
                }
                catch
                {
                    return null;
                }
            }
            set // we can only set an instance of one type into header
            {
                var context = OperationContext.Current;
                if (context.IsNull())
                    throw new Exception("Operation Context cannot be null!");

                if (context.HasHeader<GenericContext<T>>(typeName, nameSpace, MessageHeaderScope.Outgoing))
                    throw new InvalidOperationException("A header with name " + typeName + " and namespace " + nameSpace + " already exists in the message.");

                MessageHeader<GenericContext<T>> header = new MessageHeader<GenericContext<T>>(value);
                context.OutgoingMessageHeaders.Add(header.GetUntypedHeader(typeName, nameSpace));
            }
        }

        [DataMember]
        public readonly T Value;

        public GenericContext(T value)
        {
            this.Value = value;
        }
    }
}
