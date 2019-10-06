namespace DecisionTree.Models
{
	public class DecisionTree
	{
		public int NumberOfClasses { get; }

		public int NumerOfInputs => Attributes.Length;

		public DecisionVariable[] Attributes { get; }
	
		public Node Root { get; set; }

		public DecisionTree(DecisionVariable[] attributes)
		{
			Attributes = attributes;
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

