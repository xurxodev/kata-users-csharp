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
            var user = new User("Jorge Sánchez", "xurxodev@gmail.com", "12345678A");

            Assert.NotNull(user);
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

        [Fact]
        public void return_be_equal_for_instances_with_the_same_property_values()
        {
            var guid = Guid.NewGuid();

            var user1 = new User("Jorge Sánchez", "xurxoexam@gmail.com", "12345678A", guid);
            var user2 = new User("Jorge Sánchez Fernandez", "xurxodev@gmail.com", "12345678A", guid);

            Assert.Equal(user1, user2);
        }

        [Fact]
        public void return_not_be_equal_for_instances_with_the_same_property_values()
        {

            var guid = Guid.NewGuid();

            var user1 = new User("Jorge Sánchez", "xurxoexam@gmail.com", "12345678A", Guid.NewGuid());
            var user2 = new User("Jorge Sánchez Fernandez", "xurxodev@gmail.com", "12345678A", Guid.NewGuid());

            Assert.NotEqual(user1, user2);
        }
    }
}
