using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MvcClient.Infrastructure
{
    internal sealed class StartTimeAttribute : ValidationAttribute, IClientModelValidator
    {
        public string ErrorMessage { get; set; } = "Start time must be before than Stop time";

        public string EndTimeProperty { get; set; }

        public StartTimeAttribute(string endTimeProperty)
        {
            this.EndTimeProperty = endTimeProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var basePropertyInfo = validationContext.ObjectType.GetProperty(EndTimeProperty);
            var endTime = (TimeSpan)basePropertyInfo!.GetValue(validationContext.ObjectInstance, null)!;
            var startTime = (TimeSpan)value!;
            if (startTime > endTime)
            {
                return new ValidationResult(ErrorMessage);
            }

            return null;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-isBefore", ErrorMessage);
            context.Attributes.Add("data-val-isBefore-endTime", EndTimeProperty);
        }
    }
}
