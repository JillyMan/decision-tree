using System.Collections.Generic;

namespace MachineLearning.NeuralNetwork.LearnAlghoritm
{
	public interface INeuralNetLearnAlgorithm
	{
		void Learn(ICollection<TrainingSet> trainingSet);
	}
}
