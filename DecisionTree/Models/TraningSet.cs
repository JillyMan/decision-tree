using System;
using System.Linq;

namespace DecisionTree.Models
{
	public class TraningSet
	{
		public string[] Titles;

		public string[][] Values { get; }

		public string this[int i, int j] => Values[i][j];

		public TraningSet(string[] titles, string[][] values)
		{
			Titles = titles;
			Values = values;
		}

		public string[][] GetByFilter(int withoutIndex, Func<string, bool> dataFilter) => throw new NotImplementedException();
	}
}