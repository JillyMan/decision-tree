using System;

namespace MachineLearning.Core.Logger
{
    public class Logger : ILogger
    {
        public void Info(string message, bool newLine = true)
        {
            Log(message, newLine);
		}

		public void Log(string message, bool newLine = true)
        {
            Console.Write($"{message + (newLine ? '\n' : ' ')}");
        }

        private static string Format(string message)
        {
            var dateString = DateTime.Now.ToString();
            return $"{dateString}: {message}";
        }
    }
}
