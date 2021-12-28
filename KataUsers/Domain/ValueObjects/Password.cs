using System.Text.RegularExpressions;
using KataUsers.Domain.Types;
using KataUsers.Domain.Utils;

namespace KataUsers.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        const string PASSWORD_PATTERN = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

        public string Value { get; private set; }

        public Password(string value)
        {

            Validate(value);
            Value = value;

        }

        public static bool TryParse(string candidate, out Password? email, out List<ValidationError> errors)
        {
            try
            {
                Validate(candidate);
                email = new Password(candidate);
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
            var regexpErrors = Validations.ValidateRegexp(value, new Regex(PASSWORD_PATTERN));

            if (requiredErrors.Count > 0)
            {
                throw new ValidateException(new List<ValidationError>() { new ValidationError() { field = "Password", value = value, errors = requiredErrors } });
            }
            else if (regexpErrors.Count > 0)
            {
                throw new ValidateException(new List<ValidationError>() { new ValidationError() { field = "Password", value = value, errors = regexpErrors } });
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Value;
        }
    }
}
