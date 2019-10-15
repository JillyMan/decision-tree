using System.Collections.Generic;

namespace MachineLearning.DecisionTree.Services
{
	public interface IDecisionService
    {
		bool CheckError();

		int GetDecision(int[] vector);

        string GetDecision(IDictionary<string, string> input);
	}
}