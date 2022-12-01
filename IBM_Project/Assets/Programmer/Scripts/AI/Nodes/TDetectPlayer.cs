using UnityEngine;
using UnityEngine.AI;
using BT;

public class TDetectPlayer : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly Transform transform;
    private readonly BotInfo botInfo;
    private readonly Perception perception;
    
    public TDetectPlayer(NavMeshAgent pAgent, Transform pTransform, BotInfo pBotInfo, Perception pPerception)
    {
        agent = pAgent;
        transform = pTransform;
        botInfo = pBotInfo;
        perception = pPerception;
    }

    public override NodeState Evaluate()
    {
        if (!botInfo.engaging)
        {
            Vector3 playerPos = new();
            botInfo.timer += Time.deltaTime;
            if (botInfo.player)
                playerPos = botInfo.player.transform.position;
            Vector3 agentPos = agent.transform.position;
            float range = Vector3.Distance(playerPos, agentPos);
            Vector3 toTarget = playerPos - agentPos;
            Vector3 dirToTarget = toTarget.normalized;

            if (range < botInfo.viewRadius && Vector3.Angle(transform.forward, dirToTarget) < botInfo.viewAngle / 2 && !Physics.Raycast(transform.position, dirToTarget, toTarget.magnitude, botInfo.ObstacleLayer))
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
                perception.ClearFoV();
                botInfo.playerInView = false;
                botInfo.engaging = false;
                state = NodeState.SUCCESS;
                return state;
            }
            perception.ClearFoV();
            state = NodeState.FAILURE;
            return state;
        }
        perception.ClearFoV();
        botInfo.playerInView = false;
        botInfo.engaging = false;
        state = NodeState.SUCCESS;
        return state;
    }

}
