namespace MachineLearning.Core.Logger
{
    public interface ILogger
    {
        void Info(string message, bool newLine = true);

        void Log(string message, bool newLine = true);
    }
}
