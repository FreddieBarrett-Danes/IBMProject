using BT;
using UnityEngine.AI;

public class PathToPlayer : BT_Node
{
    private readonly NavMeshAgent agent;
    public PathToPlayer(NavMeshAgent pAgent)
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
