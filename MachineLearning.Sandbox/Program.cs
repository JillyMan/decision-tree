using MachineLearning.Core.Logger;
using MachineLearning.DataLayer;
using MachineLearning.DecisionTree.Services;
using MachineLearning.Sandbox.Properties;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MachineLearning.Sandbox
{
	class Program
	{
		static readonly Logger Logger = new Logger();

		static int Main()
		{
			DeicionTreeServiceRun();
			return Console.ReadKey().KeyChar;		
		}

		static void DeicionTreeServiceRun()
		{
			Logger.Info("Deicision Service Run...");
			var csvProvider = new FromCsvTableProvider();
			var jsonProvider = new JsonTableProvider();

			var data = csvProvider.GetData(Resources.CsvLearnDataPath);
			var metaInfo = jsonProvider.GetData(Resources.MetaInfoOfLearnSet);
			var test_case = new Dictionary<string, string>(
						jsonProvider
						.GetData(Resources.TestCaseDecisionTree)
						.Select(x => new KeyValuePair<string, string>(x.Key, x.Value[0])));

			var service = new DecisionTreeService(data, metaInfo, Logger);

			var error = service.CheckError();
			var result = error ? Constant.SUCCESS : Constant.FAIL;

			Logger.Info($"Run tree on learn input set: {result}");

			Logger.Info("Test case: ");
			foreach (var pair in test_case)
			{
				Logger.Info($"{pair.Key}: {pair.Value}");
			}

			var decision = service.GetDecision(test_case);
			Logger.Info($"Result: {decision}");
			Logger.Info("...Deicision Service Finish");
		}
	}
}