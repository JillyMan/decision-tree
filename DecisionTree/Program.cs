using System.Collections.Generic;

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

            return 0;
        }

        static T LoadDate<T>(string path) where T : class
        {
            var data = System.IO.File.ReadAllText(path);
            return Serializer.Deserialize<T>(data);
        }
    }
}
