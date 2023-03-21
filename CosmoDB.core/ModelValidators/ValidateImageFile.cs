using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.ModelValidators
{
    internal class ValidateImageFile: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName} is not valid");
            }
            var file = (IFormFile)value;
            if (!(file != null && file.ContentType.Contains("image")))
            {
                return new ValidationResult($"Cover Image is not valid");
            }
            return ValidationResult.Success;
        }
    }
}
