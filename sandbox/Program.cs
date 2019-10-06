using Accord;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Filters;
using System.Data;
using System.Linq;

namespace sandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			DataTable data = new DataTable("Mitchell's Tennis Example");

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

			var codebook = new Codification(data);

			DataTable symbols = codebook.Apply(data);
			int[][] inputs = symbols.ToArray<int>("Outlook", "Temperature", "Humidity", "Wind");
			int[] outputs = symbols.ToArray<int>("PlayTennis");

			var id3learning = new ID3Learning()
			{

				new DecisionVariable("Outlook",     3), // 3 possible values (Sunny, overcast, rain)
				new DecisionVariable("Temperature", 3), // 3 possible values (Hot, mild, cool)  
				new DecisionVariable("Humidity",    2), // 2 possible values (High, normal)    
				new DecisionVariable("Wind",        2)  // 2 possible values (Weak, strong) 

			};

			DecisionTree tree = id3learning.Learn(inputs, outputs);

			double error = new ZeroOneLoss(outputs).Loss(tree.Decide(inputs));


			int[] query = codebook.Transform(new[,]
			{
				{ "Outlook",     "Sunny"  },
				{ "Temperature", "Hot"    },
				{ "Humidity",    "High"   },
				{ "Wind",        "Strong" }
			});

			int predicted = tree.Decide(query);  // result will be 0

			string answer = codebook.Revert("PlayTennis", predicted); // Answer will be: "No"
		}
	}
}
