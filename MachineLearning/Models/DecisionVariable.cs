namespace MachineLearning.Models
{
	public class DecisionVariable
	{
		public string Name { get; }

		public int RangeLength { get; }

		public string[] NameRange { get; set; }

		public DecisionVariable(string name, int rangeLength, string[] nameRange)
		{
			if (nameRange.Length != rangeLength) throw new System.ArgumentException("Invalid length ranges");

			Name = name;
			NameRange = nameRange;
			RangeLength = rangeLength;
		}
	}
}
