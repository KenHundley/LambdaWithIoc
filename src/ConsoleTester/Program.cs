using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using LambdaEntry;

namespace ConsoleTester
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var x = new Functions();

            for (var i = 0; i < 10; i++)
            {
                const string input = "this should be uppercase";
                var result = await x.ToUpperFunctionHandlerAsync(input, new TestLambdaContext());

                if (result != input.ToUpper()) throw new Exception("Didn't work");
            }
            
        }
    }
}
