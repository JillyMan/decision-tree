using System.Data;

namespace MachineLearning.DataLayer
{
    public interface IDataProvider<T>
    {
		T GetData(string path);
    }
}
