using System;

namespace MachineLearning.DecisionTree.Logger
{
    public class Logger : ILogger
    {
        public void Info(string message)
        {
            var info = Format($"Info: {message}");
            Console.WriteLine(info);
        }

        private static string Format(string message)
        {
            var dateString = DateTime.Now.ToString();
            return $"{dateString}: {message}";
        }
    }
}
