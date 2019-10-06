using DecisionTree.Extensions;
using DecisionTree.Models;
using DecisionTree.Services.Builders;
using DecisionTree.Services.Converters;
using System;
using System.Collections.Generic;
using System.Data;

namespace DecisionTree
{
	static class DecisionTree
	{
		public static bool Check (this Models.DecisionTree tree, int[][] inputs, int[] outputs)
		{
			bool result = false;
			for(int i = 0; i < inputs.Length; ++i)
			{
				var res = tree.Compute(inputs[i]);
				result = result && (res != outputs[i]);
			}

			return result;
		}
	}

	class Program
	{
		static readonly string SourcePath = "C:\\Users\\Artsiom\\Documents\\Projects\\DecisionTree\\DecisionTree\\training_set.json";
		static readonly JsonSerializer Serializer = new JsonSerializer();

		static int Main(string[] args)
		{
			//var result = data.
			//	AsEnumerable().
			//	Select(x => x["Outlook"]).
			//	Distinct().Count();
			#region Setup data
			var data = new DataTable("Mitchell's Tennis Example");
			data.Columns.Add("Outlook", "Temperature", "Humidity", "Wind", "PlayTennis");

			data.Rows.Add("Sunny", "Hot", "High", "Weak", "No");
			data.Rows.Add("Sunny", "Hot", "High", "Strong", "No");
			data.Rows.Add("Overcast", "Hot", "High", "Weak", "Yes");
			data.Rows.Add("Rain", "Mild", "High", "Weak", "Yes");
			data.Rows.Add("Rain", "Cool", "Normal", "Weak", "Yes");
			data.Rows.Add("Rain", "Cool", "Normal", "Strong", "No");
			data.Rows.Add("Overcast", "Cool", "Normal", "Strong", "Yes");
			data.Rows.Add("Sunny", "Mild", "High", "Weak", "No");
			data.Rows.Add("Sunny", "Cool", "Normal", "Weak", "Yes");
			data.Rows.Add("Rain", "Mild", "Normal", "Weak", "Yes");
			data.Rows.Add("Sunny", "Mild", "Normal", "Strong", "Yes");
			data.Rows.Add("Overcast", "Mild", "High", "Strong", "Yes");
			data.Rows.Add("Overcast", "Hot", "Normal", "Weak", "Yes");
			data.Rows.Add("Rain", "Mild", "High", "Strong", "No");
			data.Filter("Outlook", "Sunny");
			#endregion

			var codebook = new Codebook(data);
			int[][] inputs = codebook.GetArray("Outlook", "Temperature", "Humidity", "Wind");
			int[] outputs = codebook.GetArray("PlayTennis");

			//need add to codebook GetVaraibleList() which investigate exists values. 
			//but you need specify haw mush range is have variable.
			//new DecisionVariable("Outtlook", 3);
			// and other information find codebook.
			var vars = new DecisionVariable[]
			{
				new DecisionVariable("Outlook", 3, new string[] { "Sunny", "Overcast", "Rain" }),
				new DecisionVariable("Temperature", 3, new string[] { "Hot", "Mild", "Cool" }),
				new DecisionVariable("Humidity", 2, new string[] { "High", "Normal" }),
				new DecisionVariable("Wind", 2, new string[] { "Weak", "Strong" }),
			};

			var tree = new ID3Builder(
				vars, 
				new DecisionVariable("Play Tennis", 2, new string[] { "No", "Yes" }))
				.Learn(inputs, outputs);

			//", "Hot", "High", "Weak"
			var sInput = new Dictionary<string, string>()
			{
				{ "Outlook", "Overcast"},
				{ "Temperature", "Hot"},
				{ "Humidity", "High"},
				{ "Wind", "Weak"},
			};

			var iInpus = codebook.Translate(sInput);
			var computedResult = tree.Compute(iInpus);
			var translatedResult = codebook.Translate("PlayTennis", computedResult);
			Console.WriteLine($"Result: {translatedResult}");

			var isError = tree.Check(inputs, outputs);

			return 0;
		}

		static T LoadDate<T>(string path) where T : class
		{
			var data = System.IO.File.ReadAllText(path);
			return Serializer.Deserialize<T>(data);
		}
	}
}
