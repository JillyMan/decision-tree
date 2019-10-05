namespace DecisionTree.Services
{
	public enum VarKind
	{
		Descrete,
		Continuous
	}

	public class Variable
	{
		public string Name { get; }

		public object Value { get; }

		public VarKind Kind { get; }

		public Variable(string name, object value, VarKind kind)
		{
			Name = name;
			Value = value;
			Kind = kind;
		}
	}
}
