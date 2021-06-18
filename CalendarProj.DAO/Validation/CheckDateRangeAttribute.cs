using Google.Apis.Calendar.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace FastTripApp.Validation
{
    public class CheckDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Select day and time");
            }
            EventDateTime dt = (EventDateTime)value;
            if (dt.DateTime >= DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Make sure your date is more than today");
        }

    }
}
