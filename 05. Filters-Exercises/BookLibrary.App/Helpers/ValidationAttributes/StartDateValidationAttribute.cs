using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.App.Helpers.ValidationAttributes
{
    public class StartDateValidationAttribute : ValidationAttribute
    {
        public StartDateValidationAttribute()
        {
            this.ErrorMessage = "Start date cannot be earlier than 1 January 2000";
        }

        public override bool IsValid(object value)
        {
            var startDate = (DateTime)value;
            var earliestValidDate = DateTime.Parse("01.01.2000");
            return earliestValidDate < startDate;
        }
    }
}
