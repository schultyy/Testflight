namespace Testflight.Logging
{
    public interface ILogger
    {
        void Error(string errorMessage);
        void Info(string infoMessage);
    }

    public interface IFileLogger : ILogger
    {
        void WriteToFile(string filename);
    }

    public interface IDatabaseLogger : ILogger
    {
        void Save();
    }
}