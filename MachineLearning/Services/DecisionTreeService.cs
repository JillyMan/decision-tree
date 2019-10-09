using MachineLearning.Converters;
using MachineLearning.LearnAlgorithms;
using MachineLearning.Logger;
using System.Collections.Generic;

namespace MachineLearning.Services
{
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
            _treeInfo = treeInfo;
            _codebook = codebook;
            _treeBuilder = builder;

            Init();
        }

        private void Init()
        {
            _inputs = _codebook.GetArray(_treeInfo.Inputs);
            _outputs = _codebook.GetArray(_treeInfo.Output);
            _tree = _treeBuilder.Learn(_inputs, _outputs);
            var error = _tree.Check(_inputs, _outputs);
            _logger.Info($"Tree is build, with error: {error}");
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
    }
}
