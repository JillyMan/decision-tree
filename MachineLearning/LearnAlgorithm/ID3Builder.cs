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
            Split(_tree.Root, inputs, outputs);
            return _tree;
        }

        private void Split(DecisionNode root, int[][] inputs, int[] outputs)
        {
            var solveEntropy = Measure.CalcEntropy(outputs, _numberOfClasses);

            if (Math.Abs(solveEntropy) < double.Epsilon)
            {
                if (outputs.Length <= 0) return;

                root.Output = outputs[0];
                root.Name = _tree.SolveAttribute.NameRange[root.Index];

                return;
            }

            var gainScores = new double[inputs[0].Length];

            for (var i = 0; i < gainScores.Length; ++i)
            {
                gainScores[i] = CalcInformationGain(inputs, outputs, i, solveEntropy);
            }

            gainScores.Max(out var maxGainAttrIndex);
            var childCount = _numberOfRange[maxGainAttrIndex];

            var children = new DecisionNode[childCount];
            for (var i = 0; i < children.Length; ++i)
            {
                children[i] = new DecisionNode()
                {
                    Index = i,
                    Parent = root,
                    Name = _tree.Attributes[maxGainAttrIndex].NameRange[i],
                    AttrIndex = maxGainAttrIndex,
                };

                SplitLearnSet(
                    inputs, outputs,
                    maxGainAttrIndex, i,
                    out var newInput, out var newOutput);

                Split(children[i], newInput, newOutput);
            }

            root.Branches.AddRange(children);
        }

        private double CalcInformationGain(int[][] inputs, int[] outputs, int index, double solveEntropy)
        {
            return solveEntropy - InfoEntropy(inputs, outputs, index);
        }

        //todo: check if len == 0
        private double InfoEntropy(int[][] attrValues, int[] outputs, int index)
        {
            var informationEntropy = 0d;
            var attrRange = _numberOfRange[index];
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
