using DecisionTree.Models;
using DecisionTree.Services.Builders;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DecisionTree.Services
{
	public class DecisionTreeService : IDecisionService
	{
		private readonly Node m_RootTree;

		public DecisionTreeService(
			DataTable learnSet, 
			IDecisionTreeBuilder builder)
		{
			var input = default(int[][]);
			var output = default(int[]);

			m_RootTree = builder.Build(input, output);
		}

		public string GetDecision(Vector line)
		{
			return DFS(m_RootTree, line);
		}

		private string DFS(Node node, Vector line)
		{
			if(node.IsSheet) { return node.Name; }
			var key = node.Name;
			var nextNode = node.FindNext(line[key]);
			return DFS(nextNode, line);
		}
	}
}
