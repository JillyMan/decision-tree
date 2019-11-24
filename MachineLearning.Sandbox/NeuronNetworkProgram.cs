using MachineLearning.Core.Converters;
using MachineLearning.Core.Logger;
using MachineLearning.DataLayer;
using MachineLearning.NeuralNetwork;
using MachineLearning.NeuralNetwork.ActivateFunctions;
using MachineLearning.NeuralNetwork.LearnAlghoritm;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MachineLearning.Sandbox
{
	public class NeuronNetworkProgram
	{
		static readonly Logger Logger = new Logger();
		static readonly JsonProvider JsonProvider = new JsonProvider();
		static readonly FromCsvTableProvider CsvProvider = new FromCsvTableProvider();

		public static void Run()
		{
			var network = new NeuralNetwork.NeuralNetwork(
				new NetworkSettings(
					new SigmoidFunction(), 
					2, 2, 1)
			);

			var trainingSet = ConvertToTrainigSet();
			BackpropagationMethod backpropagation = new BackpropagationMethod(network, 0.1d);
			backpropagation.Learn(trainingSet, 10);
			var result = network.Convolution(new[] { 1d, 0d }).Last();
			Logger.Info($"Network info result [1, 0]=[{result[0]}, {result[1]}]");
		}

		private static ICollection<TrainingSet> ConvertToTrainigSet()
		{
			return new List<TrainingSet>()
			{
				new TrainingSet()
				{
					Inputs = new [] { 1d, 0d },
					Ouputs = new [] { 1d }
				},
				new TrainingSet()
				{
					Inputs = new [] { 0d, 1d },
					Ouputs = new [] { 1d }
				},
				new TrainingSet()
				{
					Inputs = new [] { 1d, 1d },
					Ouputs = new [] { 0d }
				},
				new TrainingSet()
				{
					Inputs = new [] { 0d, 0d },
					Ouputs = new [] { 0d }
				}
			};
		}
	}
}