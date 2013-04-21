using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testflight.Core
{
    public class BuildPublisher
    {
        public BuildPublisher(IFilesystemProvider filesystemProvider)
        {
            this.FilesystemProvider = filesystemProvider;
        }

        public IFilesystemProvider FilesystemProvider { get; private set; }


        public void PublishPackages(string packagesDirectory, string publishDirectory, string[] patterns = null)
        {
            if (string.IsNullOrEmpty(packagesDirectory))
                throw new ArgumentNullException("packagesDirectory");

            if (string.IsNullOrEmpty(publishDirectory))
                throw new ArgumentNullException("publishDirectory");

            if (patterns == null)
                patterns = new[] { "*.*" };

            foreach (var pattern in patterns)
                FilesystemProvider.Copy(packagesDirectory, publishDirectory, pattern);
        }
    }
}
