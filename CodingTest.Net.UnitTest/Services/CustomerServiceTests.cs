using CodingTest.Net;
using CodingTest.Net.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

public class CustomerServiceTests
{
    private readonly CustomerService customerService;
    private readonly Mock<ICustomerRepository> customerRepositoryMock;
    private readonly Mock<ILogger<CustomerService>> loggerMock;

    public CustomerServiceTests()
    {
        customerRepositoryMock = new Mock<ICustomerRepository>();
        loggerMock = new Mock<ILogger<CustomerService>>();
        customerService = new CustomerService(customerRepositoryMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task AddAsync_ValidCustomers_AddsCustomersToRepository()
    {
        // Arrange
        var customers = new[]
        {
            new CustomerEntity { Id = 1, FirstName = "John", LastName = "Doe", Age = 25 },
            new CustomerEntity { Id = 2, FirstName = "Jane", LastName = "Smith", Age = 30 }
        };

        // Act
        await customerService.AddAsync(customers);

        // Assert
        customerRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<CustomerEntity[]>()), Times.AtLeastOnce);
    }

    [Fact]
    public void Valid_ValidCustomers_ReturnsNoErrors()
    {
        // Arrange
        var customers = new[]
        {
            new CustomerEntity { Id = 1, FirstName = "John", LastName = "Doe", Age = 25 },
            new CustomerEntity { Id = 2, FirstName = "Jane", LastName = "Smith", Age = 30 }
        };

        // Act
        List<string> errors = customerService.Valid(customers);

        // Assert
        Assert.Empty(errors);
    }

    [Fact]
    public void Valid_InvalidCustomers_ReturnsErrors()
    {
        // Arrange
        var customers = new[]
        {
            new CustomerEntity { Id = 0, FirstName = "", LastName = "", Age = 18 },
            new CustomerEntity { Id = 1, FirstName = "John", LastName = "Doe", Age = 17 }
        };

        // Act
        List<string> errors = customerService.Valid(customers);

        // Assert
        Assert.Equal(new List<string>
        {
            "Age must be above 18"
        }, errors);
    }
}
