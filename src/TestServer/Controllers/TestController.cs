using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLab.CtxLog;
using MyLab.LogDsl;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly SingletonService _singletonService;
        private readonly DslLogger _log;

        /// <summary>
        /// Initializes a new instance of <see cref="TestController"/>
        /// </summary>
        public TestController(ICtxLogSource logSource, SingletonService singletonService)
        {
            _singletonService = singletonService;
            _log = logSource.CreateLogger<TestController>();
        }

        [HttpGet("log")]
        public IActionResult GetLog()
        {
            return Ok(_log.Act("Some thing done").Create());
        }

        [HttpGet("log-from-singleton")]
        public IActionResult GetLogFromSingleton()
        {
            return Ok(_singletonService.GetLog());
        }
    }
}
