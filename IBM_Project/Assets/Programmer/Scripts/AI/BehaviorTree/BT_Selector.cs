using System.Collections.Generic;

namespace BT
{
    public class BT_Selector : BT_Node
    {
        public BT_Selector() : base() { }
        public BT_Selector(List<BT_Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (BT_Node node in childrenList)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}
