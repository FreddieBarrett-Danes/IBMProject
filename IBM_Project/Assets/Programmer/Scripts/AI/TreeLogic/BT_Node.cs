using System.Collections.Generic;

namespace BT
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class BT_Node
    {
        protected NodeState state;

        private BT_Node parent;
        protected readonly List<BT_Node> childrenList = new();

        protected BT_Node()
        {
            parent = null;
        }

        protected BT_Node(List<BT_Node> children)
        {
            foreach (BT_Node child in children)
                Attach(child);
        }

        private void Attach(BT_Node btNode)
        {
            btNode.parent = this;
            childrenList.Add(btNode);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }
}
