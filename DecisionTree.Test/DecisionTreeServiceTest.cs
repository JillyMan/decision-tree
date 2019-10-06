using DecisionTree.Models;
using DecisionTree.Services;
using DecisionTree.Services.Builders;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;
using System.Data;
using DecisionTree.Services.Converters;

namespace DecisionTree.Test
{
	public class DecisionTreeServiceTest
	{
		[Theory]
		[InlineData(new int[] { 0, 0 }, 0)]
		[InlineData(new int[] { 0, 1 }, 1)]
		[InlineData(new int[] { 1, 0 }, 1)]
		[InlineData(new int[] { 1, 1 }, 0)]
		public void XOR_TREE_RETURN_EXPECTED_RESULT(int[] vector, int expectedResult)
		{
			var mock = new Mock<IDecisionTreeBuilder>();
			mock.Setup(x => x.Learn(It.IsAny<int[][]>(), It.IsAny<int[]>()))
				.Returns(GetContext());

			var result = new DecisionTreeService(
				It.IsAny<DataTable>(), 
				It.IsAny<ICodebook>(),
				mock.Object)
				.GetDecision(vector);

			result.Should().Be(expectedResult);
		}

		private Models.DecisionTree GetContext()
		{
			var x1 = new Node()
			{
				AttrIndex = 0,
			};

			var x21 = new Node()
			{
				AttrIndex = 1,
				Value = 0,
			};

			var x22 = new Node()
			{
				AttrIndex = 1,
				Value = 1,
			};

			x1.Branches.AddRange(new List<Node> { x21, x22 });

			var l00 = new Node()
			{
				Output = 0,
				Value = 0,
			};

			var l01 = new Node()
			{
				Output = 1,
				Value = 1
			};

			var l11 = new Node()
			{
				Output = 0,
				Value = 1,
			};

			var l10 = new Node()
			{
				Output = 1,
				Value = 0,
			};
	
			x21.Branches.AddRange(new List<Node> { l01, l00 });
			x22.Branches.AddRange(new List<Node> { l11, l10 });

			return new Models.DecisionTree(null)
			{
				Root = x1,
			};
		}
	}
}
