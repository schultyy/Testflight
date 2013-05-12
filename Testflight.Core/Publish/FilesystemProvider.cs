using System;
using System.IO;

namespace Testflight.Core.Publish
{
    public class FilesystemProvider : IFilesystemProvider
    {
        public void Copy(string sourceDirectory, string destinationDirectory, string pattern)
        {
            if (string.IsNullOrEmpty(sourceDirectory))
                throw new ArgumentNullException("sourceDirectory");

            if (string.IsNullOrEmpty(destinationDirectory))
                throw new ArgumentNullException("destinationDirectory");

            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentNullException("pattern");

            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                if (dirPath == destinationDirectory)
                    continue;
                Directory.CreateDirectory(dirPath.Replace(sourceDirectory, destinationDirectory));
            }


            //Copy all the files
            foreach (string newPath in Directory.GetFiles(sourceDirectory, pattern, SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourceDirectory, destinationDirectory));
        }
    }
}
