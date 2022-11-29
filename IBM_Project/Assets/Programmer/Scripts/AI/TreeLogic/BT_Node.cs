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
            {
                child.parent = this;
                childrenList.Add(child);
            }
        }
        
        protected BT_Node(BT_Node child)
        {
            child.parent = this;
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }
}
