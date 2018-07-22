using System;
using System.ComponentModel.DataAnnotations;


namespace BookLibrary.App.Helpers.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EndDateValidationAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public EndDateValidationAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
            this.ErrorMessage = "End date cannot be earlier or equal the start date.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {         
            var endDate = (DateTime?)value;

            if (endDate == null)
            {
                return ValidationResult.Success;
            }

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var startDate = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (endDate <= startDate)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
