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

        public Task<string> S3FunctionHandlerAsync(S3Event s3Event, ILambdaContext context)
        {
            return this.host.ExecuteWithScopeAsync<S3Functions, string>(function => function.S3FunctionHandlerAsync(s3Event, context));
        }

        public Task<string> ToUpperFunctionHandlerAsync(string input, ILambdaContext context)
        {
            return this.host.ExecuteWithScopeAsync<StringFunctions, string>(functions => functions.ToUpperFunctionHandlerAsync(input, context));
        }
		
        public Task<string> ToUpperFunctionWithDelayHandlerAsync(string input, ILambdaContext context)
        {
            return this.host.ExecuteWithScopeAsync<StringFunctions, string>(function => function.ToUpperFunctionWithDelayHandlerAsync(input, context));
        }
    }

    public static class HostExtensions
    {
        public static async Task<TR> ExecuteWithScopeAsync<TS, TR>(this IHost host, Func<TS, Task<TR>> predicate)
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<TS>();

                return await predicate.Invoke(service);
            }
        }
    }
}
