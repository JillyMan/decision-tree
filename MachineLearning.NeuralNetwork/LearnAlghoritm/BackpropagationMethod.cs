using MachineLearning.NeuralNetwork.ActivateFunctions;
using System.Collections.Generic;

namespace MachineLearning.NeuralNetwork.LearnAlghoritm
{
	public class BackpropagationMethod : INeuralNetLearnAlgorithm
	{
		private readonly double _learningRate;
		private readonly NeuralNetwork _network;
		private readonly IActivateFunction _activateFunction;

		public BackpropagationMethod(NeuralNetwork network, double learningRate)
		{
			_network = network;
			_learningRate = learningRate;
			_activateFunction = _network.ActivateFunction;
		}

		public void Learn(ICollection<TrainingSet> trainingSet)
		{
			foreach (var set in trainingSet)
			{
				var neuronValues = _network.Convolution(set.Inputs);
				CorrectWeight(_network, neuronValues, set.Ouputs);
			}
		}

		private void CorrectWeight(NeuralNetwork network, double[][] neuronValues, double[] trainigOuputs)
		{
		}
	}
}