using FluentAssertions;
using MachineLearning.DecisionTree.Converters;
using MachineLearning.DecisionTree.LearnAlgorithm;
using MachineLearning.DecisionTree.Logger;
using MachineLearning.DecisionTree.Models;
using MachineLearning.DecisionTree.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MachineLearning.Test
{
	public class DecisionTreeServiceTest
	{
		[Theory]
		[InlineData(new[] { 0, 0 }, 0)]
		[InlineData(new[] { 0, 1 }, 1)]
		[InlineData(new[] { 1, 0 }, 1)]
		[InlineData(new[] { 1, 1 }, 0)]
		public void XOR_TREE_RETURN_EXPECTED_RESULT(int[] vector, int expectedResult)
		{
			var builderMock = new Mock<IDecisionTreeBuilder>();
			builderMock.Setup(x => x.Learn(It.IsAny<int[][]>(), It.IsAny<int[]>()))
				.Returns(GetContext());

			var codebookMock = new Mock<ICodebook>();
			codebookMock
				.Setup(x => x.GetArray(It.IsAny<string[]>()))
				.Returns(() => null);

			var loggerMock = new Mock<ILogger>();
			loggerMock.Setup(x => x.Info(It.IsAny<string>()));

			var result = new DecisionTreeService(
					GetTreeInfo(),
					codebookMock.Object,
					builderMock.Object,
					loggerMock.Object)
				.GetDecision(vector);

			result.Should().Be(expectedResult);
		}

		private static TreeInfo GetTreeInfo()
		{
			return new TreeInfo
			{
				Inputs = new[] { "x1", "x2" },
				Output = "result"
			};
		}

		private static DecisionTree.Models.DecisionTree GetContext()
		{
			var x1 = new DecisionNode()
			{
				AttrIndex = 0,
			};

			var x21 = new DecisionNode()
			{
				AttrIndex = 1,
				Branch = new Branch
				{
					Value = 0
				}
			};

			var x22 = new DecisionNode()
			{
				AttrIndex = 1,
				Branch = new Branch
				{
					Value = 1
				}
			};

			x1.Childs.AddRange(new List<DecisionNode> { x21, x22 });

			var l00 = new DecisionNode()
			{
				Output = 0,
				Branch = new Branch
				{
					Value = 0
				}
			};

			var l01 = new DecisionNode()
			{
				Output = 1,
				Branch = new Branch
				{
					Value = 1
				}
			};

			var l11 = new DecisionNode()
			{
				Output = 0,
				Branch = new Branch
				{
					Value = 1
				}
			};

			var l10 = new DecisionNode()
			{
				Output = 1,
				Branch = new Branch
				{
					Value = 0
				}
			};

			x21.Childs.AddRange(new List<DecisionNode> { l01, l00 });
			x22.Childs.AddRange(new List<DecisionNode> { l11, l10 });

			return new DecisionTree.Models.DecisionTree(It.IsAny<DecisionVariable[]>(), It.IsAny<DecisionVariable>())
			{
				Root = x1,
			};
		}
	}
}
