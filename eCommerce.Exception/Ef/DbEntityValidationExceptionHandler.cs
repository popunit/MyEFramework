using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace eCommerce.Exception.Ef
{
    public class DbEntityValidationExceptionHandler : ErrorHandlerBase<DbEntityValidationException>
    {
        public override void Handle(DbEntityValidationException exception)
        {
            var msg = exception.EntityValidationErrors.
                SelectMany(validationErrors => validationErrors.ValidationErrors).
                Aggregate(string.Empty, (current, validationError) => 
                    current + (string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine));

            var fail = new CommonException(msg, exception);
            throw fail;
        }
    }
}
