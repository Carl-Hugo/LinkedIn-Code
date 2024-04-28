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
