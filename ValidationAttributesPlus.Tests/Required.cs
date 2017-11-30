using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ValidationAttributesPlus.Tests
{


  [TestClass]
  public class Required
  {
    private class Model
    {
      [RequiredIfNotEmpty("SecondPropertyName")]
      public string FirstPropertyName { get; set; }

      [RequiredIfNotEmpty("FirstPropertyName")]
      public string SecondPropertyName { get; set; }
    }

    [TestMethod]
    public void RequiredIfEmpty()
    {
      var model = new Model { FirstPropertyName = "FirstPropertyValue", SecondPropertyName = null};
      var validationResults = new List<ValidationResult>();
      var ctx = new ValidationContext(model, null, null);
      var isValid = Validator.TryValidateObject(model, ctx, validationResults, true);
      isValid.ShouldBe(false);

      model.FirstPropertyName = null;
      model.SecondPropertyName = "SecondPropertyValue";
      validationResults = new List<ValidationResult>();
      ctx = new ValidationContext(model, null, null);
      isValid = Validator.TryValidateObject(model, ctx, validationResults, true);
      isValid.ShouldBe(false);

      model.FirstPropertyName = "FirstPropertyValue";
      model.SecondPropertyName = "SecondPropertyValue";
      validationResults = new List<ValidationResult>();
      ctx = new ValidationContext(model, null, null);
      isValid = Validator.TryValidateObject(model, ctx, validationResults, true);
      isValid.ShouldBe(true);
    }
  }
}
