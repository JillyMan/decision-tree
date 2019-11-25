using System.Collections.Generic;
using MachineLearning.NeuralNetwork.Models;

namespace MachineLearning.NeuralNetwork.LearnAlgorithm
{
	public interface INeuralNetLearnAlgorithm
    {
        void Learn(ICollection<TrainingSet> trainingSet, int epoch);
    }
}
