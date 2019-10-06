using DecisionTree.Core;
using DecisionTree.Models;

namespace DecisionTree.Services.Builders
{
	public class ID3Builder : IDecisionTreeBuilder
	{
		private int[] _numberRanges;
		private int _numberOfClasses;
		private int _numberOfInputs;
		public Models.DecisionTree Tree { get; private set; }

		public ID3Builder(DecisionVariable[] vars)
		{
			Tree = new Models.DecisionTree(vars);
			init(Tree);
		}

		private void init(Models.DecisionTree tree)
		{
			int attrLen = tree.Attributes.Length;
			Tree = tree;
			_numberRanges = new int[attrLen];
			_numberOfClasses = tree.NumberOfClasses;
			_numberOfInputs = tree.NumerOfInputs;

			for (int i = 0; i < _numberRanges.Length; ++i)
			{
				_numberRanges[i] = tree.Attributes[i].RangeLength;
			}
		}

		public Models.DecisionTree Learn(int[][] inputs, int[] outputs)
		{
			Split(Tree.Root, inputs, outputs);
			return Tree;
		}

		private void Split(Node root, int[][] inputs, int[] outputs)
		{
			double entropy = Measure.Entropy(outputs, _numberOfClasses);

			if(entropy == 0)
			{
				if(outputs.Length > 0)
				{
					root.Output = outputs[0];
				}

				return;
			}

			var  gainScores   = new double	[inputs.Length	 ];
			var inputSubset  = new int	[inputs.Length -1][][];
			var outputSubset = new int	[inputs.Length -1][][];

			for(var i = 0; i < gainScores.Length; ++i)
			{
				gainScores[i] = CalcInfoGain(inputs, outputs, i, entropy,
					out inputSubset[i], out outputSubset[i]);
			}

			gainScores.Max(out int maxGainAttrIndex);
			var rangeLen = _numberRanges[maxGainAttrIndex];

			var children = new Node[rangeLen];
			for (int i = 0; i < children.Length; ++i)
			{
				children[i] = new Node()
				{
					AttrIndex = maxGainAttrIndex,
 					Value = i,
				};

				int[][] newInput = null;
				int[] newOuput = null;

				Split(children[i], newInput, newOuput);
			}

			root.Branches.AddRange(children);
		}

		private double CalcInfoGain(
			int[][] inputs, int[] outputs, 
			int attrIndex,
			double entropy, 
			out int[][] inputSubset, out int[][] outputSubset)
		{
			return entropy - InfoEntr(inputs, outputs, attrIndex, out inputSubset, out outputSubset);
		}

		private double InfoEntr(
			int[][] inputs, int[] outputs, 
			int attrIndex,
			out int[][] inputSubset, out int[][] outputSubset)
		{
			double info = 0d;
			int attrRange = _numberRanges[attrIndex];
			inputSubset = new int[attrRange][];
			outputSubset = new int[attrRange][];
			return info;
		}
	}
}
