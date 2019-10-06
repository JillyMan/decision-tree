namespace DecisionTree.Models
{
	public class DecisionVariable
	{
		public string Name { get; }

		public int RangeLength { get; }

		public DecisionVariable(string name, int rangeLength)
		{
			Name = name;
			RangeLength = rangeLength;
		}
	}
}
