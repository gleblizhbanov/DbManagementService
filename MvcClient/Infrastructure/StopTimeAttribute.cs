using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MvcClient.Infrastructure
{
    internal sealed class StopTimeAttribute : ValidationAttribute, IClientModelValidator
    {
        public string ErrorMessage { get; set; } = "Stop time must be later than Start time";

        public string StartTimeProperty { get; set; }

        public StopTimeAttribute(string startTimeProperty)
        {
            this.StartTimeProperty = startTimeProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var basePropertyInfo = validationContext.ObjectType.GetProperty(StartTimeProperty);
            var startTime = (TimeSpan)basePropertyInfo!.GetValue(validationContext.ObjectInstance, null)!;
            var endTime = (TimeSpan)value!;
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
            context.Attributes.Add("data-val-isLater", ErrorMessage);
            context.Attributes.Add("data-val-isLater-startTime", StartTimeProperty);
        }
    }
}
