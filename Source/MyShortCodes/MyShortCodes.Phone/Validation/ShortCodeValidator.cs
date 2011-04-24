using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MyShortCodes.Phone.Domain;
using System.Text.RegularExpressions;

namespace MyShortCodes.Phone.Validation
{
    public class ShortCodeValidator : IValidator<ShortCode>
    {
        public ValidationResult Validate(ShortCode target)
        {
            var result = new ValidationResult { IsValid = true };

            if(String.IsNullOrEmpty(target.Name)) 
            {
                result.IsValid = false;
                result.Errors.Add("Name is required");
            }

            if (String.IsNullOrEmpty(target.Code))
            {
                result.IsValid = false;
                result.Errors.Add("Code is required");
            }
            else
            {
                if (!Regex.Match(target.Code, "[0-9]{2,6}").Success)
                {
                    result.IsValid = false;
                    result.Errors.Add("Code must be 2 to 6 digits");
                }
            }

            return result;
        }
    }
}
