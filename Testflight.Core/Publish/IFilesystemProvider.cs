using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testflight.Core
{
    public interface IFilesystemProvider
    {
        void Copy(string sourceDirectory, string destinationDirectory, string pattern);
    }
}
