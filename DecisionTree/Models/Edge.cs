namespace DecisionTree.Models
{
	public class Edge
	{
		public Node ParentNode { get; set; }

		public Node ChildNode { get; set; }

		public string Value { get; set; }
	}
}
