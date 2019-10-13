using System;
using System.Linq;

namespace MachineLearning.DecisionTree.Extensions
{
    internal static class DecisionTreeExtensions
    {
        public static bool CheckError(this Models.DecisionTree tree, int[][] inputs, int[] outputs)
        {
            if (inputs == null || outputs == null) return false;

            var result = true;
            for (var i = 0; i < inputs.Length; ++i)
            {
                var res = tree.Compute(inputs[i]);
                result = result && res == outputs[i];

                if (result) continue;

                var vector = string.Join(", ", inputs[i].Select(x => x.ToString()));
                Console.WriteLine($"Crash for: {vector}");
            }

            return result;
        }
    }
}