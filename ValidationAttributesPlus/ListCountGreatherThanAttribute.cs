using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ValidationAttributesPlus
{
  public class ListCountGreatherThanAttribute : ValidationAttribute
  {
    private readonly int _min;

    public ListCountGreatherThanAttribute(int min)
    {
      _min = min;
    }

    public override bool IsValid(object value)
    {
      var list = value as IList;
      return list?.Count > _min;
    }
  }
}
