using MachineLearning.Core.Converters;
using MachineLearning.Core.Logger;
using MachineLearning.DecisionTree.LearnAlgorithm;
using MachineLearning.DecisionTree.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MachineLearning.DecisionTree.Services
{
	//todo: pls refactor this class
	public class TreeInfo
	{
		public string[] Inputs { get; set; }
		public string Output { get; set; }
	}

	public class DecisionTreeService : IDecisionService
	{
		private int[] _outputs;
		private int[][] _inputs;

		private Models.DecisionTree _tree;

		private TreeInfo _treeInfo;
		private readonly ILogger _logger;
		private readonly ICodebook _codebook;
		private IDecisionTreeBuilder _treeBuilder;

		public DecisionTreeService(
			DataTable data,
			IDictionary<string, string[]> metaInfo,
			ILogger logger)
		{
			_logger = logger;
			_codebook = new Codebook(data, metaInfo);
			Init(metaInfo);
		}

		private void Init(IDictionary<string, string[]> metaInfo)
		{
			var vars = ParseMetaInfo(metaInfo);
			var inputInfo = vars.Take(vars.Length - 1).ToArray();
			var outputInfo = vars.Last();

			_treeInfo = new TreeInfo()
			{
				Inputs = inputInfo.Select(x => x.Name).ToArray(),
				Output = outputInfo.Name
			};

			_inputs = _codebook.GetArray(_treeInfo.Inputs);
			_outputs = _codebook.GetArray(_treeInfo.Output);
			_tree = _treeBuilder.Learn(_inputs, _outputs);

			_treeBuilder = new Id3Algorithm(inputInfo, outputInfo);
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

		private DecisionVariable[] ParseMetaInfo(IDictionary<string, string[]> metaInfo)
		{
			return metaInfo.Select(x => new DecisionVariable(x.Key, x.Value)).ToArray();
		}
	}
}
