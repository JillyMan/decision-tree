using FluentAssertions;
using MachineLearning.DecisionTree.LearnAlgorithm;
using Xunit;

namespace MachineLearning.Test
{
	public class Id3AlghoritmTest
	{
		[Theory]
		[InlineData(new[] { 0, 0 }, 0)]
		[InlineData(new[] { 0, 1 }, 1)]
		[InlineData(new[] { 1, 0 }, 1)]
		[InlineData(new[] { 1, 1 }, 0)]
		public void ReadDataFromFileLearnTree_ReturnDecisionTreeAndCheckResult(int[] vector, int expected)
		{
			var alghoritm = new Id3Algorithm(new DecisionTree.Models.DecisionVariable[]
			{
				new DecisionTree.Models.DecisionVariable("x1", "0", "1"),
				new DecisionTree.Models.DecisionVariable("x2", "0", "1"),
			},
			new DecisionTree.Models.DecisionVariable("result", "0", "1"));

			var tree = alghoritm.Learn(new int[][]
			{
				new [] {0, 0},
				new [] {0, 1},
				new [] {1, 0},
				new [] {1, 1},
			},
			new[] { 0, 1, 1, 0 });

			var result = tree.Compute(vector);
			result.Should().Be(expected);
		}
	}
}
