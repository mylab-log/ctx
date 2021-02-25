using MyLab.Log.Dsl;

namespace MyLab.Log.Ctx
{
    /// <summary>
    /// Describes extension for logging which contains some context related log data
    /// </summary>
    public interface ILogContextExtension
    {
        /// <summary>
        /// Applies context data to <see cref="IDslLogger"/>
        /// </summary>
        DslExpression Apply(DslExpression dslExpression);
    }
}
