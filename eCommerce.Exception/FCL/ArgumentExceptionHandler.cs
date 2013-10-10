using System;

namespace eCommerce.Exception.FCL
{
    public class ArgumentExceptionHandler : ErrorHandlerBase<ArgumentException>
    {
        public override void Handle(ArgumentException exception)
        {
            throw new CommonException(exception.Message);
        }
    }
}
