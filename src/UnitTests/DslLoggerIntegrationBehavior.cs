using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLab.Log.Ctx;
using MyLab.Log.Dsl;
using Xunit;

namespace UnitTests
{
    public class DslLoggerIntegrationBehavior
    {
        [Fact]
        public void ShouldProvideLogger()
        {
            //Arrange
            var sp = new ServiceCollection()
                .AddLogging()
                .AddSingleton<ILogger, TestLogger>()
                .AddLogCtx()
                .BuildServiceProvider();
            
            //Act
            var logger = sp.GetService<IDslLogger>();

            //Assert
            Assert.NotNull(logger);
        }

        [Fact]
        public void ShouldProvideLoggerWithCategory()
        {
            //Arrange
            var sp = new ServiceCollection()
                .AddLogging()
                .AddLogCtx()
                .BuildServiceProvider();

            //Act
            var logger = sp.GetService<IDslLogger<DslLoggerIntegrationBehavior>>();

            //Assert
            Assert.NotNull(logger);
        }

        [Fact]
        public void ShouldFailIfLoggingIsAbsent()
        {
            //Arrange
            var sp = new ServiceCollection()
                //.AddLogging()
                .AddLogCtx()
                .BuildServiceProvider();

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => sp.GetService<IDslLogger<DslLoggerIntegrationBehavior>>());
        }

        [Fact]
        public void ShouldProvideRealLogger()
        {
            //Arrange
            var sb = new StringBuilder();

            var sp = new ServiceCollection()
                .AddLogging(lb => lb.AddProvider(new MemoryLoggerProvider(sb)))
                .AddLogCtx()
                .BuildServiceProvider();

            var logger = (DslLogger<DslLoggerIntegrationBehavior>)sp.GetService<IDslLogger<DslLoggerIntegrationBehavior>>();

            //Act
            logger.Action("Foo").Write();

            //Assert
            Assert.Contains(sb.ToString().Split(Environment.NewLine), s => s.Trim() == "Message: Foo");
        }

        class TestLogger : ILogger
        {
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                throw new NotImplementedException();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                throw new NotImplementedException();
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }
        }
    }
}
