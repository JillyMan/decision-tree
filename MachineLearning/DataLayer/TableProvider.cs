using System.Data;
using MachineLearning.Extensions;

namespace MachineLearning.DataLayer
{
    public class JsonTableProvider : JsonSerializer, IDataTableProvider
    {
        public DataTable GetTable(string path)
        {
            var data = new DataTable("Mitchell's Tennis Example");
            data.Columns.Add("Outlook", "Temperature", "Humidity", "Wind", "Play Tennis");
            data.Rows.Add("Sunny", "Hot", "High", "Weak", "No");
            data.Rows.Add("Sunny", "Hot", "High", "Strong", "No");
            data.Rows.Add("Overcast", "Hot", "High", "Weak", "Yes");
            data.Rows.Add("Rain", "Mild", "High", "Weak", "Yes");
            data.Rows.Add("Rain", "Cool", "Normal", "Weak", "Yes");
            data.Rows.Add("Rain", "Cool", "Normal", "Strong", "No");
            data.Rows.Add("Overcast", "Cool", "Normal", "Strong", "Yes");
            data.Rows.Add("Sunny", "Mild", "High", "Weak", "No");
            data.Rows.Add("Sunny", "Cool", "Normal", "Weak", "Yes");
            data.Rows.Add("Rain", "Mild", "Normal", "Weak", "Yes");
            data.Rows.Add("Sunny", "Mild", "Normal", "Strong", "Yes");
            data.Rows.Add("Overcast", "Mild", "High", "Strong", "Yes");
            data.Rows.Add("Overcast", "Hot", "Normal", "Weak", "Yes");
            data.Rows.Add("Rain", "Mild", "High", "Strong", "No");
            return data;
        }
    }
}
