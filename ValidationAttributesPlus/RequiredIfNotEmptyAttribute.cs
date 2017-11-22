using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ValidationAttributesPlus
{
  public class RequiredIfNotEmptyAttribute : ValidationAttribute
  {
    private readonly string _otherPropertyName;

    public RequiredIfNotEmptyAttribute(string otherPropertyName)
    {
      _otherPropertyName = otherPropertyName;
    }

    protected override ValidationResult IsValid(object thisPropertyValue, ValidationContext validationContext)
    {
      var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);
      if (otherProperty == null) return new ValidationResult($"Unknown property {_otherPropertyName}");

      var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);
      var thisPropertyName = validationContext.MemberName;
      if (thisPropertyValue == null && otherPropertyValue != null)
        return new ValidationResult($"{thisPropertyName} must be not null");

      return ValidationResult.Success;
    }
  }
}
