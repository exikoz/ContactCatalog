using ContactCatalog.Validators;

namespace ContactCatalog.Tests;

public class EmailValidatorTests
{
    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("user.name@domain.se", true)]
    [InlineData("invalid-email", false)]
    [InlineData("@example.com", false)]
    [InlineData("", false)]
    public void IsValidEmail_ValidatesCorrectly(string email, bool expected)
    {
        // Act
        var result = EmailValidator.IsValidEmail(email);

        // Assert
        Assert.Equal(expected, result);
    }
}