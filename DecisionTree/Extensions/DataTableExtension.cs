using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DecisionTree.Extensions
{
	public static class DataTableExtension
	{
		public static void Add(this DataColumnCollection columns, params string[] attrs)
		{
			foreach(var attr in attrs)
			{
				columns.Add(attr);
			}
		}

		public static void AddRange(this DataTable table, DataRow[] range)
		{
			Array.ForEach(range, x => table.ImportRow(x));
			table.AcceptChanges();
		}

		public static DataTable Filter(this DataTable table, string byColumn, object value)
		{
			if (value == null) throw new ArgumentNullException();
			
			var filteredRows = table.Select($"{byColumn} = {TransformToQueryLang(value)}");
			var newTable = table.Copy();
			newTable.Columns.Remove(byColumn);
			newTable.Rows.Clear();
			newTable.AddRange(filteredRows);
			return newTable;
		}

		public static IEnumerable<DataRow> AsEnumerable(this DataTable table)
		{
			foreach(DataRow row in table.Rows)
			{
				yield return row;
			}
		}

		private static string TransformToQueryLang(object value)
		{
			var str = value.ToString();
			return value is string ? $"'{str}'" : $"{str}";
		}
	}
}
