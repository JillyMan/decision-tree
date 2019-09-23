using DecisionTree.Models;

namespace DecisionTree.Services.TreeBuilder
{
	public interface IDecisionTreeBuilder
	{
		Node Build(TraningSet set);
	}
}
