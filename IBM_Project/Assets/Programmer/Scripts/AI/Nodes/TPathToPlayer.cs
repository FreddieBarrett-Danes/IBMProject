using BT;
using UnityEngine.AI;

public class TPathToPlayer : BT_Node
{
    private readonly NavMeshAgent agent;
    public TPathToPlayer(NavMeshAgent pAgent)
    {
        agent = pAgent;
    }

    public override NodeState Evaluate()
    {
        agent.SetDestination(BBTInfo.player.transform.position);
        BBTInfo.timer = BBTInfo.wanderTimer;
        BBTInfo.engaging = false;
        state = NodeState.RUNNING;
        return state;
    }

}
