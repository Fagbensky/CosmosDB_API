using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.ModelValidators
{
    public class ValidIfIncluded: ValidationAttribute
    {
        private string[] _allowedValues;

        public ValidIfIncluded(string[] allowedValues)
        {
            _allowedValues = allowedValues;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            var position = value.ToString();
            if (_allowedValues.Contains(position))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"{position} is not a valid {validationContext.DisplayName}");
        }
    }
}
