using System;
using System.Linq;

namespace DecisionTree.Core
{
	public class ValueOfFeature
	{
		private static readonly double Log2Base = 2d;

		private int[] m_Results;

		public int Qty { get; }

		public double Entropy { get; }

		public string Name { get; }

		public ValueOfFeature(int[] result, string name)
		{
			m_Results = result;
			Name = name;
			Qty = result.Sum(x => x);
			Entropy = CalculateEntropy(m_Results);
		}

		private double CalculateEntropy(int[] frequency)
		{
			var result = 0d;
			for (var i = 0; i < frequency.Length; ++i)
			{
				var area = frequency[i] / (double)Qty;
				result += -area * Log2(area);
			}

			return result;
		}

		private static double Log2(double value) => Math.Log(value, Log2Base);
	}
}