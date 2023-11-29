using Microsoft.Extensions.Logging;
using Moq;

public class AtmServiceTests
{
    private readonly AtmService atmService;
    private readonly Mock<ILogger<CustomerService>> loggerMock;

    public AtmServiceTests()
    {
        loggerMock = new Mock<ILogger<CustomerService>>();
        atmService = new AtmService(loggerMock.Object);
    }

    [Fact]
    public void CalculateOptionsPayOut_ValidAmount_ReturnsCorrectOptions()
    {
        // Arrange
        int amount = 120;

        // Act
        List<string> options = atmService.CalculateOptionsPayOut(amount);

        // Assert
        Assert.Equal(new List<string>
        {
            "10 X 12 EUR",
            "50 X 2 EUR + 10 X 2 EUR",
            "50 X 1 EUR + 10 X 7 EUR",
            "100 X 1 EUR + 10 X 2 EUR"
        }, options);
    }

    [Fact]
    public void CalculateOptionsPayOut_AmountLessThan10_ReturnsOnly10EuroOption()
    {
        // Arrange
        int amount = 5;

        // Act
        List<string> options = atmService.CalculateOptionsPayOut(amount);

        // Assert
        Assert.Equal(new List<string>
        {
            "10 X 0 EUR"
        }, options);
    }

    [Fact]
    public void Valid_ValidAmount_ReturnsNoError()
    {
        // Arrange
        int amount = 50;

        // Act
        List<string> errors = atmService.Valid(amount);

        // Assert
        Assert.Empty(errors);
    }

    [Fact]
    public void Valid_NegativeAmount_ReturnsError()
    {
        // Arrange
        int amount = -50;

        // Act
        List<string> errors = atmService.Valid(amount);

        // Assert
        Assert.Equal(new List<string> { "The number is negative or has values in the last space that are different from zero" }, errors);
    }

    [Fact]
    public void Valid_AmountWithNonZeroLastDigit_ReturnsError()
    {
        // Arrange
        int amount = 15;

        // Act
        List<string> errors = atmService.Valid(amount);

        // Assert
        Assert.Equal(new List<string> { "The number is negative or has values in the last space that are different from zero" }, errors);
    }
}
