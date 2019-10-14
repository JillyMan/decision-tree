using System;
using System.Linq;
using MachineLearning.Core.Extensions;

namespace MachineLearning.Core.Statistics
{
    public static class Measure
    {
        public static double CalcEntropy(int[] input, int numberOfClasses)
        {
            if (input.Length == 0) return 0d;
            var inputFreq = input.GetFrequency(numberOfClasses);
            return EntropyByFreq(inputFreq, input.Length);
        }

		public static double EntropyByFreq(int[] frequency)
		{
			var qty = frequency.Sum(x => x);
			return EntropyByFreq(frequency, qty);
		}

		public static double EntropyByFreq(int[] frequency, int qty)
        {
			if (qty == 0) return 0d;

            var result = 0d;
            for (var i = 0; i < frequency.Length; ++i)
            {
                var area = frequency[i] / (double)qty;
                if (Math.Abs(area) < double.Epsilon) continue;
                result += -area * Log2(area);
            }

            return result;
        }

        private static double Log2(double value) => Math.Log(value, 2);
    }
}
