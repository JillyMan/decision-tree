using MachineLearning.Core.Logger;
using MachineLearning.DataLayer;
using MachineLearning.DecisionTree.Services;
using MachineLearning.Sandbox.Properties;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MachineLearning.Sandbox
{
	internal class Program
	{
		private static int Main()
		{
			var csvProvider = new FromCsvTableProvider();
			var jsonProvider = new JsonTableProvider();

			var data = csvProvider.GetData(Resources.CsvLearnDataPath);
			var metaInfo = jsonProvider.GetData(Resources.MetaInfoOfLearnSet);
			var test_case = new Dictionary<string, string>(
						jsonProvider
						.GetData(Resources.TestCaseDecisionTree)
						.Select(x => new KeyValuePair<string, string>(x.Key, x.Value[0])));

			var logger = new Logger();
			var service = new DecisionTreeService(data, metaInfo, logger);

			var error = service.CheckError();
			var result = error ? "Success" : "Fail";

			logger.Info($"Tree learn: {result}");

			logger.Info("Test case: ");

			foreach (var pair in test_case)
			{
				logger.Info($"{pair.Key}: {pair.Key}");
			}

			var decision = service.GetDecision(test_case);
			logger.Info($"Result: {decision}");

			return Console.ReadKey().KeyChar;		
		}
	}
}