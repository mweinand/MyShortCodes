using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShortCodes.Phone.Validation
{
    public interface IValidator<TEntity> where TEntity : class
    {
        ValidationResult Validate(TEntity target);
    }
}
