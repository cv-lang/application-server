using Cvl.ApplicationServer.Core.Model.Temporary;
using Microsoft.Extensions.Logging;

namespace Cvl.ApplicationServer.Core.Logging.Logger
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



    public class HierarchicalLogger : ILogger, IDisposable
    {
        private Guid id = Guid.NewGuid();
        private readonly string _name;
        private readonly Func<ColorConsoleLoggerConfiguration> _getCurrentConfig;
        
        public HierarchicalLogger(
            string name,
            Func<ColorConsoleLoggerConfiguration> getCurrentConfig
            
            )
        {
            (_name, _getCurrentConfig) = (name, getCurrentConfig);            
        }

        //private static AsyncLocal<ConcurrentStack<HierarchicalLoggerScope>> scopes 
        //    = new AsyncLocal<ConcurrentStack<HierarchicalLoggerScope>>() { Value = new ConcurrentStack<HierarchicalLoggerScope>()};

        public IDisposable BeginScope<TState>(TState state)
        {
            
            throw new NotImplementedException();
            return null;
        }

        public void EndScope()
        {            
        }

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

            
            var log = new LogElement(DateTime.Now, logLevel, "", "_name", state.ToString() ??"", formatter(state, exception));

            //var requestLoggerScope = serviceProvider.GetRequiredService<RequestLoggerScope>();
            //requestLoggerScope.AddLog(log);

            ColorConsoleLoggerConfiguration config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                ConsoleColor originalColor = Console.ForegroundColor;

                Console.ForegroundColor = config.LogLevels[logLevel];
                Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

                //if(scopes.Value.Count > 0)
                //{
                //    HierarchicalLoggerScope scope;
                //    if (scopes.Value.TryPop(out scope))
                //    {
                //        Console.WriteLine($"{scope}");
                //    }
                    
                //}
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
