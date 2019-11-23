using System;
using MachineLearning.Core.Extensions;

namespace MachineLearning.NeuralNetwork
{
	public class Layer
	{
		private static readonly Random r = new Random();

		public double[,] Weights { get; }

		private int _inputNeuronCount;
		private int _outputNeuronCount;

		public bool WithHelpNeuron { get; }

		public Layer(int inputCount, int outputCount, double minWeight, double maxWeight, bool withHelpNeuron)
		{
			WithHelpNeuron = withHelpNeuron;

			_inputNeuronCount = inputCount;
			_outputNeuronCount = withHelpNeuron ? outputCount + 1 : outputCount;


			Weights = new double[inputCount, outputCount];

			for(int i = 0; i < inputCount; ++i)
			{
				for(int j = 0; j < outputCount; ++j)
				{
					Weights[i, j] = r.NextDouble(minWeight, maxWeight);
				}
			}
		}

		public double[] CalcOutputValues(double[] inputs)
		{
			if (_inputNeuronCount != inputs?.Length) 
				throw new ArgumentOutOfRangeException();

			var result = new double[_outputNeuronCount];

			for (var i = 0; i < _inputNeuronCount; ++i)
			{
				for (var j = 0; j < _outputNeuronCount; ++j)
				{
					result[i] += inputs[i] * Weights[i, j];
				}
			}

			return result;
		}
	}
}
