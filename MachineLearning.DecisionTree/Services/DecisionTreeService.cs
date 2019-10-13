using System.Collections.Generic;
using MachineLearning.DecisionTree.Logger;
using MachineLearning.DecisionTree.Extensions;
using MachineLearning.DecisionTree.LearnAlgorithm;
using MachineLearning.Core.Converters;

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

        private readonly TreeInfo _treeInfo;
        private readonly ILogger _logger;
        private readonly ICodebook _codebook;
        private readonly IDecisionTreeBuilder _treeBuilder;

        public DecisionTreeService(
            TreeInfo treeInfo,
            ICodebook codebook,
            IDecisionTreeBuilder builder,
            ILogger logger)
        {
            _logger = logger;
            _codebook = codebook;
			_treeInfo = treeInfo;
            _treeBuilder = builder;

            Init();
        }

        private void Init()
        {
            _inputs = _codebook.GetArray(_treeInfo.Inputs);
            _outputs = _codebook.GetArray(_treeInfo.Output);
            _tree = _treeBuilder.Learn(_inputs, _outputs);
		}

        public string GetDecision(IDictionary<string, string> input)
        {
            var testInputs = _codebook.Translate(input);
            var computedResult = _tree.Compute(testInputs);
            var translatedResult = _codebook.Translate(_treeInfo.Output, computedResult);
            return translatedResult;
        }

		public bool CheckError()
		{
			var error = _tree.CheckError(_inputs, _outputs);
			if(error)
			{
				_logger.Info("---Success---");
			}
			else
			{
				_logger.Info("---Fail---");
			}
			return error;
		}

		public int GetDecision(int[] vector)
        {
            return _tree.Compute(vector);
        }
    }
}
