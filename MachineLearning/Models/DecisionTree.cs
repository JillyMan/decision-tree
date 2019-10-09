using System.Collections.Generic;

namespace MachineLearning.Models
{
	public class DecisionTree
	{
		public DecisionNode Root { get; set; }

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
			return SearchAnswer(Root, vector);
		}

        private static int SearchAnswer(DecisionNode node, IReadOnlyList<int> vector)
        {
            for(;;)
            {
                if (node.IsSheet) 
                    return node.Output;
                node = node.NextNode(vector[node.AttrIndex]);
            }
        }
    }
}