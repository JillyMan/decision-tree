using System.Collections.Generic;
using System.Linq;

namespace MachineLearning.DecisionTree.Models
{
	public struct NodeInfo
    {
        public int Index;

		public string Name;
    }

    public class DecisionTreeNode
    {
        public NodeInfo Branch;

        public NodeInfo AttributeInfo;

        public NodeInfo LeafInfo;

        public DecisionTreeNode Parent;

        public bool IsLeaf => Childs.Count == 0;

		public List<DecisionTreeNode> Childs { get; } = new List<DecisionTreeNode>();

        public DecisionTreeNode GetNodeByBranchValue(int branchIndex)
        {
            return IsLeaf ? this : Childs.FirstOrDefault(e => e.Branch.Index == branchIndex);
        }
	}
}