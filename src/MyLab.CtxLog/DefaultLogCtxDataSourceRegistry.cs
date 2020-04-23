using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MyLab.LogDsl;

namespace MyLab.CtxLog
{
    class DefaultLogCtxDataSourceRegistry : ILogCtxDataSourceRegistrar, ILogCtxDataSourceFactory
    {
        readonly List<Type> _dataSourceTypes = new List<Type>();

        public void RegisterLogDataSource<T>() where T : class, ILogDataSource
        {
            _dataSourceTypes.Add(typeof(T));
        }

        public IEnumerable<ILogDataSource> CreateDataSources(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            return _dataSourceTypes.Select(t => (ILogDataSource)ActivatorUtilities.CreateInstance(serviceProvider, t));
        }
    }
}