namespace Testflight.Logging
{
    public interface IDatabaseLogger : ILogger
    {
        void Save();
    }
}