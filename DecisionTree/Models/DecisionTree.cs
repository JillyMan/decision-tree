namespace DecisionTree.Models
{
	public class DecisionTree
	{
		public Node Root { get; set; }

		public int NumerOfInputs => Attributes.Length;

		public int NumberOfClasses => SolveAttribute.RangeLength;

		public DecisionVariable[] Attributes { get; }

		public DecisionVariable SolveAttribute { get; }

		public DecisionTree(DecisionVariable[] attributes, DecisionVariable solveAttribute)
		{
			Attributes = attributes;
			SolveAttribute = solveAttribute;
		}

		public int Compute(int[] vector)
		{
			return Dfs(Root, vector);
		}

		private int Dfs(Node node, int[] vector)
		{
			if (node.IsSheet) return node.Output.Value;
			return Dfs(
				node.NextNode(vector[node.AttrIndex]),
				vector
			);
		}
	}
}