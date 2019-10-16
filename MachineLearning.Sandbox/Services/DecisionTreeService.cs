using MachineLearning.Core.Converters;
using MachineLearning.Core.Logger;
using MachineLearning.DecisionTree.LearnAlgorithm;
using MachineLearning.DecisionTree.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MachineLearning.DecisionTree.Services
{
	public class TreeInfo
	{
		public string[] Inputs { get; set; }
		public string Output { get; set; }
	}

	public class DecisionTreeService : IDecisionService
	{
		private readonly ILogger _logger;
		private readonly ICodebook _codebook;
		private readonly IDecisionTreeBuilder _treeBuilder;

		private readonly TreeInfo _treeInfo;
		private readonly Models.DecisionTree _tree;

		public DecisionTreeService(
			DataTable data,
			IDictionary<string, string[]> metaInfo,
			ILogger logger)
		{
			_logger = logger;
			_codebook = new Codebook(data, metaInfo);

			var vars = ParseMetaInfo(metaInfo);
			var inputInfo = vars.Take(vars.Length - 1).ToArray();
			var outputInfo = vars.Last();

			_treeInfo = new TreeInfo()
			{
				Inputs = inputInfo.Select(x => x.Name).ToArray(),
				Output = outputInfo.Name
			};

			var inputs = _codebook.GetArray(_treeInfo.Inputs);
			var outputs = _codebook.GetArray(_treeInfo.Output);

			_treeBuilder = new Id3Algorithm(inputInfo, outputInfo);
			_tree = _treeBuilder.Learn(inputs, outputs);
		}

		public string GetDecision(IDictionary<string, string> input)
		{
			var testInputs = _codebook.Translate(input);
			var computedResult = _tree.Compute(testInputs);
			var translatedResult = _codebook.Translate(_treeInfo.Output, computedResult);
			return translatedResult;
		}

		public int GetDecision(int[] vector)
		{
			return _tree.Compute(vector);
		}

		public bool CheckError()
		{
			var _inputs = _codebook.GetArray(_treeInfo.Inputs);
			var _outputs = _codebook.GetArray(_treeInfo.Output);

			if (_inputs == null || _outputs == null) return false;

			var result = true;
			for (var i = 0; i < _inputs.Length; ++i)
			{
				var res = _tree.Compute(_inputs[i]);
				result = result && res == _outputs[i];

				if (result) continue;

				var vector = string.Join(", ", _inputs[i].Select(x => x.ToString()));
				_logger.Info($"Crash for: {vector}");
			}

			return result;
		}

        public void DumpTree()
        {
            var deep = 0;
            PrintTree(_tree.Root, ref deep);
        }

        private void PrintTree(DecisionTreeNode root, ref int deep)
        {
            ++deep;

            PrintTab(deep);
            _logger.Log(root.IsLeaf ? $"({root.LeafInfo.Name})" : $"[{root.AttributeInfo.Name}]");

            foreach (var child in root.Childs)
            {
                PrintTab(deep);
                _logger.Log($"{root.AttributeInfo.Name}.{child.Branch.Name}");
                PrintTree(child, ref deep);
            }

            --deep;

            void PrintTab(int len)
            {
                for (var i = 0; i < len; ++i)
                {
                    _logger.Log("\t", false);
                }
            }
        }

		private DecisionVariable[] ParseMetaInfo(IDictionary<string, string[]> metaInfo)
		{
			return metaInfo.Select(x => new DecisionVariable(x.Key, x.Value)).ToArray();
		}
	}
}