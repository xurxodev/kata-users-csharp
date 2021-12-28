using System;
using KataUsers.Domain.Entities;
using KataUsers.Domain.Types;
using Xunit;

namespace KataUsers.Tests.Domain.Entities
{
    public class UserShould
    {
        [Fact]
        public void not_return_errors_if_all_fields_are_valid()
        {
            var password = new User("Jorge Sánchez", "xurxodev@gmail.com", "12345678A");

            Assert.NotNull(password);
        }

        [Fact]
        public void return_field_cannot_be_blank_if_name_is_empty()
        {
            Action act = () => new User("", "xurxodev@gmail.com", "12345678A"); ;

            ValidateException exception = Assert.Throws<ValidateException>(act);

            Assert.Single(exception.Errors);
            Assert.Single(exception.Errors[0].errors);
            Assert.Equal("Name", exception.Errors[0].field);
            Assert.Equal(Errors.ValidationErrorKey.FIELD_CANNOT_BE_BLANK, exception.Errors[0].errors[0]);
        }

        [Fact]
        public void return_field_cannot_be_blank_if_email_is_empty()
        {
            Action act = () => new User("Jorge Sánchez", "", "12345678A"); ;

            ValidateException exception = Assert.Throws<ValidateException>(act);

            Assert.Single(exception.Errors);
            Assert.Single(exception.Errors[0].errors);
            Assert.Equal("Email", exception.Errors[0].field);
            Assert.Equal(Errors.ValidationErrorKey.FIELD_CANNOT_BE_BLANK, exception.Errors[0].errors[0]);
        }

        [Fact]
        public void return_field_cannot_be_blank_if_password_is_empty()
        {
            Action act = () => new User("Jorge Sánchez", "xurxodev@gmail.com", ""); ;

            ValidateException exception = Assert.Throws<ValidateException>(act);

            Assert.Single(exception.Errors);
            Assert.Single(exception.Errors[0].errors);
            Assert.Equal("Password", exception.Errors[0].field);
            Assert.Equal(Errors.ValidationErrorKey.FIELD_CANNOT_BE_BLANK, exception.Errors[0].errors[0]);
        }

        [Fact]
        public void return_invalid_field_if_email_is_not_valid()
        {
            Action act = () => new User("Jorge Sánchez", "xurxodevgmail.com", "12345678A"); ;

            ValidateException exception = Assert.Throws<ValidateException>(act);

            Assert.Single(exception.Errors);
            Assert.Single(exception.Errors[0].errors);
            Assert.Equal("Email", exception.Errors[0].field);
            Assert.Equal(Errors.ValidationErrorKey.INVALID_FIELD, exception.Errors[0].errors[0]);
        }

        [Fact]
        public void return_invalid_field_if_password_is_not_valid()
        {
            Action act = () => new User("Jorge Sánchez", "xurxodev@gmail.com", "1234"); ;

            ValidateException exception = Assert.Throws<ValidateException>(act);

            Assert.Single(exception.Errors);
            Assert.Single(exception.Errors[0].errors);
            Assert.Equal("Password", exception.Errors[0].field);
            Assert.Equal(Errors.ValidationErrorKey.INVALID_FIELD, exception.Errors[0].errors[0]);
        }
    }
}
