using MachineLearning.Converters;
using MachineLearning.DataLayer;
using MachineLearning.Models;
using MachineLearning.Services;
using System;
using System.Collections.Generic;
using MachineLearning.LearnAlgorithm;
using System.Linq;

namespace MachineLearning
{
	internal class Program
	{
		/*
            todo: pls refactoring info about variables types for Id3Builder(), need remove inputInfo from id3Builder constructor args
			todo: pls Move Project To 'MachineLearnig.DecisionTree assembly'
        */
		private static int Main()
		{
			var csvProvider = new FromCsvTableProvider();
			var jsonProvider = new JsonTableProvider();

			var data = csvProvider.GetData(Properties.Resources.CsvLearnDataPath);
			var metaInfo = jsonProvider.GetData(Properties.Resources.MetaInfoOfLearnSet);

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
				new Codebook(data),
				new Id3Algorithm(inputInfo, outputInfo),
				new Logger.Logger()
			);

			//service.GetDecision(new Dictionary<string, string> {
			//		{ "Outlook", "Overcast" },
			//		{ "Temperature", "Hot" },
			//		{ "Humidity", "High" },
			//		{ "Wind", "Weak" },
			//	}
			//);

			var result = service.GetDecision(new Dictionary<string, string> {
					{ "x1", "0" },
					{ "x2", "1" },
				}
			);

			Console.ReadKey();
			return 0;
		}

		private static DecisionVariable[] ParseMetaInfo(IDictionary<string, string[]> metaInfo)
		{
			return metaInfo.Select(x => new DecisionVariable(x.Key, x.Value)).ToArray();
		}
	}
}