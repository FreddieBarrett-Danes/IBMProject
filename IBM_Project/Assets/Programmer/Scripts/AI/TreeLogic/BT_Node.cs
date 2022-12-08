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
        protected readonly int loopTimesInt;
        protected readonly BT_Node decoratorNode;
        protected readonly BT_Node childNode;

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
        
        protected BT_Node(BT_Node decorator, BT_Node child)
        {
            decorator.parent = this;
            child.parent = this;
            decoratorNode = decorator;
            childNode = child;
        }
        
        protected BT_Node(int timesToLoop, BT_Node child)
        {
            loopTimesInt = timesToLoop;
            child.parent = this;
            childNode = child;
        }

        protected BT_Node(BT_Node child)
        {
            child.parent = this;
            childNode = child;
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }
}
