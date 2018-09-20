using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GenericProcessor
{
    public class StringFunctions
    {
        private readonly ILogger<StringFunctions> logger;
        private readonly IOptions<StringFunctionsConfiguration> config;

        public StringFunctions(ILogger<StringFunctions> logger, IOptions<StringFunctionsConfiguration> config)
        {
            this.logger = logger;
            this.config = config;
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<string> ToUpperFunctionHandlerAsync(string input, ILambdaContext context)
        {
            context.Logger.LogLine("Logged from Lambda Context");
            this.logger.LogInformation($"Logged from {this.logger.GetType()}");

            return Task.FromResult(input?.ToUpper());
        }

        public async Task<string> ToUpperFunctionWithDelayHandlerAsync(string input, ILambdaContext context)
        {
            //induces an arbitrary delay to prove that the tasks are awaited as expected
            await Task.Delay(TimeSpan.FromSeconds(5));

            context.Logger.LogLine("Logged from Lambda Context");
            this.logger.LogInformation($"Logged from {this.logger.GetType()}");

            return input?.ToUpper();
        }
    }

    public class StringFunctionsConfiguration
    {
        public string ApplicationName { get; set; }
    }
}
