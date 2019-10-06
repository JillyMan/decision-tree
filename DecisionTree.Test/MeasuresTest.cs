using DecisionTree.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DecisionTree.Test
{
	public class MeasuresTest
	{
		private readonly double Epsilon = 0.0001;
		[Theory] 
		[InlineData(new int[] { 1, 1, 1, 1, 2, 2, 2  }, 2, 0.9852d)]
		[InlineData(new int[] { 1, 1, 1, 1, 2, 2, 2 }, 4, 0.9852d)]
		public void TestOnTrueResult(int[] input, int numberOfClasses, double expectedResult)
		{
			var entropy = Measure.Entropy(input, numberOfClasses);
			Assert.True(Math.Abs(expectedResult - entropy) < Epsilon);
		}
	}
}
