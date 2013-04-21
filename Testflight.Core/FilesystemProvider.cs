using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Testflight.Core
{
    public class FilesystemProvider : IFilesystemProvider
    {
        public void Copy(string sourceDirectory, string destinationDirectory)
        {
            if (string.IsNullOrEmpty(sourceDirectory))
                throw new ArgumentNullException("sourceDirectory");

            if (string.IsNullOrEmpty(destinationDirectory))
                throw new ArgumentNullException("destinationDirectory");

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourceDirectory, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourceDirectory, destinationDirectory));

            //Copy all the files
            foreach (string newPath in Directory.GetFiles(sourceDirectory, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourceDirectory, destinationDirectory));
        }
    }
}
