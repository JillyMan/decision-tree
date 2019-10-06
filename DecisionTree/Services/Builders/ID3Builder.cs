using DecisionTree.Core;
using DecisionTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTree.Services.Builders
{
	public class ID3Builder : IDecisionTreeBuilder
	{
		private int[] _numberOfRange;
		private int _numberOfClasses;

		public Models.DecisionTree Tree { get; private set; }

		public ID3Builder(DecisionVariable[] inputs, DecisionVariable outputType)
		{
			Tree = new Models.DecisionTree(inputs, outputType);
			init(Tree);
		}

		public ID3Builder(int[] numberOfRange)
		{
			_numberOfRange = numberOfRange;
		}

		private void init(Models.DecisionTree tree)
		{
			int attrLen = tree.Attributes.Length;
			Tree = tree;
			_numberOfRange = new int[attrLen];
			_numberOfClasses = tree.NumberOfClasses;

			for (int i = 0; i < _numberOfRange.Length; ++i)
			{
				_numberOfRange[i] = tree.Attributes[i].RangeLength;
			}
		}

		public Models.DecisionTree Learn(int[][] inputs, int[] outputs)
		{
			Tree.Root = new Node();
			Split(Tree.Root, inputs, outputs);
			return Tree;
		}

		private void Split(Node root, int[][] inputs, int[] outputs)
		{
			double solveEntropy = Measure.CalcEntropy(outputs, _numberOfClasses);

			if(solveEntropy == 0)
			{
				if(outputs.Length > 0)
				{
					root.Output = outputs[0];
				}

				return;
			}

			var gainScores = new double[inputs.Length];

			for(var i = 0; i < gainScores.Length; ++i)
			{
				gainScores[i] = CalcInfoGain(inputs[i], outputs, i, solveEntropy);
			}

			gainScores.Max(out int maxGainAttrIndex);
			var childCount = _numberOfRange[maxGainAttrIndex];

			var children = new Node[childCount];
			for (int i = 0; i < children.Length; ++i)
			{
				children[i] = new Node()
				{
 					Value = i,
					Parent = root,
					AttrIndex = maxGainAttrIndex,
				};

				SplitLearnSet(
					inputs, outputs,
					maxGainAttrIndex, i,
					out int[][] newInput, out int[] newOutput);

				Split(children[i], newInput, newOutput);
			}

			root.Branches.AddRange(children);
		}

		private void SplitLearnSet(
			int[][] input, int[] output, 
			int attrIndex, int value,
			out int[][] inputSubset, out int[] outputSubset)
		{		
			var ll = new List<List<int>>();
			var outs = new List<int>();

			for (int j = 0; j < input.Length; ++j)
			{
				if (input[j][attrIndex] == value)
				{
					var list = new List<int>(input[j]);
					list.RemoveAt(attrIndex);
					ll.Add(list);
					outs.Add(output[attrIndex]);
				}
			}

			inputSubset = ll.Select(x => x.ToArray()).ToArray();
			outputSubset = outs.ToArray();
		}

		private double CalcInfoGain(int[] input, int[] outputs, int index, double solveEntropy)
		{
			return solveEntropy - InfoEntr(input, outputs, index);
		}

		//todo: check if len == 0
		private double InfoEntr(int[] attrValues, int[] outputs, int index)
		{
			double info = 0d;
			int attrRange = _numberOfRange[index];
			int outpRange = _numberOfClasses;

			int[][] freqs = new int[attrRange][];
			for(int i = 0; i < freqs.Length; ++i)
			{
				freqs[i] = new int[outpRange];
			}

			for (int i = 0; i < attrValues.Length; ++i)
			{
				freqs[attrValues[i]][outputs[i]]++;
			}

			for(int i = 0; i < freqs.Length; ++i)
			{
				int count = freqs[i].Sum(x => x);
				double e = Measure.Entropy(freqs[i], count);
				info += (count / (double)attrValues.Length) * e;
			}

			return info;
		}
	}
}
