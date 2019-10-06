namespace DecisionTree.Services.Builders
{
	public static class ArrayExtensions
	{
		public static double Max(this double[] array, out int index)
		{
			double max = array[0];
			index = 0;

			for (int i = 0; i < array.Length; ++i)
			{
				if(array[i] > max)
				{
					index = i;
					max = array[i];
				} 
			}
			
			return max;
		}
	}
}
