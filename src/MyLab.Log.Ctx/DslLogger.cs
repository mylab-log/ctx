using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLab.Log.Dsl;

namespace MyLab.Log.Ctx
{
    class DslLogger : IDslLogger
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDslLogger _dsl;

        public DslLogger(IServiceProvider serviceProvider, ILogger innerLogger = null)
        {
            if(innerLogger == null)
                throw new InvalidOperationException("Built-In .NET core logging required");
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            _dsl = innerLogger.Dsl();
        }

        public DslExpression Debug(string message)
        {
            return ApplyExtensions(_dsl.Debug(message));
        }

        

        public DslExpression Action(string message)
        {
            return ApplyExtensions(_dsl.Action(message));
        }

        public DslExpression Warning(string message)
        {
            return ApplyExtensions(_dsl.Warning(message));
        }

        public DslExpression Error(string message)
        {
            return ApplyExtensions(_dsl.Error(message));
        }

        private DslExpression ApplyExtensions(DslExpression expr)
        {
            DslExpression resultExpr = expr;

            var extensions = _serviceProvider
                .GetServices(typeof(ILogContextExtension))
                .Cast<ILogContextExtension>()
                .ToArray();

            foreach (var extension in extensions)
            {
                resultExpr = extension.Apply(resultExpr);
            }

            return resultExpr;
        }
    }

    class DslLogger<TCategory> : DslLogger, IDslLogger<TCategory>
    {
        public DslLogger(IServiceProvider serviceProvider, ILogger<TCategory> innerLogger = null) : base(serviceProvider, innerLogger)
        {
            
        }
    }
}