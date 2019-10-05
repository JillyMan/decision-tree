using System.Data;
using System.Linq;
using DecisionTree.Extensions;
using DecisionTree.Services;
using DecisionTree.Services.Builders;

namespace DecisionTree
{

	class Program
    {
        static readonly string SourcePath = "C:\\Users\\Artsiom\\Documents\\Projects\\DecisionTree\\DecisionTree\\training_set.json";
        static readonly JsonSerializer Serializer = new JsonSerializer();

        static int Main(string[] args)
        {
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
			data.Rows.Add( "Rain", "Mild", "Normal", "Weak", "Yes");
			data.Rows.Add( "Sunny", "Mild", "Normal", "Strong", "Yes");
			data.Rows.Add( "Overcast", "Mild", "High", "Strong", "Yes");
			data.Rows.Add( "Overcast", "Hot", "Normal", "Weak", "Yes");
			data.Rows.Add( "Rain", "Mild", "High", "Strong", "No");
			data.Filter("Outlook", "Sunny");

			var result = data.
				AsEnumerable().
				Select(x => x["Outlook"]).
				Distinct().Count();

			var service = new DecisionTreeService(data,
				new ID3Builder(
					new Variable[] 
					{
					}));

			service.GetDecision(new Vector());

			return 0;
        }

        static T LoadDate<T>(string path) where T : class
        {
            var data = System.IO.File.ReadAllText(path);
            return Serializer.Deserialize<T>(data);
        }
    }
}
