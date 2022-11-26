namespace BT
{
    public class BT_Decorator : BT_Node
    {
        public BT_Decorator(BT_Node decorator, BT_Node child) : base(decorator, child) { }

        public override NodeState Evaluate()
        {
            switch (decoratorNode.Evaluate())
            {
                case NodeState.SUCCESS:
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
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
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