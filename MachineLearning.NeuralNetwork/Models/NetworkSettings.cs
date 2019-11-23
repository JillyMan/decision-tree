using MachineLearning.NeuralNetwork.Abstractions;

namespace MachineLearning.NeuralNetwork
{
	public class NetworkSettings
	{
		public int[] LayersSize { get; }

		public (double min, double max) NeuronInputInterval { get; }

		public int Size => LayersSize.Length;

		public IActivateFunction ActivateFunction { get; }

		public NetworkSettings((double min, double max) valueInterval, IActivateFunction activationFunction, params int[] layers)
		{
			LayersSize = layers;
			NeuronInputInterval = valueInterval;
			ActivateFunction = activationFunction;
		}
	}
}
