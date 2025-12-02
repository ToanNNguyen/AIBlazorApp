using ApiAiBlazorLab;
using Xunit;

public class TextUtilitiesTests
{
    [Fact]
    public void NormalizeFact_NullInput_ReturnsDefaultMessage()
    {
        // Arrange
        string? input = null;

        // Act
        string result = TextUtilities.NormalizeFact(input);

        // Assert
        Assert.Equal("No fact available.", result);
    }

    [Fact]
    public void NormalizeFact_EmptyString_ReturnsDefaultMessage()
    {
        // Arrange
        string input = "";

        // Act
        string result = TextUtilities.NormalizeFact(input);

        // Assert
        Assert.Equal("No fact available.", result);
    }

    [Fact]
    public void NormalizeFact_MissingPeriod_AddsPeriod()
    {
        // Arrange
        string input = "Cats sleep a lot";

        // Act
        string result = TextUtilities.NormalizeFact(input);

        // Assert
        Assert.Equal("Cats sleep a lot.", result);
    }

    [Fact]
    public void NormalizeFact_ExistingPeriod_KeepsSinglePeriod()
    {
        // Arrange
        string input = "Cats sleep a lot.";

        // Act
        string result = TextUtilities.NormalizeFact(input);

        // Assert
        Assert.Equal("Cats sleep a lot.", result);
    }

    [Fact]
    public void NormalizeFact_ExtraWhitespace_TrimsAndAddsPeriodIfNeeded()
    {
        // Arrange
        string input = "   Cats sleep a lot   ";

        // Act
        string result = TextUtilities.NormalizeFact(input);

        // Assert
        Assert.Equal("Cats sleep a lot.", result);
    }
}

