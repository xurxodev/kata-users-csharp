using System;
using System.Text.RegularExpressions;
using KataUsers.Domain.Types;
using KataUsers.Domain.Utils;
using KataUsers.Domain.ValueObjects;
using Xunit;

namespace KataUsers.Tests.Domain.ValueObjects
{
    public class PasswordShould
    {
        [Fact]
        public void return_password_if_value_is_valid()
        {
            var passwordValue = "12345678A";
            var password = new Password(passwordValue);

            Assert.NotNull(password);
        }

        [Fact]
        public void return_field_cannot_be_blank_if_value_is_empty()
        {
            var passwordValue = "";

            Action act = () => new Password(passwordValue);

            ValidateException exception = Assert.Throws<ValidateException>(act);

            Assert.Single(exception.Errors);
            Assert.Single(exception.Errors[0].errors);
            Assert.Equal(Errors.ValidationErrorKey.FIELD_CANNOT_BE_BLANK, exception.Errors[0].errors[0]);
        }

        [Fact]
        public void return_invalid_field_if_value_is_not_valid()
        {
            var passwordValue = "1234";

            Action act = () => new Password(passwordValue);

            ValidateException exception = Assert.Throws<ValidateException>(act);

            Assert.Single(exception.Errors);
            Assert.Single(exception.Errors[0].errors);
            Assert.Equal(Errors.ValidationErrorKey.INVALID_FIELD, exception.Errors[0].errors[0]);
        }
    }
}
