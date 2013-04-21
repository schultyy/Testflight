﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Testflight.Core;

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
            builder = new Builder(loggerMock.Object);
        }

        [Test]
        public void LoggerIsSet()
        {
            Assert.AreEqual(loggerMock.Object, builder.Logger);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunBuilderSolutionFileNull()
        {
            builder.Run(null);
        }

        [Test]
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
    }
}
