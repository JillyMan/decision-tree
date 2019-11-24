using MachineLearning.NeuralNetwork.ActivateFunctions;

namespace MachineLearning.NeuralNetwork
{
	public class NetworkSettings
	{
		public int[] LayersSize { get; }

		public int Size => LayersSize.Length;

		public IActivateFunction ActivateFunction { get; }

		public NetworkSettings(IActivateFunction activationFunction,
			params int[] layers)
		{
			LayersSize = layers;
			ActivateFunction = activationFunction;
		}
	}
}