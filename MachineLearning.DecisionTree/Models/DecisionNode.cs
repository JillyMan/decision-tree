using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.DecisionTree.Models
{
	public struct Branch
	{
		public DecisionNode Parent { get; set; }

		public string Name;

		public int Value;
	}

	public class DecisionNode
	{
        public string AttrName { get; set; }

        public int AttrIndex { get; set; }
	
		public Branch Branch { get; set; }

        public int Output { get; set; }

        public bool IsLeaf => Childs.Count == 0;

		public List<DecisionNode> Childs { get; } = new List<DecisionNode>();

		public DecisionNode NextNode(int branchValue) =>
			Childs
				.FirstOrDefault(e => e.Branch.Value == branchValue);
	}
}
