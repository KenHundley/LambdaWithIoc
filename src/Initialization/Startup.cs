using System.IO;
using Amazon.S3;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GenericProcessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using S3Processor;

namespace Initialization
{
    public static class Startup
    {
        public static IHost Host { get; }

        static Startup()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddEnvironmentVariables();
                    config.AddJsonFile("appsettings.json");
                })
                .ConfigureAppConfiguration((context, config) => { context.HostingEnvironment.ApplicationName = "LambdaWithStartup"; })
                .UseEnvironment(EnvironmentName.Development)
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging();
                    services.Configure<StringFunctionsConfiguration>(context.Configuration.GetSection("StringFunctionsConfiguration"));
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    builder.RegisterType<AmazonS3Client>().As<IAmazonS3>();
                    builder.RegisterType<S3Functions>().AsSelf();
                    builder.RegisterType<StringFunctions>().AsSelf();
                });
            Host = hostBuilder.Build();
        }
    }
}
