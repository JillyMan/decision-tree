using DecisionTree.Models;

namespace DecisionTree.Services.Builders
{
	public interface IDecisionTreeBuilder
	{
		Node Build(int[][] inputs, int[] outputs);
	}
}
