using Microsoft.Extensions.Logging;

namespace Cvl.ApplicationServer.Core.Logging.Logger
{
    public class HierarchicalLoggerProvider : ILoggerProvider
    {
        private Guid _id= Guid.NewGuid();
        private readonly IDisposable _onChangeToken;
        private ColorConsoleLoggerConfiguration _currentConfig;
        //private readonly ConcurrentDictionary<string, HierarchicalLogger> _loggers = new();
        private readonly IServiceProvider _serviceProvider;
        private readonly LogManager logManager;

        //public HierarchicalLoggerProvider(
        //    IOptionsMonitor<ColorConsoleLoggerConfiguration> config
        //    , RequestLoggerScope requestLoggerScope)
        //{
        //    _currentConfig = config.CurrentValue;
        //    _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        //    //_serviceProvider = serviceProvider;
        //}

        public HierarchicalLoggerProvider(
            //IOptionsMonitor<ColorConsoleLoggerConfiguration> config
            //,
            LogManager logManager
            )
        {
            //_currentConfig = config.CurrentValue;
            //_onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
            this.logManager = logManager;
            //_serviceProvider = serviceProvider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            logManager.Test();
            return new HierarchicalLogger(categoryName, GetCurrentConfig);
            //return _loggers.GetOrAdd(categoryName, name => new HierarchicalLogger(name, GetCurrentConfig));
        }

        private ColorConsoleLoggerConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            //_loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
