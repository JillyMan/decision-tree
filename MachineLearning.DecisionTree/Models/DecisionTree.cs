using System.Collections.Generic;

namespace MachineLearning.DecisionTree.Models
{
	public class DecisionTree
	{
		public DecisionTreeNode Root { get; set; }

		public int NumerOfInputs => Attributes.Length;

		public int NumberOfOuputClasses => SolveAttribute.RangeLength;

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

        private int SearchAnswer(DecisionTreeNode node, IReadOnlyList<int> vector)
        {
            for(;;)
            {
                if (node.IsLeaf) 
                    return node.LeafInfo.Index;
				var attributeIndex = node.AttributeInfo.Index;
				node = node.GetNodeByBranchValue(vector[attributeIndex]);
            }
        }
    }
}