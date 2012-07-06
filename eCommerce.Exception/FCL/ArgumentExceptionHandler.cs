using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
