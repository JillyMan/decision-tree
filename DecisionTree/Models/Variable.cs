namespace DecisionTree.Models
{
	public enum VarKind
	{
		Descrete,
		Continuous
	}

	public class DecisionVariable
	{
		public string Name { get; }

		public VarKind Kind { get; }

		public int RangeLength { get; }

		public DecisionVariable(string name, int rangeLength, VarKind kind = VarKind.Descrete)
		{
			Name = name;
			RangeLength = RangeLength;
			Kind = kind;
		}
	}
}
