using System.Text.RegularExpressions;
using KataUsers.Domain.Types;
using KataUsers.Domain.Utils;
using KataUsers.Domain.ValueObjects;

namespace KataUsers.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }

        public User(string name, string email, string password, Guid id = new Guid()) : base(id.ToString())
        {
            var totalErrors = new List<ValidationError>();

            Email? emailObj = CreateEmail(email, totalErrors);

            Password? passwordObj = CreatePassword(password, totalErrors);

            ValidateName(name, totalErrors);

            if (totalErrors.Count > 0)
            {
                throw new ValidateException(totalErrors);
            }

            Name = name;
            Email = emailObj!;
            Password = passwordObj!;
        }

        private static Password? CreatePassword(string password, List<ValidationError> totalErrors)
        {
            Password? passwordObj;
            List<ValidationError> passwordErrors;

            Password.TryParse(password, out passwordObj, out passwordErrors);
            totalErrors.AddRange(passwordErrors);

            return passwordObj;
        }

        private static Email? CreateEmail(string email, List<ValidationError> totalErrors)
        {
            Email? emailObj;
            List<ValidationError> emailErrors;

            Email.TryParse(email, out emailObj, out emailErrors);
            totalErrors.AddRange(emailErrors);

            return emailObj;
        }

        private static void ValidateName(string name, List<ValidationError> totalErrors)
        {
            var nameRequiredErrors = Validations.ValidateRequired(name);

            if (nameRequiredErrors.Count > 0)
            {
                totalErrors.Add(new ValidationError() { field = "Name", value = name, errors = nameRequiredErrors });
            }
        }
    }
}
