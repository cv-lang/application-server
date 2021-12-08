using Cvl.ApplicationServer.Core.Model.Temporary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Logging.Logger
{

    public class ColorConsoleLoggerConfiguration
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new()
        {
            [LogLevel.Information] = ConsoleColor.Green,
            [LogLevel.Warning] = ConsoleColor.Magenta
        };
    }



    internal class HierarchicalLogger : ILogger, IDisposable
    {
        private Guid id = Guid.NewGuid();
        private readonly string _name;
        private readonly Func<ColorConsoleLoggerConfiguration> _getCurrentConfig;
        private readonly IServiceProvider serviceProvider;

        public HierarchicalLogger(
            string name,
            Func<ColorConsoleLoggerConfiguration> getCurrentConfig,
            IServiceProvider serviceProvider
            )
        {
            (_name, _getCurrentConfig) = (name, getCurrentConfig);
            this.serviceProvider = serviceProvider;
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) =>
            _getCurrentConfig().LogLevels.ContainsKey(logLevel);

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            
            var log = new LogElement(DateTime.Now, logLevel, "", "_name", state.ToString(), formatter(state, exception));

            var requestLoggerScope = serviceProvider.GetRequiredService<RequestLoggerScope>();
            requestLoggerScope.AddLog(log);

            ColorConsoleLoggerConfiguration config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                ConsoleColor originalColor = Console.ForegroundColor;

                Console.ForegroundColor = config.LogLevels[logLevel];
                Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

                Console.ForegroundColor = originalColor;
                Console.WriteLine($"     {_name} - {formatter(state, exception)}");
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
