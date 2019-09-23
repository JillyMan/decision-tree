using DecisionTree.Models;
using DecisionTree.Services.TreeBuilder;

namespace DecisionTree.Services
{
	public class DecisionTreeService : IDecisionService
	{
		private readonly Node m_RootTree;

		public DecisionTreeService(object obj, IDecisionTreeBuilder builder)
		{
			m_RootTree = builder.Build(TransormToTraningSet(obj));
		}

		private TraningSet TransormToTraningSet(object obj)
		{
			return new TraningSet(null, null);
		}

		public string GetDecision(Line line)
		{
			return DFS(m_RootTree, line);
		}

		public string DFS(Node node, Line line)
		{
			if(node.IsSheet) { return node.Name; }
			var key = node.Name;
			var nextNode = node.FindNext(line[key]);
			return DFS(nextNode, line);
		}
	}
}
