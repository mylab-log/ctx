using System;
using System.Collections.Generic;
using System.Text;
using MyLab.LogDsl;

namespace MyLab.CtxLog
{
    class CtxLogDataSource : ILogDataSource
    {
        private readonly ILogCtxDataSourceFactory _dataSourceFactory;
        private readonly IServiceProvider _serviceProvider;

        public CtxLogDataSource(
            ILogCtxDataSourceFactory dataSourceFactory,
            IServiceProvider serviceProvider)
        {
            _dataSourceFactory = dataSourceFactory;
            _serviceProvider = serviceProvider;
        }
        public void AddLogData(DslLogEntityBuilder builder)
        {
            var sources = _dataSourceFactory.CreateDataSources(_serviceProvider);
            foreach (var source in sources)
            {
                source.AddLogData(builder);
            }
        }
    }
}
