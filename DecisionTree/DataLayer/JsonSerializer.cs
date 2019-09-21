namespace DecisionTree
{
    partial class Program
    {
        public class JsonSerializer : IJsonSerializer
        {
            public string Serialize(object obj)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }

            public T Deserialize<T>(string obj) where T : class
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(obj) as T;
            }
        }
    }
}
