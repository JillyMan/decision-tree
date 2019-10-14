using MachineLearning.Core.Extensions;
using MachineLearning.Core.Statistics;
using MachineLearning.DecisionTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.DecisionTree.LearnAlgorithm
{
	public class Id3Algorithm : IDecisionTreeBuilder
	{
		private int[] _numberOfInputsRange;
		private int _numberOfOuputRange;
		private readonly Models.DecisionTree _tree;

		public Id3Algorithm(DecisionVariable[] inputs, DecisionVariable outputType)
		{
			_tree = new Models.DecisionTree(inputs, outputType);
			Init();
		}

		private void Init()
		{
			var attrLen = _tree.Attributes.Length;
			_numberOfInputsRange = new int[attrLen];
			_numberOfOuputRange = _tree.NumberOfOuputClasses;

			for (var i = 0; i < _numberOfInputsRange.Length; ++i)
			{
				_numberOfInputsRange[i] = _tree.Attributes[i].RangeLength;
			}
		}

		public Models.DecisionTree Learn(int[][] inputs, int[] outputs)
		{
			_tree.Root = new DecisionNode();
			var mappings = new int[_tree.Attributes.Length];

			for (int i = 0; i < mappings.Length; ++i)
			{
				mappings[i] = i;
			}

			Split(_tree.Root, inputs, outputs, mappings);
			return _tree;
		}

		private void Split(DecisionNode root, int[][] inputs, int[] outputs, int[] mappings)
		{
			var solveEntropy = Measure.CalcEntropy(outputs, _numberOfOuputRange);

			if (Math.Abs(solveEntropy) < double.Epsilon)
			{
				if (outputs.Length > 0)
				{
					root.Output = outputs[0];
					root.AttrName = _tree.SolveAttribute.NameRange[root.Output];
				}

				return;
			}

			var gainScores = new double[inputs[0].Length];

			for (var i = 0; i < gainScores.Length; ++i)
			{
				var realId = mappings[i];
				gainScores[i] = InformationGain(inputs, outputs, solveEntropy, i, realId);
			}

			gainScores.Max(out var maxGainAttrIndex);
			var realMaxGainIndex = mappings[maxGainAttrIndex];
			var newMappings = RecalculateMappings(mappings, maxGainAttrIndex);

			var currentAttribute = _tree.Attributes[realMaxGainIndex];
			root.AttrName = currentAttribute.Name;
			root.AttrIndex = realMaxGainIndex;

			var childCount = _numberOfInputsRange[realMaxGainIndex];
			var children = new DecisionNode[childCount];
			for (var i = 0; i < children.Length; ++i)
			{
				children[i] = new DecisionNode()
				{
					Branch = new Branch
					{
						Parent = root,
						Name = currentAttribute.NameRange[i],
						Value = i
					}
				};

				SplitLearnSet(
					inputs, outputs,
					maxGainAttrIndex, i,
					out var newInput, out var newOutput);

				Split(children[i], newInput, newOutput, newMappings);
			}

			root.Childs.AddRange(children);
		}

		private int[] RecalculateMappings(int[] oldMappings, int id)
		{
			var list = new List<int>(oldMappings);
			list.RemoveAt(id);
			return list.ToArray();
		}

		private double InformationGain(int[][] inputs, int[] outputs, double solveEntropy, int index, int realId)
		{
			return solveEntropy - InfoEntropy(inputs, outputs, index, realId);
		}

		private double InfoEntropy(int[][] inputs, int[] outputs, int index, int realId)
		{
			var valueFrequency = GetAttrValueFrequency(inputs, outputs, index, realId);

			var informationEntropy = 0d;
			for (var i = 0; i < valueFrequency.Length; ++i)
			{
				var count = valueFrequency[i].Sum(x => x);
				var e = Measure.EntropyByFreq(valueFrequency[i], count);
				informationEntropy += (count / (double)inputs.Length) * e;
			}

			return informationEntropy;
		}

		private int[][] GetAttrValueFrequency(int[][] inputs, int[] outputs, int attrIndex, int realId)
		{
			var attrRange = _numberOfInputsRange[realId];

			var valueFrequency = new int[attrRange][];
			for (var i = 0; i < valueFrequency.Length; ++i)
			{
				valueFrequency[i] = new int[_numberOfOuputRange];
			}

			for (var i = 0; i < inputs.Length; ++i)
			{
				var attrRowIndex = inputs[i][attrIndex];
				var outputColumnId = outputs[i];
				valueFrequency[attrRowIndex][outputColumnId]++;
			}

			return valueFrequency;
		}

		private static void SplitLearnSet(
			int[][] input, int[] output,
			int attrIndex, int value,
			out int[][] inputSubset, out int[] outputSubset)
		{
			var ll = new List<List<int>>();
			var outs = new List<int>();

			for (var j = 0; j < input.Length; ++j)
			{
				if (input[j][attrIndex] != value)
					continue;

				var list = new List<int>(input[j]);
				list.RemoveAt(attrIndex);
				ll.Add(list);
				outs.Add(output[j]);
			}

			inputSubset = ll.Select(x => x.ToArray()).ToArray();
			outputSubset = outs.ToArray();
		}
	}
}
