using UnityEngine;

namespace BT
{
    public class BT_LoopDecorator : BT_Node
    {
        public BT_LoopDecorator(int loopTimes, BT_Node child) : base(loopTimes, child) { }
        
        public override NodeState Evaluate()
        {
            if (loopTimesInt > 2)
            {
                for (int i = 0; i < loopTimesInt; i++)
                {
                    //Debug.Log(i);
                    state = childNode.Evaluate() switch
                    {
                        NodeState.FAILURE => NodeState.FAILURE,
                        NodeState.SUCCESS => NodeState.SUCCESS,
                        NodeState.RUNNING => NodeState.RUNNING,
                        _ => NodeState.SUCCESS
                    };
                }
                //Debug.Log("FOR LOOP");
                return state;
            }
            //Debug.LogError("Inefficient use of LoopDecorator. Either its looping once or not at all. Use a regular Decorator node.");
            state = NodeState.FAILURE;
            return state;
        }
        
    }
}