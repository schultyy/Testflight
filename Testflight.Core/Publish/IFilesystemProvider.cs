namespace Testflight.Core.Publish
{
    public interface IFilesystemProvider
    {
        void Copy(string sourceDirectory, string destinationDirectory, string pattern);
    }
}
