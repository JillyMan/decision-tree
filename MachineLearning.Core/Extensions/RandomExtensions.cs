using System;

namespace MachineLearning.Core.Extensions
{
	public static class RandomExtensions
	{
		public static double NextDouble(this Random r, double min, double max)
		{
			return r.NextDouble() * (max - min) + max;
		}
	}
}
