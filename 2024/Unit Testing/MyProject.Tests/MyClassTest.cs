namespace MyProject;

// Using xUnit for unit testing MyClass
public class MyClassTest
{
    // Regroup all tests of MyMethod here
    public class MyMethod
    {
        // Ensure the method returns true
        [Fact]
        public void Should_return_true()
        {
            // Arrange
            var myClass = new MyClass();
            var expected = true;

            // Act
            var result = myClass.MyMethod();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}