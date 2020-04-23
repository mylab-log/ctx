using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MyLab.Logging;
using Newtonsoft.Json;
using TestServer;
using Xunit;
using Xunit.Abstractions;

namespace FuncTests
{
    public class ContextLoggingBehavior : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _clientFactory;
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Initializes a new instance of <see cref="ContextLoggingBehavior"/>
        /// </summary>
        public ContextLoggingBehavior(WebApplicationFactory<Startup> clientFactory, ITestOutputHelper output)
        {
            _clientFactory = clientFactory;
            _output = output;
        }

        [Fact]
        public async Task ShouldProvideRequestContext()
        {
            //Arrange
            var client = _clientFactory.CreateClient();
            
            //Act
            var remoteCtx = await GetServerLogCtxAsync(client, "test/log", "foo");

            //Assert
            Assert.NotNull(remoteCtx);
            Assert.Equal("foo", remoteCtx);
        }

        [Fact]
        public async Task ShouldProvideCtxFromDifferentRequests()
        {
            //Arrange
            var client = _clientFactory.CreateClient();

            //Act
            var remoteCtx1 = await GetServerLogCtxAsync(client, "test/log-from-singleton", "foo");
            var remoteCtx2 = await GetServerLogCtxAsync(client, "test/log-from-singleton", "bar");

            //Assert
            Assert.NotNull(remoteCtx1);
            Assert.Equal("foo", remoteCtx1);
            Assert.NotNull(remoteCtx2);
            Assert.Equal("bar", remoteCtx2);
        }

        async Task<string> GetServerLogCtxAsync(HttpClient client, string path, string context)
        {
            client.DefaultRequestHeaders.Remove("X-Context");
            client.DefaultRequestHeaders.Add("X-Context", context);

            var resp = await client.GetAsync(path);
            var restStr = await resp.Content.ReadAsStringAsync();

            _output.WriteLine(restStr);

            resp.EnsureSuccessStatusCode();

            var logEntity =  JsonConvert.DeserializeObject<LogEntity>(restStr);
            return logEntity.Attributes?.FirstOrDefault(a => a.Name == "ctx")?.Value.ToString();
        }
    }
}
