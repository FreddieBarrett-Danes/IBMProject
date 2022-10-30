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
        agent.SetDestination(botInfo.player.transform.position);
        botInfo.timer = botInfo.wanderTimer;
        botInfo.engaging = false;
        Debug.Log(botInfo.playerInView);
        state = NodeState.RUNNING;
        return state;
    }

}
