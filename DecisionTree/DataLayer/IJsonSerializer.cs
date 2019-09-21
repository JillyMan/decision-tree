namespace DecisionTree
{

    partial class Program
    {
        interface IJsonSerializer
        {
            string Serialize(object obj);

            T Deserialize<T>(string obj) where T : class;
        }
    }
}
