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
        
        static readonly FromCsvTableProvider CsvProvider = new FromCsvTableProvider();

        static readonly JsonTableProvider JsonProvider = new JsonTableProvider();

        static int Main()
		{
            RunSerivce(Resources.CarDataCsv, Resources.CarAttrMetaInfo, "Car Evaluation Service");
            DeicionTreeServiceRun();
            return Console.ReadKey().KeyChar;		
		}

        static IDecisionService RunSerivce(string dataPath, string metaInfoPath, string nameTest)
        {
            Logger.Info($"Start... ({nameTest})");
            var data = CsvProvider.GetData(dataPath);
            var metaInfo = JsonProvider.GetData(metaInfoPath);

            var service = new DecisionTreeService(data, metaInfo, Logger);
            var error = service.CheckError();
            var result = error ? Constant.SUCCESS : Constant.FAIL;
            Logger.Info($"Run tree on learn input set: {result}");
            Logger.Info("End...");
            Logger.Info("-----------------");

            service.DumpTree();

            return service;
        }

        static void DeicionTreeServiceRun()
		{
			Logger.Info("Deicision Service Run...");

			var data = CsvProvider.GetData(Resources.CsvLearnDataPath);
			var metaInfo = JsonProvider.GetData(Resources.MetaInfoOfLearnSet);
			var test_case = new Dictionary<string, string>(
						JsonProvider
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
            service.DumpTree();

            Logger.Info("...Deicision Service Finish");
        }
    }
}