using System.Collections.Generic;

namespace MachineLearning.DecisionTree.Services
{
	public interface IDecisionService
    {
        string GetDecision(IDictionary<string, string> input);

		int GetDecision(int[] vector);
	}
}