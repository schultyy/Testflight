namespace Testflight.Core
{
    public interface ILogger
    {
        void Error(string errorMessage);
        void Info(string infoMessage);
        void WriteToFile(string filename);
    }
}