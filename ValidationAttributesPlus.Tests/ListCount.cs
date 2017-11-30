using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ValidationAttributesPlus.Tests
{


  [TestClass]
  public class ListCount
  {
    private class Model
    {
      [ListCountGreatherThan(1)]
      public IList<string> GreatherThanOne { get; set; } = new List<string>();

      [ListCountGreatherThan(2)]
      public IList<string> GreatherThanFive { get; set; } = new List<string>();
    }

    [TestMethod]
    public void ListCountGreatherThan()
    {
      // Validation should pass
      var listOne = new List<string> { "First", "Second", "Third" };
      var modelOne = new Model
      {
        GreatherThanOne = listOne,
        GreatherThanFive = listOne
      };

      var validationResults = new List<ValidationResult>();
      var ctx = new ValidationContext(modelOne, null, null);
      var isValid = Validator.TryValidateObject(modelOne, ctx, validationResults, true);
      isValid.ShouldBe(true);

      // Validation should fail
      var listTwo = new List<string> { "First", "Second" };
      var modelTwo = new Model
      {
        GreatherThanOne = listTwo,
        GreatherThanFive = listTwo
      };

      validationResults = new List<ValidationResult>();
      ctx = new ValidationContext(modelTwo, null, null);
      isValid = Validator.TryValidateObject(modelTwo, ctx, validationResults, true);
      isValid.ShouldBe(false);
    }
  }
}
