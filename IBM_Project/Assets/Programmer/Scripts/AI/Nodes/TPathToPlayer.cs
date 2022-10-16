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
        agent.SetDestination(BTInfo.player.transform.position);
        BTInfo.timer = BTInfo.wanderTimer;
        BTInfo.engaging = false;
        state = NodeState.RUNNING;
        return state;
    }

}
