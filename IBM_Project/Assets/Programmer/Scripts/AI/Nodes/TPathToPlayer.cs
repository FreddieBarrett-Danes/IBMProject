using BT;
using UnityEngine;
using UnityEngine.AI;

public class TPathToPlayer : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;
    public TPathToPlayer(NavMeshAgent pAgent, BotInfo pbotInfo)
    {
        agent = pAgent;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        if (!botInfo.bPlayer)
        {
            state = NodeState.FAILURE;
            return state;
        }
        Vector3 pVelocity = botInfo.bPlayer.GetComponent<PlayerController>().velocity;
        Debug.Log(pVelocity);
        Vector3 bTargetPos = botInfo.bPlayer.transform.position + pVelocity * botInfo.bPredictionTime;
        botInfo.bRecentlyChase = true;
        agent.SetDestination(bTargetPos);
        botInfo.bTimer = botInfo.bWanderTimer;
        botInfo.bEngaging = false;
        state = NodeState.SUCCESS;
        return state;
    }

}
