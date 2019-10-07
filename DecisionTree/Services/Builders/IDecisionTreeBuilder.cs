namespace DecisionTree.Services.Builders
{
	public interface IDecisionTreeBuilder
	{
		Models.DecisionTree Learn(int[][] inputs, int[] outputs);
	}
}
