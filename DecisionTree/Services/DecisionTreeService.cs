using DecisionTree.Models;

namespace DecisionTree.Services
{
	public interface IDecisionTreeBuilder
	{
		Node Build(TraningSet set);
	}

	public class DecisionTreeService : IDecisionService
	{
		private Node m_RootTree;
		public DecisionTreeService(TraningSet traningSet, IDecisionTreeBuilder builder)
		{
			m_RootTree = builder.Build(traningSet);
		}

		public object GetDecision(Line line)
		{
			return "Answer";
		}
	}
}
