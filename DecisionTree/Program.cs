using System.Collections.Generic;
using System.Data;
using DecisionTree.Extensions;
using DecisionTree.Services;

namespace DecisionTree
{
	public class Line : Dictionary<string, string>
    {
    }

    class Program
    {
        static readonly string SourcePath = "C:\\Users\\Artsiom\\Documents\\Projects\\DecisionTree\\DecisionTree\\training_set.json";
        static readonly JsonSerializer Serializer = new JsonSerializer();

        static int Main(string[] args)
        {
			var data = new DataTable("Mitchell's Tennis Example");

			data.Columns.Add("Day", "Outlook", "Temperature", "Humidity", "Wind", "PlayTennis");

			data.Rows.Add("D1", "Sunny", "Hot", "High", "Weak", "No");
			data.Rows.Add("D2", "Sunny", "Hot", "High", "Strong", "No");
			data.Rows.Add("D3", "Overcast", "Hot", "High", "Weak", "Yes");
			data.Rows.Add("D4", "Rain", "Mild", "High", "Weak", "Yes");
			data.Rows.Add("D5", "Rain", "Cool", "Normal", "Weak", "Yes");
			data.Rows.Add("D6", "Rain", "Cool", "Normal", "Strong", "No");
			data.Rows.Add("D7", "Overcast", "Cool", "Normal", "Strong", "Yes");
			data.Rows.Add("D8", "Sunny", "Mild", "High", "Weak", "No");
			data.Rows.Add("D9", "Sunny", "Cool", "Normal", "Weak", "Yes");
			data.Rows.Add("D10", "Rain", "Mild", "Normal", "Weak", "Yes");
			data.Rows.Add("D11", "Sunny", "Mild", "Normal", "Strong", "Yes");
			data.Rows.Add("D12", "Overcast", "Mild", "High", "Strong", "Yes");
			data.Rows.Add("D13", "Overcast", "Hot", "Normal", "Weak", "Yes");
			data.Rows.Add("D14", "Rain", "Mild", "High", "Strong", "No");

			data.Filter("Outlook", "Sunny");

			return 0;
        }

        static T LoadDate<T>(string path) where T : class
        {
            var data = System.IO.File.ReadAllText(path);
            return Serializer.Deserialize<T>(data);
        }
    }
}
