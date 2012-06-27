using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Exception.Ef
{
    public class DbEntityValidationExceptionHandler : ErrorHandlerBase<DbEntityValidationException>
    {
        public override void Handle(DbEntityValidationException exception)
        {
            var msg = string.Empty;

            foreach (var validationErrors in exception.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

            var fail = new System.Exception(msg, exception);
            throw fail;
        }
    }
}
