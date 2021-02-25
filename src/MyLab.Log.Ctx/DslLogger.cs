using System;
using Microsoft.Extensions.Logging;
using MyLab.Log.Dsl;

namespace MyLab.Log.Ctx
{
    class DslLogger : IDslLogger
    {
        private readonly IDslLogger _dsl;

        public DslLogger(ILogger innerLogger = null)
        {
            if(innerLogger == null)
                throw new InvalidOperationException("Built-In .NET core logging required");
            
            _dsl = innerLogger.Dsl();
        }

        public DslExpression Debug(string message)
        {
            return _dsl.Debug(message);
        }

        public DslExpression Action(string message)
        {
            return _dsl.Action(message);
        }

        public DslExpression Warning(string message)
        {
            return _dsl.Warning(message);
        }

        public DslExpression Error(string message)
        {
            return _dsl.Error(message);
        }
    }

    class DslLogger<TCategory> : DslLogger, IDslLogger<TCategory>
    {
        public DslLogger(ILogger<TCategory> innerLogger = null) : base(innerLogger)
        {
            
        }
    }
}