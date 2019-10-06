using System.Data;
using DecisionTree.Models;
using DecisionTree.Services.Builders;
using DecisionTree.Services.Converters;

namespace DecisionTree.Services
{
	public class DecisionTreeService : IDecisionService
	{
		private Models.DecisionTree _tree;
		private ICodebook _codebook;
		private IDecisionTreeBuilder _treeBuilder;

		public DecisionTreeService(
			DataTable learnSet,
			ICodebook codebook,
			IDecisionTreeBuilder builder)
		{
			_codebook = codebook;
			_treeBuilder = builder;
			init(learnSet);
		}

		private void init(DataTable learnSet)
		{
			var input = default(int[][]);
			var output = default(int[]);

			_tree = _treeBuilder.Learn(input, output);
		}

		public int GetDecision(int[] vector)
		{
			return _tree.Compute(vector);
		}
	}
}
