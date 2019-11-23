using MachineLearning.NeuralNetwork.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.NeuralNetwork
{
	public class NeuralNetwork
	{
		public IList<Layer> Layers { get; private set; }
		private IActivateFunction _activateFunction;

		public NeuralNetwork(NetworkSettings settings)
		{
			_activateFunction = settings.ActivateFunction;
			Init(settings);
		}

		private void Init(NetworkSettings settings)
		{
			Layers = new List<Layer>(settings.Size)
			{ 
				new Layer(settings.LayersSize[0],
					settings.LayersSize[1],
					settings.NeuronInputInterval.min,
					settings.NeuronInputInterval.max)
			};

			for (var i = 1; i < settings.Size; ++i)
			{
				Layers[i] = new Layer(
					settings.LayersSize[i], 
					settings.LayersSize[i-1],
					settings.NeuronInputInterval.min, 
					settings.NeuronInputInterval.max);
			}
		}

		public double[] GetAnswer(double[] inputs)
		{
			double[] outputs = inputs;
			foreach (var layer in Layers)
			{
				outputs = layer.CalcOutputValues(outputs).Select(x => _activateFunction.Activate(x)).ToArray();
			}

			return outputs;
		}
	}
}
