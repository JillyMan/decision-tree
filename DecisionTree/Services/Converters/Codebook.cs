using DecisionTree.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace DecisionTree.Services.Converters
{
	public class Codebook : ICodebook
	{
		private int[][] _newColumnsPresentation;

		private IDictionary<string, int> _namesIds;
		private IDictionary<string, IDictionary<string, int>> _mapBook;

		public Codebook(DataTable table)
		{
			Init(table);
		}

		#region Setup

		private void Init(DataTable dataTable)
		{
			int width = dataTable.Columns.Count;
			int height = dataTable.Rows.Count;

			var rows = dataTable
				.AsEnumerable()
				.ToArray()
				.Select(x => x.ItemArray.Cast<string>().ToArray())
				.ToArray();

			var columns = dataTable.Columns;
			_namesIds = FillIdsMappings(columns);
			_mapBook = FillMapBook(rows, columns);
			_newColumnsPresentation = TransformStringRowsToInt(rows, columns);
		}

		private IDictionary<string, IDictionary<string, int>> PrepareMap(DataColumnCollection columns)
		{
			var map = new Dictionary<string, IDictionary<string, int>>();

			for (int i = 0; i < columns.Count; ++i)
			{
				var colName = columns[i].ColumnName;
				map.Add(colName, new Dictionary<string, int>());
			}

			return map;
		}

		private IDictionary<string, IDictionary<string, int>> FillMapBook(
			string[][] rows,
			DataColumnCollection columns)
		{
			var map = PrepareMap(columns);

			int[] counters = new int[columns.Count];
			for (int i = 0; i < rows.Length; ++i)
			{
				FillRow(rows[i], counters, columns, map);
			}

			return map;
		}

		private void FillRow(
			string[] row,
			int[] counters,
			DataColumnCollection columns,
			IDictionary<string, IDictionary<string, int>> map)
		{
			for (int i = 0; i < row.Length; ++i)
			{
				var value = row[i];
				var colName = columns[i].ColumnName;
				if (!map[colName].ContainsKey(value))
				{
					map[colName][value] = counters[i]++;
				}
			}
		}

		private int[][] TransformStringRowsToInt(string[][] rows, DataColumnCollection columns)
		{
			int width = columns.Count;
			int height = rows.Length;
			int[][] result = new int[width][];

			for (int i = 0; i < width; ++i)
			{
				result[i] = new int[height];
				var name = columns[i].ColumnName;
				for (int j = 0; j < height; ++j)
				{
					var value = rows[j][i];
					result[i][j] = _mapBook[name][value];
				}
			}

			return result;
		}

		private IDictionary<string, int> FillIdsMappings(DataColumnCollection columns)
		{
			var result = new Dictionary<string, int>();
			for (int i = 0; i < columns.Count; ++i)
			{
				var colName = columns[i].ColumnName;
				result[colName] = i;
			}
			return result;
		}
		#endregion

		public int[][] GetArray(params string[] vars)
		{
			int[][] result = new int[vars.Length][];
			for (int i = 0; i < vars.Length; ++i)
			{
				if (!_namesIds.TryGetValue(vars[i], out var nameIndex))
					throw new System.ArgumentException($"Argument not found {vars[i]}");

				result[i] = _newColumnsPresentation[nameIndex];
			}

			return result;
		}

		public int[] GetArray(string varName)
		{
			if (!_namesIds.TryGetValue(varName, out int id))
				throw new System.ArgumentException($"Argument not found: {varName}");

			return _newColumnsPresentation[id];
		}

		public int[] Translate(IDictionary<string, string> input)
		{
			int[] result = new int[input.Keys.Count];
			
			foreach(var pair in input)
			{
				int index = _namesIds[pair.Key];
				result[index] = _mapBook[pair.Key][pair.Value];
			}

			return result;
		}

		// fucking trasn: todo: need refactor.
		// can be break!
		public string Translate(string name, int value)
		{
			return _mapBook[name].Where(x => x.Value == value).FirstOrDefault().Key;
		}
	}
}
