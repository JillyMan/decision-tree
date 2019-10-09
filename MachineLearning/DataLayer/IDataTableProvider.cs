using System.Data;

namespace MachineLearning.DataLayer
{
    public interface IDataTableProvider
    {
        DataTable GetTable(string path);
    }
}
