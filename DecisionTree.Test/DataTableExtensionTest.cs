using DecisionTree.Extensions;
using FluentAssertions;
using System.Data;
using Xunit;

namespace DecisionTree.Test
{
	public class DataTableExtensionTest
	{
		[Theory]
		[InlineData("Outlook", "Rain", 3)]
		[InlineData("Numbers", 1, 2)]
		public void FilterData_CountShouldEqualBeExpected(string col, object value, int expected)
		{
			var table = GetContext();
			var filtered = table.Filter(col, value);
			filtered.Columns.Contains(col).Should().BeFalse();
			filtered.Rows.Count.Should().Be(expected);
		}

		public DataTable GetContext()
			{
			var table = new DataTable();
			table.Columns.Add("Outlook", "Temperature", "Humidity", "Numbers", "PlayTennis");
			table.Rows.Add("Sunny", "Hot", "High",					"1", "No");
			table.Rows.Add("Sunny", "Hot", "High",					"2", "No");
			table.Rows.Add("Overcast", "Hot", "High",				"2", "Yes");
			table.Rows.Add("Rain", "Mild", "High",					"3", "Yes");
			table.Rows.Add("Rain", "Cool", "Normal",				"1", "Yes");
			table.Rows.Add("Rain", "Cool", "Normal",				"2", "No");
			return table;
		}
	}
}
