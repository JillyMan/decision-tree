using System.Collections.Generic;
using System.Linq;

namespace DecisionTree.Models
{
	public class Node
	{
		public Node Parent { get; set; }

		public List<Node> Branches { get; } = new List<Node>();

		public bool IsSheet => Branches.Count == 0;

		public int AttrIndex { get; set; }

		public int Value { get; set; }

		public int Output { get; set; }

		public string Name { get; set; }

		public Node NextNode(int value) =>
			Branches
				.Where(e => e.Value == value)
				.FirstOrDefault();
	}
}
