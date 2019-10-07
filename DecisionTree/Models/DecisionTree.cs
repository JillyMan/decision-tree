using System.Collections.Generic;

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
			return Bfs(Root, vector);
		}

        private static int Bfs(Node node, IReadOnlyList<int> vector)
        {
            while (true)
            {
                if (node.IsSheet) 
                    return node.Output;
                node = node.NextNode(vector[node.AttrIndex]);
            }
        }
    }
}