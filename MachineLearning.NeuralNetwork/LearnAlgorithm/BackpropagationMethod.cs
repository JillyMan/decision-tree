using System;
using MachineLearning.NeuralNetwork.ActivateFunctions;
using System.Collections.Generic;
using MachineLearning.NeuralNetwork.Models;

namespace MachineLearning.NeuralNetwork.LearnAlgorithm
{
	public class BackpropagationMethod : INeuralNetLearnAlgorithm
	{
		private readonly double _learningRate;
		private readonly Models.NeuralNetwork _network;
		private readonly IActivateFunction _activateFunction;

		public BackpropagationMethod(Models.NeuralNetwork network, double learningRate)
		{
			_network = network;
			_learningRate = learningRate;
			_activateFunction = _network.ActivateFunction;
		}

		public void Learn(ICollection<TrainingSet> trainingSet, int epoch)
		{
			while(--epoch > 0)
			{
				foreach (var set in trainingSet)
				{
					var neuronValues = _network.Convolution(set.Inputs);
					CorrectWeight(_network, neuronValues, set.Outputs);
				}
			}
		}

		private void CorrectWeight(Models.NeuralNetwork network, double[][] neuronValues, double[] trainigOuputs)
		{

		}

        private double MSE_Cost(double actual, double expected) =>
            (actual * actual + expected * expected) / 2d;
	}
}