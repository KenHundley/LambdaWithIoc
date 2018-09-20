using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using GenericProcessor;
using Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using S3Processor;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaEntry
{
    public class Functions
    {
        private readonly IHost host;

        public Functions() : this(Startup.Host) { }

        public Functions(IHost host)
        {
            this.host = host;
        }

        public Task<string> S3FunctionHandlerAsync(S3Event s3Event, ILambdaContext context) => ExecuteWithScopeAsync<S3Functions, string>(function => function.S3FunctionHandlerAsync(s3Event, context));

        public Task<string> ToUpperFunctionHandlerAsync(string input, ILambdaContext context) => ExecuteWithScopeAsync<StringFunctions, string>(function => function.ToUpperFunctionHandlerAsync(input, context));
        
        private async Task<TR> ExecuteWithScopeAsync<TS, TR>(Func<TS, Task<TR>> predicate)
        {
            using (var scope = this.host.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<TS>();

                return await predicate.Invoke(service);
            }
        }
    }
}
