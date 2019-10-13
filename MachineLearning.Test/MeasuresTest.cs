using MachineLearning.DecisionTree.Core;
using System;
using Xunit;

namespace MachineLearning.Test
{
    public class MeasuresTest
    {
        private const double Epsilon = 0.0001;

        [Theory]
        [InlineData(new[] { 1, 1, 1, 1, 2, 2, 2 }, 4, 0.9852d)]
        [InlineData(new[] { 0, 0, 0, 0, 1, 1, 1 }, 2, 0.9852d)]
        public void TestOnTrueResult(int[] input, int numberOfClasses, double expectedResult)
        {
            var entropy = Measure.CalcEntropy(input, numberOfClasses);
            Assert.True(Math.Abs(expectedResult - entropy) < Epsilon);
        }
    }
}
