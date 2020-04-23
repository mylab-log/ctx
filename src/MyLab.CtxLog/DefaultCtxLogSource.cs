using System;
using Microsoft.Extensions.Logging;
using MyLab.LogDsl;

namespace MyLab.CtxLog
{
    class DefaultCtxLogSource : ICtxLogSource
    {
        private readonly ILogCtxDataSourceFactory _dataSourceFactory;
        private readonly ILoggerFactory _loggingFactory;
        private readonly IServiceProvider _serviceProvider;

        public DefaultCtxLogSource(
            ILogCtxDataSourceFactory dataSourceFactory,
            ILoggerFactory loggingFactory,
            IServiceProvider serviceProvider)
        {
            _dataSourceFactory = dataSourceFactory ?? throw new ArgumentNullException(nameof(dataSourceFactory));
            _loggingFactory = loggingFactory ?? throw new ArgumentNullException(nameof(loggingFactory));
            _serviceProvider = serviceProvider;
        }

        public DslLogger CreateLogger(string categoryName)
        {
            return new DslLogger(
                _loggingFactory.CreateLogger(categoryName),
                CreateDataSource());
        }

        public DslLogger CreateLogger(Type type)
        {
            return new DslLogger(
                _loggingFactory.CreateLogger(type),
                CreateDataSource());
        }

        public DslLogger CreateLogger<T>()
        {
            return new DslLogger(
                _loggingFactory.CreateLogger<T>(),
                CreateDataSource());
        }

        ILogDataSource CreateDataSource()
        {
            return new CtxLogDataSource(_dataSourceFactory, _serviceProvider);
        }
    }
}