using System;
using System.Collections.Generic;
using System.Text;
using DecisionTree.Models;

namespace DecisionTree.Services.Builders
{
	public class ID3Builder : IDecisionTreeBuilder
	{
		public ID3Builder(Variable[] vars)
		{

		}

		public Node Build(int[][] inputs, int[] outputs)
		{
			throw new NotImplementedException();
		}
	}
}
