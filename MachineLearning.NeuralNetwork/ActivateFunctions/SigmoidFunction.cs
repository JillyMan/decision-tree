using System;

namespace MachineLearning.NeuralNetwork.ActivateFunctions
{
	public class SigmoidFunction : IActivateFunction
	{
		public double Activate(double x)
		{
			return 1 / (1 + Math.Exp(-x));
		}

		public double Differentiation(double value)
		{
			double fdx = Activate(value);
			return fdx * (1 - fdx);
		}
	}
}
