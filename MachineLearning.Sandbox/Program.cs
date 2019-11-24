using MachineLearning.Core.Logger;
using MachineLearning.DataLayer;
using System;

namespace MachineLearning.Sandbox
{
	class Program
	{
		static readonly Logger Logger = new Logger();
		static readonly FromCsvTableProvider CsvProvider = new FromCsvTableProvider();
		static readonly JsonProvider JsonProvider = new JsonProvider();

		static int Main()
		{
			NeuronNetworkProgram.Run();
			return Console.ReadKey().KeyChar;
		}
	}
}