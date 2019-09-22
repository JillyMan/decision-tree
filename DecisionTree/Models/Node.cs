using System.Collections.Generic;
using System.Linq;

namespace DecisionTree.Models
{
	public class Node
	{
		public List<Edge> Childs { get; } = new List<Edge>();

		public bool IsCheet => Childs.Count == 0;

		public string Name { get; set; }

		public Node SelectNextNode(string value) =>
				Childs.Where(edge => edge.Value == value)
				.FirstOrDefault()
				.ChildNode;
	}
}
