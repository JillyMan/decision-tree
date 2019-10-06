using System;
using System.Linq;

namespace DecisionTree.Services.Builders
{
	public static class ArrayStatisticExtensions
	{
		public static int[] GetOnlyById(this int[] input, int[] elementsId)
		{
			int[] result = new int[elementsId.Length];

			for (int i = 0; i < elementsId.Length; ++i)
			{
				result[i] = input[elementsId[i]];
			}

			return result;
		}

		public static int[] GetFrequency(this int[] input, int numberOfClasses)
		{
			if (input.Length == 0) return null;
			var inputFreq = new int[numberOfClasses];
			Array.ForEach(input, x => inputFreq[x]++);
			return inputFreq;
		}

		public static double Max(this double[] array, out int index)
		{
			double max = array[0];
			index = 0;

			for (int i = 0; i < array.Length; ++i)
			{
				if(array[i] > max)
				{
					index = i;
					max = array[i];
				} 
			}
			
			return max;
		}
	}
}
