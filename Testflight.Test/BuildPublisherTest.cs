using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Testflight.Core;
using Testflight.Core.Publish;

namespace Testflight.Test
{
    [TestFixture]
    public class BuildPublisherTest
    {
        private Mock<IFilesystemProvider> filesystemProviderMock;

        private BuildPublisher publisher;

        const string packagesDirectory = "Packages";
        const string publishDirectory = "Publish";

        [SetUp]
        public void Setup()
        {
            filesystemProviderMock = new Mock<IFilesystemProvider>();
            publisher = new BuildPublisher(filesystemProviderMock.Object);
        }

        [Test]
        public void FilesytemProviderIsSet()
        {
            Assert.AreEqual(filesystemProviderMock.Object, publisher.FilesystemProvider);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyPackagesSourceDirectoryNull()
        {
            publisher.PublishPackages(null, "blah");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyPackagesDestinationDirectoryNull()
        {
            publisher.PublishPackages("blah", null);
        }

        [Test]
        public void CopyPackages()
        {
            publisher.PublishPackages(packagesDirectory, publishDirectory);

            filesystemProviderMock.Verify(c => c.Copy(It.Is<string>(s => s == packagesDirectory), It.Is<string>(s => s == publishDirectory), "*.*"));
        }

        [Test]
        public void CopyPackagesWithPattern()
        {
            var filePatterns = new[] { "*.exe", "*.dll", "*.xml" };
            publisher.PublishPackages(packagesDirectory, publishDirectory, filePatterns);

            foreach (var filePattern in filePatterns)
            {
                string pattern = filePattern;
                filesystemProviderMock.Verify(c => c.Copy(It.Is<string>(s => s == packagesDirectory), It.Is<string>(s => s == publishDirectory), It.Is<string>(s => s == pattern)));
            }
        }
    }
}
