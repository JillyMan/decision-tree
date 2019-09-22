using DecisionTree.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DecisionTree.Test
{
	public class FeatureTest
	{
		private readonly double Epsilon = 0.0001;
		[Theory] 
		[InlineData(
			new int[] { 2, 2 },
			"yes",
			new int[] { 2, 1 }, 
			"no",
			0.965d)]
		public void TestOnTrueResult(int[] listFreq1, string name1, int[] listFreq2, string name2, double expectedResult)
		{
			var entropy = new Feature(
				new ValueOfFeature[]
				{
					GetValue(listFreq1, name1),
					GetValue(listFreq2, name2),
				}).InformationEntropy;

			Assert.True(Math.Abs(expectedResult - entropy) < Epsilon);
		}

		private ValueOfFeature GetValue(int[] freq, string name)
		{
			return new ValueOfFeature(freq, name);
		}
	}
}
