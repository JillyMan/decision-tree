using System;
using System.Linq;

namespace DecisionTree.Core
{
	public static class Measure
	{
		public static double Entropy(int[] input, int numberOfClasses)
		{
			if(input.Length == 0) return 0d;
			var inputFreq = new int[numberOfClasses + 1];
			Array.ForEach(input, x => inputFreq[x]++);
			return CalcEntr(inputFreq, input.Length);
		}

		private static double CalcEntr(int[] frequency, int qty)
		{
			var result = 0d;
			for (var i = 0; i < frequency.Length; ++i)
			{
				var area = frequency[i] / (double)qty;
				if (area == 0d) continue;
				result += -area * Log2(area);
			}

			return result;
		}

		private static double Log2(double value) => Math.Log(value, 2);
	}
}
