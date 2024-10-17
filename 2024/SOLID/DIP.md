```csharp
// Unit test example
using Moq;
using Xunit;


public class CustomerServiceTests
{
    public class RegisterCustomer : CustomerServiceTests
    {
        [Fact]
        public void Should_call_the_Add_method()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var customerService = new CustomerService(
                customerRepositoryMock.Object);
            var customerToRegister = new Customer {
                Name = "John Doe",
                Id = 1
            };


            // Act
            customerService.RegisterCustomer(customerToRegister);


            // Assert
            customerRepositoryMock.Verify(
                mock => mock.Add(customerToRegister),
                Times.Once()
            );
        }
    }
}
```

The preceding unit test depicts the benefits of the DIP by demonstrating how it enables the creation of modular and testable code. By depending only on the `ICustomerRepository` interface, we can use a mock object to test the `CustomerService` class in isolation. This allows our test to verify that `CustomerService` correctly calls the `Add` method without requiring an actual database.
