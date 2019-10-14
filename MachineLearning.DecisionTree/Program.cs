using MachineLearning.Core.Converters;
using MachineLearning.Core.Logger;
using MachineLearning.Core.Properties;
using MachineLearning.DataLayer;
using MachineLearning.DecisionTree.LearnAlgorithm;
using MachineLearning.DecisionTree.Models;
using MachineLearning.DecisionTree.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.DecisionTree
{
	internal class Program
	{
		private static int Main()
		{
			var csvProvider = new FromCsvTableProvider();
			var jsonProvider = new JsonTableProvider();

			var data = csvProvider.GetData(Resources.CsvLearnDataPath);
			var metaInfo = jsonProvider.GetData(Resources.MetaInfoOfLearnSet);

			var vars = ParseMetaInfo(metaInfo);
			var inputInfo = vars.Take(vars.Length - 1).ToArray();
			var outputInfo = vars.Last();

			var list = inputInfo.Select(x => new KeyValuePair<string, string[]>(x.Name, x.NameRange)).ToList();
			list.Add(new KeyValuePair<string, string[]>(outputInfo.Name, outputInfo.NameRange));

			var service = new DecisionTreeService(
				new TreeInfo()
				{
					Inputs = inputInfo.Select(x => x.Name).ToArray(),
					Output = outputInfo.Name
				},
				new Codebook(data, metaInfo),
				new Id3Algorithm(inputInfo, outputInfo),
				new Logger()
			);

			var error = service.CheckError();
			var result = error ? "Success" : "Fail";

			Console.WriteLine($"Tree learn: {result}");

			#region test
			//var decision = service.GetDecision(new Dictionary<string, string> {
			//		{ "Outlook", "Overcast" },
			//		{ "Temperature", "Hot" },
			//		{ "Humidity", "High" },
			//		{ "Wind", "Weak" },
			//	}
			//);

			//var result = service.GetDecision(new Dictionary<string, string> {
			//		{ "x1", "0" },
			//		{ "x2", "1" },
			//	}
			//);
			#endregion
			Console.ReadKey();
			return 0;
		}

		private static DecisionVariable[] ParseMetaInfo(IDictionary<string, string[]> metaInfo)
		{
			return metaInfo.Select(x => new DecisionVariable(x.Key, x.Value)).ToArray();
		}
	}
}