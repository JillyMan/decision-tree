using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MachineLearning.Core.Converters
{
	public class Codebook : ICodebook
	{
		private int[][] _newColumnsPresentation;

		private IDictionary<string, int> _namesIds;
		private IDictionary<string, IDictionary<string, int>> _mapBook;
		private IDictionary<string, string[]> _metaInfo;

		public Codebook(DataTable table, IDictionary<string, string[]> metaInfo)
		{
			_metaInfo = metaInfo;
			Init(table);
		}

		#region Setup

		private void Init(DataTable dataTable)
		{
			var width = dataTable.Columns.Count;
			var height = dataTable.Rows.Count;

			var rows = dataTable
				.AsEnumerable()
				.ToArray()
				.Select(x => x.ItemArray.Cast<string>().ToArray())
				.ToArray();

			var columns = dataTable.Columns;
			_namesIds = FillIdsMappings(columns);
			_mapBook = FillMapBook(_metaInfo);
			_newColumnsPresentation = TransformStringRowsToInt(rows, columns);
		}

		private IDictionary<string, IDictionary<string, int>> PrepareMap(DataColumnCollection columns)
		{
			var map = new Dictionary<string, IDictionary<string, int>>();

			for (var i = 0; i < columns.Count; ++i)
			{
				var colName = columns[i].ColumnName;
				map.Add(colName, new Dictionary<string, int>());
			}

			return map;
		}

		private IDictionary<string, IDictionary<string, int>> FillMapBook(IDictionary<string, string[]> metaInfo)
		{
			var result = new Dictionary<string, IDictionary<string, int>>();

			foreach ((var key, var values) in metaInfo)
			{
				result.Add(key, new Dictionary<string, int>());
				for (int i = 0; i < values.Length; ++i)
				{
					result[key].Add(values[i], i);
				}
			}

			return result;
		}

		private IDictionary<string, IDictionary<string, int>> FillMapBook(
			string[][] rows,
			DataColumnCollection columns)
		{
			var map = PrepareMap(columns);

			var counters = new int[columns.Count];
			foreach (var row in rows)
			{
				FillRow(row, counters, columns, map);
			}

			return map;
		}

		private void FillRow(
			string[] row,
			int[] counters,
			DataColumnCollection columns,
			IDictionary<string, IDictionary<string, int>> map)
		{
			for (var i = 0; i < row.Length; ++i)
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
			var width = columns.Count;
			var height = rows.Length;
			var result = new int[height][];

			for (var i = 0; i < height; ++i)
			{
				result[i] = new int[width];
				for (var j = 0; j < width; ++j)
				{
					var name = columns[j].ColumnName;
					var value = rows[i][j];
					result[i][j] = _mapBook[name][value];
				}
			}

			return result;
		}

		private IDictionary<string, int> FillIdsMappings(DataColumnCollection columns)
		{
			var result = new Dictionary<string, int>();
			for (var i = 0; i < columns.Count; ++i)
			{
				var colName = columns[i].ColumnName;
				result[colName] = i;
			}
			return result;
		}
		#endregion

		public int[][] GetArray(params string[] vars)
		{
			var result = new int[_newColumnsPresentation.Length][];
			for (var i = 0; i < result.Length; ++i)
			{
				result[i] = GetRow(i, vars);
			}
			return result;
		}

		private int[] GetRow(int rowId, params string[] vars)
		{
			var result = new int[vars.Length];

			for (var i = 0; i < vars.Length; ++i)
			{
				var id = _namesIds[vars[i]];
				result[i] = _newColumnsPresentation[rowId][id];
			}

			return result;
		}

		public int[] GetArray(string varName)
		{
			if (!_namesIds.TryGetValue(varName, out var id))
				throw new System.ArgumentException($"Argument not found: {varName}");

			var result = new int[_newColumnsPresentation.Length];
			for (var i = 0; i < _newColumnsPresentation.Length; ++i)
			{
				result[i] = _newColumnsPresentation[i][id];
			}

			return result;
		}

		public int[] Translate(IDictionary<string, string> input)
		{
			var result = new int[input.Keys.Count];

			foreach (var (key, value) in input)
			{
				var index = _namesIds[key];
				result[index] = _mapBook[key][value];
			}

			return result;
		}

		// todo: could you make faster? (linear search).
		public string Translate(string name, int value)
		{
			return _mapBook[name].FirstOrDefault(x => x.Value == value).Key;
		}
	}
}
