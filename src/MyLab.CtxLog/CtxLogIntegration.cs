using System;
using Microsoft.Extensions.DependencyInjection;

namespace MyLab.CtxLog
{
    /// <summary>
    /// Contains methods to integrate context logging
    /// </summary>
    public static class CtxLogIntegration
    {
        /// <summary>
        /// Integrates context logging 
        /// </summary>
        public static IServiceCollection AddCtxLogging(
            this IServiceCollection serviceCollection, 
            Action<ILogCtxDataSourceRegistrar> ctxLogSourcesRegistration)
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));
            if (ctxLogSourcesRegistration == null) throw new ArgumentNullException(nameof(ctxLogSourcesRegistration));

            var resourceRegistry = new DefaultLogCtxDataSourceRegistry();

            ctxLogSourcesRegistration(resourceRegistry);

            return serviceCollection
                .AddSingleton<ILogCtxDataSourceFactory>(resourceRegistry)
                .AddHttpContextAccessor()
                .AddSingleton<ICtxLogSource, DefaultCtxLogSource>();
        }
    }
}
