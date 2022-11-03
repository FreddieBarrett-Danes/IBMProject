namespace BT
{
    public class BT_Decorator : BT_Node
    {
        public BT_Decorator(BT_Node child) : base(child) { }

        public override NodeState Evaluate()
        {
            switch (childNode.Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    return state;
                case NodeState.SUCCESS:
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
                default:
                    state = NodeState.SUCCESS;
                    return state;
            }
        }
    }
}