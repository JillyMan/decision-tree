using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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

		public string[][] GetByFilter(Func<string, bool> filter, int i) => 
			Values.Select(x => x.Where(filter).ToArray()).ToArray();
	}
}
