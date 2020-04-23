using MyLab.CtxLog;
using MyLab.LogDsl;
using MyLab.Logging;

namespace TestServer
{
    public class SingletonService
    {
        private readonly ICtxLogSource _ctxLogSource;

        public SingletonService(ICtxLogSource ctxLogSource)
        {
            _ctxLogSource = ctxLogSource;
        }

        public LogEntity GetLog()
        {
            return _ctxLogSource.CreateLogger<SingletonService>().Act("Test").Create();
        }
    }
}
