using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.Models
{
	public class DecisionNode
	{
		public DecisionNode Parent { get; set; }

		public List<DecisionNode> Branches { get; } = new List<DecisionNode>();

		public bool IsSheet => Branches.Count == 0;

		public int AttrIndex { get; set; }

		public int Value { get; set; }

		public int Output { get; set; }

		public string Name { get; set; }

		public DecisionNode NextNode(int value) =>
			Branches
				.FirstOrDefault(e => e.Value == value);
	}
}
