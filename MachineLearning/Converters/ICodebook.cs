using System.Collections.Generic;

namespace MachineLearning.Converters
{
    public interface ICodebook
    {
        int[][] GetArray(params string[] vars);

        int[] GetArray(string varName);

        int[] Translate(IDictionary<string, string> input);

        string Translate(string name, int value);
    }
}
