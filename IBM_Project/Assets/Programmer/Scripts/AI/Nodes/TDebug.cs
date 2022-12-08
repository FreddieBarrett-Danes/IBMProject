using UnityEngine;
using UnityEngine.AI;
using BT;

public class TDebug : BT_Node
{
    public TDebug() { }

    public override NodeState Evaluate()
    {
        Debug.Log("Debug Node Trigger");
        state = NodeState.SUCCESS;
        return state;
    }

}
