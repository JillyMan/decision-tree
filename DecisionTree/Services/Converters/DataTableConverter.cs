using System.Collections.Generic;
using System.Data;
using System.Linq;
using DecisionTree.Extensions;

namespace DecisionTree.Services.Converters
{
	public interface ICodebook
	{
		int Translate(params string[] attrs);
	}

	/*
	 * Main purpose investigate DecisionTree class.
	 * Goal map string to int
	 * id3 -> will work only with int[][]
	 * that mean -> need map string to int
	 */ 
	public class Codebook : ICodebook
	{
		private readonly DataTable _table;
		private readonly IDictionary<string, int> _mapBook;

		public Codebook(Variable[] vars, DataTable table)
		{
			_table = table;
		}

		private int CalcDistinct(DataTable table, string colName) =>
			table
				.AsEnumerable()
				.Select(x => x[colName])
				.Distinct()
				.Count();

		//todo: while use only when dataset contains descrete vars
		public int[][] ConvertDescreteVars(DataRow row)
		{

			return null;
		}

		public int Map(string key)
		{
			return _mapBook[key];
		}
	}
}
