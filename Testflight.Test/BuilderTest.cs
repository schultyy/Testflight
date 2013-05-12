using System;
using Moq;
using NUnit.Framework;
using Testflight.Shared;
using Testflight.Core;
using Testflight.Core.Build;
using Testflight.Logging;

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
                .Returns(() => new BuildResult()
                                   {
                                       TargetResults = new ITargetResult[] { }
                                   });

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
                                       TargetResults = new ITargetResult[]
                                                           {
                                                               new TargetResult(){Component = "Build"}
                                                           }
                                   });

            #endregion

            builder.Run("Test.sln");

            loggerMock.Verify(c => c.Info(It.Is<string>(s => s == "Build"), It.IsAny<string>()));
            loggerMock.Verify(c => c.Error(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void LogStdErrAfterRun()
        {
            #region Preface

            builderMock.Setup(c => c.Call(It.IsAny<string>(), It.IsAny<BuildConfiguration>()))
                .Returns(() => new BuildResult
                                   {
                                       TargetResults = new ITargetResult[]
                                                           {
                                                               new TargetResult()
                                                               {
                                                                   Result = ResultCode.Failure,
                                                                   Component = "Build",
                                                                   Exception = new Exception("Failure")
                                                               }
                                                           }
                                   });

            #endregion

            builder.Run("Test.sln");

            loggerMock.Verify(c => c.Error(It.Is<string>(s => s == "Build"), It.IsAny<Exception>()));
        }

        [Test]
        public void ExitCodeSuccess()
        {
            builderMock.Setup(c => c.Call(It.IsAny<string>(), It.IsAny<BuildConfiguration>()))
                .Returns(() => new BuildResult
                                   {
                                       ExitCode = ResultCode.Success,
                                       TargetResults = new ITargetResult[]
                                                           {
                                                               new TargetResult
                                                                   {
                                                                       Component = "Build",
                                                                       Result = ResultCode.Success
                                                                   }
                                                           }
                                   });

            Assert.IsTrue(builder.Run("Test.sln"));
        }

        [Test]
        public void ExitCodeFailure()
        {
            builderMock.Setup(c => c.Call(It.IsAny<string>(), It.IsAny<BuildConfiguration>()))
                .Returns(() => new BuildResult
                {
                    ExitCode = ResultCode.Failure,
                    TargetResults = new ITargetResult[]
                                        {
                                            new TargetResult
                                                {
                                                    Exception = new Exception(),
                                                    Result = ResultCode.Failure,
                                                    Component = "Build"
                                                }
                                        }
                });

            Assert.IsFalse(builder.Run("Test.sln"));
        }
    }
}
