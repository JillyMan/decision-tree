using System.Collections.Generic;
using System.Linq;

namespace DecisionTree.Models
{
	public class Node
	{
		public List<Edge> Childs { get; } = new List<Edge>();

		public bool IsSheet => Childs.Count == 0;

		public string Name { get; set; }

		public Node FindNext(string value) =>
			Childs.Where(e => e.Value == value)
			.FirstOrDefault()?.ChildNode;
	}
}
