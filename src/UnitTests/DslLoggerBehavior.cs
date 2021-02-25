using System;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyLab.Log.Ctx;
using MyLab.Log.Dsl;
using Xunit;

namespace UnitTests
{
    public class DslLoggerBehavior
    {
        [Fact]
        public void ShouldUseExtensions()
        {
            //Arrange
            var sb = new StringBuilder();

            var sp = new ServiceCollection()
                .AddLogging(builder => builder.AddProvider(new MemoryLoggerProvider(sb)))
                .AddLogCtx(registrar => registrar
                    .Register<AddLabelFooCtxLogExtension>()
                    .Register<AddLabelBarCtxLogExtension>()
                )
                .BuildServiceProvider();

            var logger = sp.GetService<IDslLogger<DslLoggerBehavior>>();

            //Act
            logger.Action("Baz").Write();
            var logLines = sb
                .ToString()
                .Split(Environment.NewLine)
                .Select(s => s.Trim())
                .ToArray();

            //Assert
            Assert.Contains(logLines, s => s == "Message: Baz");
            Assert.Contains(logLines, s => s == "foo: true");
            Assert.Contains(logLines, s => s == "bar: true");
        }  

        class AddLabelFooCtxLogExtension : ILogContextExtension
        {
            public DslExpression Apply(DslExpression dslExpression)
            {
                return dslExpression.AndLabel("foo");
            }
        }

        class AddLabelBarCtxLogExtension : ILogContextExtension
        {
            public DslExpression Apply(DslExpression dslExpression)
            {
                return dslExpression.AndLabel("bar");
            }
        }
    }
}
