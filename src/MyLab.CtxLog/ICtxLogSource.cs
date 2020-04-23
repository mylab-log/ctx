using System;
using MyLab.LogDsl;

namespace MyLab.CtxLog
{
    /// <summary>
    /// Provides abilities to create log with context data
    /// </summary>
    public interface ICtxLogSource
    {
        DslLogger CreateLogger(string categoryName);
        DslLogger CreateLogger(Type type);
        DslLogger CreateLogger<T>();
    }
}