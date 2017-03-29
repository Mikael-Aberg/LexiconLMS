using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Models.Data_Annotation
{
    public class RequiredIfNotAttribute : ValidationAttribute
    {
        private readonly string property;

        public RequiredIfNotAttribute(string property)
        {
            this.property = property;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(this.property);
            if (propertyTestedInfo == null)
            {
                return new ValidationResult(string.Format("unknown property {0}", this.property));
            }

            var propertyTestedValue = (bool)propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

            if (propertyTestedValue)
            {
                return ValidationResult.Success;
            }
            else
            {
                if (value != null)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("", null);
        }
    }
}