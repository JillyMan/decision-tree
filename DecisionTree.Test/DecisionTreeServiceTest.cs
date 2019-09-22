using DecisionTree.Models;
using DecisionTree.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DecisionTree.Test
{
	public class DecisionTreeServiceTest
	{
		[Theory]
		[InlineData("par1")]
		public void TestDecisionTree(string par1)
		{
			var mock = new Mock<IDecisionTreeBuilder>();
			mock.Setup(x => x.Build(It.IsAny<TraningSet>()))
				.Returns(GetContext());
			var service = new DecisionTreeService(It.IsAny<TraningSet>(), mock.Object);

			var line = new Line();
			var result = service.GetDecision(line);

		}

		private Node GetContext()
		{
			var root = new Node();

			var parent1 = new Node() { Name = "Wind" };
			var parent1Sheet = new Node { Name = "Yes" };
			parent1.Childs.Add(new Edge { ParentNode = parent1, ChildNode = parent1Sheet, Value = "Strong" });

			var parent2 = new Node() { Name = "Humidity" };
			var parent2Sheet = new Node { Name = "Noo" };
			parent2.Childs.Add(new Edge { ParentNode = parent2, ChildNode = parent2Sheet, Value = "Hight" });

			root.Childs.AddRange(
				new List<Edge>
				{
					new Edge() { ParentNode = root, ChildNode = parent1, Value="sunnuy"},
					new Edge() { ParentNode = root, ChildNode = parent2, Value="outlook"},
				});

			return root;
		}
	}
}
