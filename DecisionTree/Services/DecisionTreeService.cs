using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree.Services
{
	public abstract class ID3DecisionTreeService : IDecisionService
	{
		public object GetDecision(object obj)
		{
			throw new NotImplementedException();
		}
	}

	interface IDataProvider
	{

	}

	public class DecisionTreeService : IDecisionService
	{
		public DecisionTreeService()
		{
		}

		public object GetDecision(object obj)
		{
			throw new NotImplementedException();
		}

		private void ID3Learn()
		{

		}
	}
}
