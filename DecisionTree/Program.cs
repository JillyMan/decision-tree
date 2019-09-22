namespace DecisionTree
{
	public class Line
    {
        public string Temp { get; set; }

		public string Wind { get; set; }

        public string Outlook { get; set; }

		public string Humidity { get; set; }

		public string Decision { get; set; }
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
