using System;
using System.Collections.Generic;
using System.Linq;
using MachineLearning.Core;
using MachineLearning.Extensions;
using MachineLearning.Models;

namespace MachineLearning.LearnAlgorithm
{
    public class Id3Algorithm : IDecisionTreeBuilder
    {
        private int[] _numberOfRange;
        private int _numberOfClasses;
        private readonly DecisionTree _tree;

        public Id3Algorithm(DecisionVariable[] inputs, DecisionVariable outputType)
        {
            _tree = new DecisionTree(inputs, outputType);
            Init();
        }

        private void Init()
        {
            var attrLen = _tree.Attributes.Length;
            _numberOfRange = new int[attrLen];
            _numberOfClasses = _tree.NumberOfClasses;

            for (var i = 0; i < _numberOfRange.Length; ++i)
            {
                _numberOfRange[i] = _tree.Attributes[i].RangeLength;
            }
        }

        public DecisionTree Learn(int[][] inputs, int[] outputs)
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
            var solveEntropy = Measure.CalcEntropy(outputs, _numberOfClasses);

            if (Math.Abs(solveEntropy) < double.Epsilon)
            {
				if (outputs.Length > 0)
				{
					root.Output = outputs[0];
					root.Name = _tree.SolveAttribute.NameRange[root.Index];
				}

				return;
            }

            var gainScores = new double[inputs[0].Length];

            for (var i = 0; i < gainScores.Length; ++i)
            {
				var realId = mappings[i];
                gainScores[i] = CalcInformationGain(inputs, outputs, solveEntropy, i, realId);
            }

            gainScores.Max(out var maxGainAttrIndex);
			var realMaxGainIndex = mappings[maxGainAttrIndex];
			var newMappings = RecalculateMappings(mappings, maxGainAttrIndex);

			var currentAttribute = _tree.Attributes[realMaxGainIndex];
			root.Name = currentAttribute.Name;

			var childCount = _numberOfRange[realMaxGainIndex];
			var children = new DecisionNode[childCount];
			for (var i = 0; i < children.Length; ++i)
            {
                children[i] = new DecisionNode()
                {
                    Index = i,
                    Parent = root,
                    Name = currentAttribute.NameRange[i],
                    AttrIndex = realMaxGainIndex,
                };

                SplitLearnSet(
                    inputs, outputs,
                    maxGainAttrIndex, i,
                    out var newInput, out var newOutput);

                Split(children[i], newInput, newOutput, newMappings);
            }

            root.Branches.AddRange(children);
        }

		private int[] RecalculateMappings(int[] oldMappings, int id)
		{
			var list = new List<int>(oldMappings);
			list.RemoveAt(id);
			return list.ToArray();
		}

        private double CalcInformationGain(int[][] inputs, int[] outputs, double solveEntropy, int index, int realId)
        {
            return solveEntropy - InfoEntropy(inputs, outputs, index, realId);
        }

        //todo: check if len == 0
        private double InfoEntropy(int[][] attrValues, int[] outputs, int index, int realId)
        {
            var informationEntropy = 0d;
            var attrRange = _numberOfRange[realId];
            var outputRange = _numberOfClasses;

            var valueFrequency = new int[attrRange][];
            for (var i = 0; i < valueFrequency.Length; ++i)
            {
                valueFrequency[i] = new int[outputRange];
            }

            for (var i = 0; i < attrValues.Length; ++i)
            {
                valueFrequency[attrValues[i][index]][outputs[i]]++;
            }

            for (var i = 0; i < valueFrequency.Length; ++i)
            {
                var count = valueFrequency[i].Sum(x => x);
                var e = Measure.Entropy(valueFrequency[i], count);
                informationEntropy += (count / (double)attrValues.Length) * e;
            }

            return informationEntropy;
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
