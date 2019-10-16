using System;

namespace MachineLearning.Core.Logger
{
    public class Logger : ILogger
    {
        public void Info(string message, bool newLine = true)
        {
            var info = Format($"Info: {message}");
            Log(message, newLine);
        }

        public void Log(string message, bool newLine = true)
        {
            if (newLine)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
        }

        private static string Format(string message)
        {
            var dateString = DateTime.Now.ToString();
            return $"{dateString}: {message}";
        }
    }
}
