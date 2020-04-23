using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MyLab.LogDsl;

namespace TestServer
{
    public class CtxHeaderLogDataSource : ILogDataSource
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CtxHeaderLogDataSource(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddLogData(DslLogEntityBuilder builder)
        {
            builder.AndFactIs("ctx", _httpContextAccessor.HttpContext.Request.Headers["X-Context"].First());
        }
    }
}
