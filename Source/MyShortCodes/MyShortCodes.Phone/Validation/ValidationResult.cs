using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShortCodes.Phone.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public IList<string> Errors { get; private set; }

        public ValidationResult()
        {
            Errors = new List<string>();
        }
    }
}
