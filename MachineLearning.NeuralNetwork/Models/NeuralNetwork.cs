using MachineLearning.NeuralNetwork.ActivateFunctions;
using System.Collections.Generic;

namespace MachineLearning.NeuralNetwork
{
	public class NeuralNetwork
	{
		public IList<Layer> Layers { get; }

		public IActivateFunction ActivateFunction { get; }

		public NeuralNetwork(NetworkSettings settings)
		{
			ActivateFunction = settings.ActivateFunction;
			Layers = new List<Layer>(settings.Size);

			for (var i = 0; i < settings.Size - 1; ++i)
			{
				Layers[i] = new Layer(
					settings.LayersSize[i],
					settings.LayersSize[i + 1]);
			}
		}

		public double[][] Convolution(double[] inputs)
		{
			var result = new double[Layers.Count][];
			var currentInputs = inputs;

			for (var i = 0; i < Layers.Count; ++i)
			{
				result[i] = Layers[i].CalcOutputValues(currentInputs, ActivateFunction);
			}

			return result;
		}
	}
}