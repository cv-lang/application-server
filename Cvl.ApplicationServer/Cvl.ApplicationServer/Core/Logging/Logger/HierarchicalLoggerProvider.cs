using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Logging.Logger
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
