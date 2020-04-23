using MyLab.LogDsl;

namespace MyLab.CtxLog
{
    /// <summary>
    /// Determines object which registers log data sources for integration
    /// </summary>
    public interface ILogCtxDataSourceRegistrar
    {
        /// <summary>
        /// Registers log data source
        /// </summary>
        void RegisterLogDataSource<T>() where T : class, ILogDataSource;
    }
}