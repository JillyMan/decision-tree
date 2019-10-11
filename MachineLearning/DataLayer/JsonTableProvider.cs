using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MachineLearning.DataLayer
{
	public class JsonTableProvider : JsonSerializer, IDataProvider<IDictionary<string, string[]>>
	{
		public IDictionary<string, string[]> GetData(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path);
			}

			return JsonConvert.DeserializeObject<IDictionary<string, string[]>>(File.ReadAllText(path));
		}
	}
}
