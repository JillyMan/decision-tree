using System;
using MachineLearning.NeuralNetwork.Abstractions;

namespace MachineLearning.NeuralNetwork.ActivateFunctions
{
	public class SigmoidFunction : IActivateFunction
	{
		public double Activate(double x)
		{
			return 1 / (1 + Math.Exp(-x));
		}
	}
}
