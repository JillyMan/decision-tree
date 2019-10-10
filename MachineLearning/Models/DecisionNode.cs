using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.Models
{
	public class DecisionNode
	{
        // index value of attribute
		public int Index { get; set; }

        // is business value (human readable)
        public string Name { get; set; }

        // index of attribute
        public int AttrIndex { get; set; }

        // isLeaf == true then this value of leaf
        public int Output { get; set; }

        public DecisionNode Parent { get; set; }

        public bool IsLeaf => Branches.Count == 0;

		public List<DecisionNode> Branches { get; } = new List<DecisionNode>();

		public DecisionNode NextNode(int index) =>
			Branches
				.FirstOrDefault(e => e.Index == index);
	}
}
