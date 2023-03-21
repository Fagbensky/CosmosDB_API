using Microsoft.Azure.Cosmos.Spatial;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.ModelValidators
{
    public class ValidArrayIfAllIncluded : ValidationAttribute
    {
        private string[] _allowedValues;

        public ValidArrayIfAllIncluded(string[] allowedValues)
        {
            _allowedValues = allowedValues;
        }

        protected override ValidationResult? IsValid(object? values, ValidationContext validationContext)
        {
            if (values == null)
            {
                return ValidationResult.Success;
            }
            var valuesArray = (string[])values;
            foreach (string value in valuesArray)
            {
                if (!(_allowedValues.Contains(value)))
                {
                    return new ValidationResult($"{value} is not a valid {validationContext.DisplayName}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
