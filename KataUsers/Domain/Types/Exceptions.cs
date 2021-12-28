namespace KataUsers.Domain.Types
{
    public class ValidateException : Exception
    {
        public List<ValidationError> Errors { get; private set; }

        public ValidateException(List<ValidationError> errors)
            : base(String.Join("\n", errors))
        {
            Errors = errors;
        }
    }

    public class DuplicateResourceException : Exception
    {

        public DuplicateResourceException(string message)
            : base(message)
        {
        }
    }
}