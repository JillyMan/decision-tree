using System;

namespace MachineLearning.Core.Extensions
{
    public static class ArrayStatisticExtensions
    {
        public static int[] GetOnlyById(this int[] input, int[] elementsId)
        {
            var result = new int[elementsId.Length];

            for (var i = 0; i < elementsId.Length; ++i)
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
            var max = array[0];
            index = 0;

            for (var i = 0; i < array.Length; ++i)
            {
                if (!(array[i] > max)) continue;

                index = i;
                max = array[i];
            }

            return max;
        }
    }
}
