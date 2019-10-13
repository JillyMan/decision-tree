namespace MachineLearning.DecisionTree.LearnAlgorithm
{
	public interface IDecisionTreeBuilder
	{
		Models.DecisionTree Learn(int[][] inputs, int[] outputs);
	}
}
