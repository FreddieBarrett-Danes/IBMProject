using BT;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TPathToPlayer : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;
    private float timer = 0.0f;
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
        Vector3 bTargetPos = botInfo.bPlayer.transform.position + pVelocity * botInfo.bPredictionTime;
        botInfo.bRecentlyChase = true;
        if (botInfo.bPlayer.GetComponent<PlayerController>().failedHack)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                agent.SetDestination(bTargetPos);
                botInfo.bPlayer.GetComponent<PlayerController>().failedHack = false;
                timer = 0f;
            }
        }
        else
        {
            agent.SetDestination(bTargetPos);
        }
        botInfo.bTimer = botInfo.bWanderTimer;
        botInfo.bEngaging = false;
        state = NodeState.SUCCESS;
        return state;
    }

}
