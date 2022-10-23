namespace BT
{
    public class BT_Decorator : BT_Node
    {
        public BT_Decorator(BT_Node child) : base(child) { }

        public override NodeState Evaluate()
        {
            bool ChildIsRunning = false;
            BT_Node node = null;
            
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    return state;
                case NodeState.SUCCESS:
                    break;
                case NodeState.RUNNING:
                    ChildIsRunning = true;
                    break;
                default:
                    state = NodeState.SUCCESS;
                    return state;
            }
            state = ChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}