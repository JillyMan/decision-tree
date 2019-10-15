using FluentAssertions;
using MachineLearning.DecisionTree.Models;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MachineLearning.Test
{
	public class DecisionComputeTreeTest
	{
		[Theory]
		[InlineData(new[] { 0, 0 }, 0)]
		[InlineData(new[] { 0, 1 }, 1)]
		[InlineData(new[] { 1, 0 }, 1)]
		[InlineData(new[] { 1, 1 }, 0)]
		public void XOR_TREE_RETURN_EXPECTED_RESULT(int[] vector, int expectedResult)
		{
			var tree = GetContext();
			var result = tree.Compute(vector);
			result.Should().Be(expectedResult);
		}

		private static DecisionTree.Models.DecisionTree GetContext()
		{
			var x1 = new DecisionTreeNode()
			{
				AttributeInfo = new NodeInfo
				{
					Index = 0,
				}
			};

			var x21 = new DecisionTreeNode()
			{
				AttributeInfo = new NodeInfo
				{
					Index = 1,
				},
				Branch = new NodeInfo
				{
					Index = 0
				}
			};

			var x22 = new DecisionTreeNode()
			{
				AttributeInfo = new NodeInfo
				{
					Index = 1,
				},
				Branch = new NodeInfo
				{
					Index = 1
				}
			};

			x1.Childs.AddRange(new List<DecisionTreeNode> { x21, x22 });

			var l00 = new DecisionTreeNode()
			{
				LeafInfo = new NodeInfo
				{
					Index = 0,
				},
				Branch = new NodeInfo
				{
					Index = 0
				}
			};

			var l01 = new DecisionTreeNode()
			{
				LeafInfo = new NodeInfo
				{
					Index = 1,
				},
				Branch = new NodeInfo
				{
					Index = 1
				}
			};

			var l11 = new DecisionTreeNode()
			{
				LeafInfo = new NodeInfo
				{
					Index = 0,
				},
				Branch = new NodeInfo
				{
					Index = 1
				}
			};

			var l10 = new DecisionTreeNode()
			{
				LeafInfo = new NodeInfo
				{
					Index = 1,
				},
				Branch = new NodeInfo
				{
					Index = 0
				}
			};

			x21.Childs.AddRange(new List<DecisionTreeNode> { l01, l00 });
			x22.Childs.AddRange(new List<DecisionTreeNode> { l11, l10 });

			return new DecisionTree.Models.DecisionTree(It.IsAny<DecisionVariable[]>(), It.IsAny<DecisionVariable>())
			{
				Root = x1,
			};
		}
	}
}
