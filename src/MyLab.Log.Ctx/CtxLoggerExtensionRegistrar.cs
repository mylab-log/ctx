using Microsoft.Extensions.DependencyInjection;

namespace MyLab.Log.Ctx
{
    /// <summary>
    /// Registers context logger extensions
    /// </summary>
    public class CtxLoggerExtensionRegistrar
    {
        private readonly IServiceCollection _serviceCollection;

        /// <summary>
        /// Initializes a new instance of <see cref="CtxLoggerExtensionRegistrar"/>
        /// </summary>
        public CtxLoggerExtensionRegistrar(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public CtxLoggerExtensionRegistrar Register<T>() where T : ILogContextExtension
        {
            _serviceCollection.AddScoped(typeof(ILogContextExtension), typeof(T));

            return this;
        }
    }
}