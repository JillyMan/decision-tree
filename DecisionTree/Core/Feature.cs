using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTree.Core
{
	public class Feature
	{
		public double InformationEntropy { get; }

		public Feature(ValueOfFeature[] values)
		{
			InformationEntropy = CalculateInfoEntropy(values);
		}

		private double CalculateInfoEntropy(ValueOfFeature[] values)
		{
			var infoEntropy = 0d;
			var linesCount = values.Sum(x => x.Qty);

			for (var i = 0; i < values.Length; ++i)
			{
				var probabilityValuei = (double)values[i].Qty / linesCount;
				infoEntropy += probabilityValuei * values[i].Entropy;
			}

			return infoEntropy;
		}
	}
}