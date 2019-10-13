using FluentAssertions;
using MachineLearning.DataLayer.Extensions;
using MachineLearning.DecisionTree.Converters;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace MachineLearning.Test
{
	public class CodebookTest
	{
		[Fact]
		public void Convert_XOR_Set_To_ArrayInt()
		{
			var context = GetXorContext();
			var codebook = new Codebook(context, GetMetaInfo());

			var inputs = codebook.GetArray("x1", "x2");
			var outputs = codebook.GetArray("result");

			var result = inputs[0].SequenceEqual(new[] { 0, 0 }) &&
						 inputs[1].SequenceEqual(new[] { 0, 1 }) &&
						 inputs[2].SequenceEqual(new[] { 1, 0 }) &&
						 inputs[3].SequenceEqual(new[] { 1, 1 }) &&
						 outputs.SequenceEqual(new int[] { 0, 1, 1, 0 });

			result.Should().Be(true);
		}

		[Theory]
		[InlineData(
			new[] { "x1", "x2" },
			new[] { "0", "0" },
			new[] { 0, 0 })]
		[InlineData(
			new[] { "x1", "x2" },
			new[] { "1", "0" },
			new[] { 1, 0 })]
		[InlineData(
			new[] { "x1", "x2" },
			new[] { "0", "1" },
			new[] { 0, 1 })]
		[InlineData(
			new[] { "x1", "x2" },
			new[] { "1", "1" },
			new[] { 1, 1 })]
		public void Translate_XOR_Set_Return_String_Representation(string[] keys, string[] values, int[] expected)
		{
			var context = GetXorContext();
			var codebook = new Codebook(context, GetMetaInfo());

			var input = new Dictionary<string, string>();

			for (var i = 0; i < keys.Length; ++i)
			{
				input[keys[i]] = values[i];
			}

			var result = codebook.Translate(input);
			result.SequenceEqual(expected).Should().BeTrue();
		}

		[Fact]
		public void Translate_XOR_Set_By_Name_Value_Return_StringResult()
		{
			var context = GetXorContext();
			var codebook = new Codebook(context, GetMetaInfo());
			codebook.Translate("x1", 0).Should().Be("0");
		}

		private static DataTable GetXorContext()
		{
			var table = new DataTable();
			table.Columns.Add("x1", "x2", "result");
			table.Rows.Add("0", "0", "0");
			table.Rows.Add("0", "1", "1");
			table.Rows.Add("1", "0", "1");
			table.Rows.Add("1", "1", "0");
			return table;
		}

		private static IDictionary<string, string[]> GetMetaInfo()
		{
			return new Dictionary<string, string[]>
			{
				{ "x1", new[] {"0", "1" } },
				{ "x2", new[] {"0", "1" } },
				{ "result", new[] {"0", "1" } },
			};
		}
	}
}
