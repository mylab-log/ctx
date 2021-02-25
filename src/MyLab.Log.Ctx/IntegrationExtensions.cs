using System;
using Microsoft.Extensions.DependencyInjection;
using MyLab.Log.Dsl;

namespace MyLab.Log.Ctx
{
    /// <summary>
    /// Contains extension methods for integration
    /// </summary>
    public static class IntegrationExtensions
    {
        public static IServiceCollection AddLogCtx(this IServiceCollection services, Action<CtxLoggerExtensionRegistrar> extensionRegistration = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            extensionRegistration?.Invoke(new CtxLoggerExtensionRegistrar(services));

            return services.AddScoped<IDslLogger, DslLogger>()
                .AddScoped(typeof(IDslLogger<>), typeof(DslLogger<>));
        }
    }
}
