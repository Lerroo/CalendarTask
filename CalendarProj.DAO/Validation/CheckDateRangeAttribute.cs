using Google.Apis.Calendar.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CalendarProj.

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
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            IGoogleCalendarService googleCalendarService 

            // This call fails with global bad request
            var fbres = fbq.Fetch();

            return new ValidationResult(ErrorMessage ?? "Make sure your date is >= than today");
        }

    }
}
