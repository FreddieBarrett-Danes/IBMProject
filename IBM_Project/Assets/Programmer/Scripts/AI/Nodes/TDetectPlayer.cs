using UnityEngine;
using UnityEngine.AI;
using BT;

public class TDetectPlayer : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly Transform transform;
    private readonly BotInfo botInfo;
    
    public TDetectPlayer(NavMeshAgent pAgent, Transform pTransform, BotInfo pbotInfo)
    {
        agent = pAgent;
        transform = pTransform;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        if (!botInfo.engaging)
        {
            botInfo.timer += Time.deltaTime;
            Vector3 playerPos = botInfo.player.transform.position;
            Vector3 agentPos = agent.transform.position;
            float range = Vector3.Distance(playerPos, agentPos);
            Vector3 dirToTarget = (playerPos - agentPos).normalized;

            if (range < botInfo.viewRadius && Vector3.Angle(transform.forward, dirToTarget) < botInfo.viewAngle / 2)
            {
                botInfo.detectionTimer += Time.deltaTime;
                if (botInfo.detectionTimer >= 1.5)
                {
                    botInfo.timer = botInfo.wanderTimer;
                    botInfo.engaging = true;
                    botInfo.lastKnownPos = playerPos;
                    botInfo.playerInView = true;
                    state = NodeState.SUCCESS;
                    return state;
                }
                botInfo.playerInView = false;
                botInfo.engaging = false;
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        botInfo.playerInView = false;
        botInfo.engaging = false;
        state = NodeState.SUCCESS;
        return state;
    }

}
