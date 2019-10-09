using MachineLearning.Converters;
using MachineLearning.DataLayer;
using MachineLearning.LearnAlgorithms;
using MachineLearning.Models;
using MachineLearning.Services;
using System;
using System.Collections.Generic;

namespace MachineLearning
{
    internal class Program
    {
        /*
            todo: pls refactoring info about variables types for Id3Builder(), need remove inputInfo from id3Builder constructor args
        */
        private static int Main()
        {
            var data = new JsonTableProvider().GetTable("path");

            var inputInfo = new[]
            {
                new DecisionVariable("Outlook", 3, new[] { "Sunny", "Overcast", "Rain" }),
                new DecisionVariable("Temperature", 3, new[] { "Hot", "Mild", "Cool" }),
                new DecisionVariable("Humidity", 2, new[] { "High", "Normal" }),
                new DecisionVariable("Wind", 2, new[] { "Weak", "Strong" }),
            };
            var outputInfo = new DecisionVariable("Play Tennis", 2, new[] { "No", "Yes" });

            var service = new DecisionTreeService(
                new TreeInfo()
                {
                    Inputs = new[] { "Outlook", "Temperature", "Humidity", "Wind" },
                    Output = "Play Tennis"
                },
                new Codebook(data, new Dictionary<string, string[]>()
                {
                    { "Outlook", new[] { "Sunny", "Overcast", "Rain" } },
                    { "Temperature", new[] { "Hot", "Mild", "Cool" } },
                    { "Humidity", new[] { "High", "Normal" } },
                    { "Wind", new[] {  "Weak", "Strong"  } },
                    { "Play Tennis", new[] { "No", "Yes" } },
                }),
                new Id3Builder(inputInfo, outputInfo),
                new Logger.Logger()
            );

            service.GetDecision(new Dictionary<string, string> {
                    { "Outlook", "Overcast" },
                    { "Temperature", "Hot" },
                    { "Humidity", "High" },
                    { "Wind", "Weak" },
                }
            );

            Console.ReadKey();
            return 0;
        }
    }
}