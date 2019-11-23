using MachineLearning.Core.Logger;
using MachineLearning.DataLayer;
using System;

namespace MachineLearning.Sandbox
{
	class Program
	{
		static readonly Logger Logger = new Logger();
		static readonly FromCsvTableProvider CsvProvider = new FromCsvTableProvider();
		static readonly JsonTableProvider JsonProvider = new JsonTableProvider();

		static int Main()
		{
			return Console.ReadKey().KeyChar;
		}
	}
}