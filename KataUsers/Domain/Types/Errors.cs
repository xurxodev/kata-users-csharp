

using static KataUsers.Domain.Types.Errors;

namespace KataUsers.Domain.Types
{
    public static class Errors
    {

        public enum ValidationErrorKey
        {
            FIELD_CANNOT_BE_BLANK,
            INVALID_FIELD
        }

        public static Dictionary<ValidationErrorKey, Func<string, string>> ValidationErrorMessages = new Dictionary<ValidationErrorKey, Func<string, string>>(){
                {ValidationErrorKey.FIELD_CANNOT_BE_BLANK, field=> $"{Capitalize(field)} cannot be blank"},
                {ValidationErrorKey.INVALID_FIELD,field=> $"Invalid {Capitalize(field)}" }
            };

        public static string Capitalize(string text)
        {
            switch (text)
            {
                case null: throw new ArgumentNullException(nameof(text));
                case "": throw new ArgumentException($"{nameof(text)} cannot be empty", nameof(text));
                default: return text[0].ToString().ToUpper() + text.Substring(1);
            }
        }
    }

    public readonly struct ValidationError
    {
        public string field { get; init; }
        public object value { get; init; }
        public List<ValidationErrorKey> errors { get; init; }
    }
}
