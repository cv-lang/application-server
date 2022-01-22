using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
//using Microsoft.Extensions.Logging.Configuration;

namespace Cvl.ApplicationServer.Logging.Logger
{
    public static class HierarchicalLoggerExtensions
    {
        public static ILoggingBuilder AddHierarchicalLogger(
        this ILoggingBuilder builder)
        {
            //builder.Services.AddScoped<RequestLoggerScope, RequestLoggerScope>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, HierarchicalLoggerProvider>());
            builder.Services.AddSingleton<LogManager>();

            //LoggerProviderOptions.RegisterProviderOptions
            //<ColorConsoleLoggerConfiguration, HierarchicalLoggerProvider>(builder.Services);


            return builder;
        }

        public static ILoggingBuilder AddHierarchicalLogger(
        this ILoggingBuilder builder,
        Action<ColorConsoleLoggerConfiguration> configure)
        {
            builder.AddHierarchicalLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
