using System.IO;
using System.Collections.Generic;

namespace MachineLearning.DataLayer
{
	public class JsonProvider : IDataProvider<IDictionary<string, string[]>>
	{
		private readonly JsonSerializer serializer = new JsonSerializer();

		public IDictionary<string, string[]> GetData(string path)
		{
			return serializer.Deserialize<IDictionary<string, string[]>>(File.ReadAllText(path));
		}
	}
}
