namespace DecisionTree.Services
{
	public class DecisionVars
	{
		public string Name { get; }

		public int Values { get; }

		public DecisionVars(string name, int vals)
		{
			Name = name;
			Values = vals;
		}
	}
}
