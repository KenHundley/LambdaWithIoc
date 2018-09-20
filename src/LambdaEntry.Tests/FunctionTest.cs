using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using Xunit;

namespace LambdaEntry.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Functions();
            var context = new TestLambdaContext();
            var upperCase = await function.ToUpperFunctionHandlerAsync("hello world", context);

            Assert.Equal("HELLO WORLD", upperCase);
        }
    }
}
