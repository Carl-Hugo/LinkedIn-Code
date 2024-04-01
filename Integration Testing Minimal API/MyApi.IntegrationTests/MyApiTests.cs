using Microsoft.AspNetCore.Mvc.Testing;
namespace MyApi.IntegrationTests;
public class MyApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    public class Get(WebApplicationFactory<Program> factory) : MyApiTests(factory)
    {
        [Fact]
        public async Task Should_return_200_OK()
        {
            // Act
            var response = await _client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Should_return_the_expected_content()
        {
            // Arrange
            var expectedContent = "Hello World!";

            // Act
            var response = await _client.GetAsync("/");

            // Assert
            var actualContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(expectedContent, actualContent);
        }
    }
}