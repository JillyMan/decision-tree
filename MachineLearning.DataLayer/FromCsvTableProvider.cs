using System.Data;
using System.IO;
using System.Linq;
using MachineLearning.DataLayer.Extensions;

namespace MachineLearning.DataLayer
{
	public class FromCsvTableProvider : IDataProvider<DataTable>
	{
		public DataTable GetData(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path);
			}

			var stringData = File.ReadAllText(path);
			var lines = Csv.CsvReader.ReadFromText(stringData).ToArray();
			var result = new DataTable();

			result.Columns.Add(lines[0].Headers);
		
			foreach (var line in lines)
			{
				result.Rows.Add(line.Values);
			}

			return result;
		}
    }
}
