using System;
using KataUsers.Domain.Types;
using KataUsers.Domain.Utils;
using KataUsers.Domain.ValueObjects;
using Xunit;

namespace KataUsers.Tests.Domain.ValueObjects
{
    public class EmailShould
    {
        [Fact]
        public void return_email_if_value_is_valid()
        {
            var emailValue = "xurxodev@gmail.com";
            var email = new Email(emailValue);

            Assert.NotNull(email);
        }

        [Fact]
        public void return_field_cannot_be_blank_if_value_is_empty()
        {
            var emailValue = "";

            Action act = () => new Email(emailValue);

            ValidateException exception = Assert.Throws<ValidateException>(act);

            Assert.Single(exception.Errors);
            Assert.Single(exception.Errors[0].errors);
            Assert.Equal(Errors.ValidationErrorKey.FIELD_CANNOT_BE_BLANK, exception.Errors[0].errors[0]);
        }


        [Fact]
        public void return_invalid_field_if_value_is_not_valid()
        {
            var emailValue = "xurxodevgmail.com";

            Action act = () => new Email(emailValue);

            ValidateException exception = Assert.Throws<ValidateException>(act);

            Assert.Single(exception.Errors);
            Assert.Single(exception.Errors[0].errors);
            Assert.Equal(Errors.ValidationErrorKey.INVALID_FIELD, exception.Errors[0].errors[0]);
        }

        [Fact]
        public void return_be_equal_for_instances_with_the_same_property_values()
        {

            var email1 = new Email("xurxodev@gmail.com");
            var email2 = new Email("xurxodev@gmail.com");

            Assert.Equal(email1, email2);
        }


        [Fact]
        public void return_not_be_equal_for_instances_with_the_same_property_values()
        {

            var email1 = new Email("xurxodev@gmail.com");
            var email2 = new Email("xurxodev@eample.com");

            Assert.NotEqual(email1, email2);
        }
    }
}
