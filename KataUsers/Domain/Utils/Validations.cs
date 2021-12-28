using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static KataUsers.Domain.Types.Errors;

namespace KataUsers.Domain.Utils
{
    public static class Validations
    {
        public static List<ValidationErrorKey> ValidateRequired(Object value)
        {
            string? textValue = value as string;
            var isBlank = !(value != null && textValue?.Trim().Length != 0);

            return isBlank ? new List<ValidationErrorKey>() { ValidationErrorKey.FIELD_CANNOT_BE_BLANK } : new List<ValidationErrorKey>();
        }

        public static List<ValidationErrorKey> ValidateRegexp(string value, Regex regex)
        {
            return regex.Matches(value).Count > 0 ? new List<ValidationErrorKey>() : new List<ValidationErrorKey>() { ValidationErrorKey.INVALID_FIELD };
        }

    }
}


