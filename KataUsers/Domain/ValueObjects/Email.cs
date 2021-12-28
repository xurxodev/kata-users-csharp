using System.Text.RegularExpressions;
using KataUsers.Domain.Types;
using KataUsers.Domain.Utils;

namespace KataUsers.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        const string EMAIL_PATTERN = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";

        public string Value { get; private set; }

        public Email(string value)
        {
            Validate(value);

            Value = value;
        }

        public static bool TryParse(string candidate, out Email? email, out List<ValidationError> errors)
        {
            try
            {
                Validate(candidate);
                email = new Email(candidate);
                errors = new List<ValidationError>();

                return true;
            }
            catch (ValidateException ex)
            {
                email = null;
                errors = ex.Errors;
                return false;
            }
        }

        private static void Validate(string value)
        {
            var requiredErrors = Validations.ValidateRequired(value);
            var regexpErrors = Validations.ValidateRegexp(value, new Regex(EMAIL_PATTERN));

            if (requiredErrors.Count > 0)
            {
                throw new ValidateException(new List<ValidationError>() { new ValidationError() { field = "Email", value = value, errors = requiredErrors } });
            }
            else if (regexpErrors.Count > 0)
            {
                throw new ValidateException(new List<ValidationError>() { new ValidationError() { field = "Email", value = value, errors = regexpErrors } });
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Value;
        }
    }
}
