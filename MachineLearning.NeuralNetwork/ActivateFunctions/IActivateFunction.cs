namespace MachineLearning.NeuralNetwork.ActivateFunctions
{
	public interface IActivateFunction
	{
		double Activate(double value);

		double Differentiation(double value);
	}
}