using UnityEngine;
using UnityEngine.AI;
using BT;

public class TDetectPlayer : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly Transform transform;
    private readonly BotInfo botInfo;
    private readonly Perception perception;
    
    public TDetectPlayer(NavMeshAgent pAgent, Transform pTransform, BotInfo pbotInfo, Perception pperception)
    {
        agent = pAgent;
        transform = pTransform;
        botInfo = pbotInfo;
        perception = pperception;
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
                botInfo.visibleTargets.Clear();
                botInfo.detectionTimer += Time.deltaTime;
                if (botInfo.detectionTimer >= 1.5)
                {
                    botInfo.timer = botInfo.wanderTimer;
                    botInfo.engaging = true;
                    botInfo.visibleTargets.Add(botInfo.player.transform);
                    botInfo.playerInView = true;
                    perception.ClearFoV();
                    foreach(Transform target in botInfo.visibleTargets)
                    {
                        perception.AddMemory(target.gameObject);
                    }
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
