using System;
using System.Collections.Generic;
using MyLab.LogDsl;

namespace MyLab.CtxLog
{
    /// <summary>
    /// Creates log context data sources
    /// </summary>
    public interface ILogCtxDataSourceFactory
    {
        /// <summary>
        /// Creates log context data sources
        /// </summary>
        IEnumerable<ILogDataSource> CreateDataSources(IServiceProvider serviceProvider);
    }
}