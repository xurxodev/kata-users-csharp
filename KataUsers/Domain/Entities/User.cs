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
            Email? emailObj;
            List<ValidationError> emailErrors;
            Email.TryParse(email, out emailObj, out emailErrors);

            Password? passwordObj;
            List<ValidationError> passwordErrors;
            Password.TryParse(password, out passwordObj, out passwordErrors);

            var totalErrors = new List<ValidationError>();
            totalErrors.AddRange(emailErrors);
            totalErrors.AddRange(passwordErrors);

            var nameRequiredErrors = Validations.ValidateRequired(name);

            if (nameRequiredErrors.Count > 0)
            {
                totalErrors.Add(new ValidationError() { field = "Name", value = name, errors = nameRequiredErrors });
            }

            if (totalErrors.Count > 0)
            {
                throw new ValidateException(totalErrors);
            }

            Name = name;
            Email = emailObj!;
            Password = passwordObj!;
        }
    }
}
