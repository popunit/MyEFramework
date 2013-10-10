using System.ServiceModel;
using System.ServiceModel.Channels;

namespace eCommerce.Wcf.Framework.Context
{
    public enum MessageHeaderScope
    {
        Outgoing,
        Incoming
    }

    public static class OperationContextExtensions
    {
        public static MessageHeaders GetHeaders(this OperationContext context, MessageHeaderScope scope)
        {
            MessageHeaders headers = null;
            switch (scope)
            {
                case MessageHeaderScope.Outgoing:
                    headers = context.OutgoingMessageHeaders; break;
                case MessageHeaderScope.Incoming:
                    headers = context.IncomingMessageHeaders; break;
                default:
                    throw new InvalidMessageContractException();
            }

            return headers;
        }

        public static bool HasHeader<T>(this OperationContext context, string typeName, string nameSpace, MessageHeaderScope scope)
        {
            var exists = false;
            try
            {
                var headers = context.GetHeaders(scope);
                headers.GetHeader<T>(typeName, nameSpace);
                exists = true;
            }
            catch
            {
                exists = false;
            }

            return exists;
        }
    }
}
