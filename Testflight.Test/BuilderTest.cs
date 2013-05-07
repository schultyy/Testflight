using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using TestFlight.Shared;
using Testflight.Core;
using Testflight.Core.Build;

namespace Testflight.Test
{
    [TestFixture]
    public class BuilderTest
    {
        private Builder builder;

        private Mock<ILogger> loggerMock;

        private Mock<IBuilderCapability> builderMock;

        [SetUp]
        public void Setup()
        {
            loggerMock = new Mock<ILogger>();
            builderMock = new Mock<IBuilderCapability>();

            builderMock.Setup(c => c.Call(It.IsAny<string>(), It.IsAny<BuildConfiguration>()))
                .Returns(() => new BuildResult());

            builder = new Builder(loggerMock.Object, builderMock.Object);
        }

        [Test]
        public void LoggerIsSet()
        {
            Assert.AreEqual(loggerMock.Object, builder.Logger);
        }

        [Test]
        public void BuilderCapabilityIsSet()
        {
            Assert.AreEqual(builderMock.Object, builder.BuilderCapability);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunBuilderSolutionFileNull()
        {
            builder.Run(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunBuilderTargetNull()
        {
            builder.Run("blah", BuildConfiguration.Release, null);
        }

        [Test]
        public void MSBuildIsCalledOnRun()
        {
            builder.Run("Test.sln");

            builderMock.Verify(c => c.Call(It.Is<string>(sln => sln == "Test.sln"),
                                           It.Is<BuildConfiguration>(bc => bc == BuildConfiguration.Release)));
        }

        [Test]
        public void LogStdoutAfterRun()
        {
            #region Preface

            builderMock.Setup(c => c.Call(It.IsAny<string>(), It.IsAny<BuildConfiguration>()))
                .Returns(() => new BuildResult
                                   {
                                       StdOut = "Stdout"
                                   });

            #endregion

            builder.Run("Test.sln");

            loggerMock.Verify(c => c.Info(It.Is<string>(s => s == "Stdout")));
            loggerMock.Verify(c => c.Error(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void LogStdErrAfterRun()
        {
            #region Preface

            builderMock.Setup(c => c.Call(It.IsAny<string>(), It.IsAny<BuildConfiguration>()))
                .Returns(() => new BuildResult
                                   {
                                       StdError = "Error"
                                   });

            #endregion

            builder.Run("Test.sln");

            loggerMock.Verify(c => c.Error(It.Is<string>(s => s == "Error")));
        }

        [Test]
        public void ExitCodeSuccess()
        {
            builderMock.Setup(c => c.Call(It.IsAny<string>(), It.IsAny<BuildConfiguration>()))
                .Returns(() => new BuildResult
                                   {
                                       ExitCode = 0
                                   });

            Assert.IsTrue(builder.Run("Test.sln"));
        }

        [Test]
        public void ExitCodeFailure()
        {
            builderMock.Setup(c => c.Call(It.IsAny<string>(), It.IsAny<BuildConfiguration>()))
                .Returns(() => new BuildResult
                {
                    ExitCode = 5
                });

            Assert.IsFalse(builder.Run("Test.sln"));
        }
    }
}
