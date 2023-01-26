using UnityEngine;
using BT;

public class TDetectPlayer : BT_Node
{
    private readonly BotInfo botInfo;
    private readonly Perception perception;
    
    public TDetectPlayer(BotInfo pBotInfo, Perception pPerception)
    {
        botInfo = pBotInfo;
        perception = pPerception;
    }

    public override NodeState Evaluate()
    {
        if (botInfo.bRecentlyChase)
        {
            botInfo.bRecentChaseTimer--;
            if (botInfo.bRecentChaseTimer <= 0)
            {
                botInfo.bRecentlyChase = false;
                botInfo.bRecentChaseTimer = botInfo.bRecentChaseTimerN;
            }
        }
        if (!botInfo.bEngaging)
        {
            Vector3 playerPos = new();
            botInfo.bTimer += Time.deltaTime;
            if (botInfo.bPlayer)
                playerPos = botInfo.bPlayer.transform.position;
            Vector3 agentPos = botInfo.transform.position;
            float range = Vector3.Distance(playerPos, agentPos);
            Vector3 toTarget = playerPos - agentPos;
            Vector3 dirToTarget = toTarget.normalized;

            if (range < botInfo.bViewRadius && Vector3.Angle(botInfo.transform.forward, dirToTarget) < botInfo.bViewAngle / 2 && !Physics.Raycast(botInfo.transform.position, dirToTarget, toTarget.magnitude, botInfo.bObstacleLayer))
            {
                botInfo.bDetectionTimer += Time.deltaTime;
                perception.ClearFoV();
                perception.AddMemory(botInfo.bPlayer.transform.gameObject);
                botInfo.bDebugLastKnownPos = botInfo.bPlayer.transform.position;
                botInfo.bPlayerInView = true;
                if (botInfo.bDetectionTimer >= 1.5)
                {
                    botInfo.bTimer = botInfo.bWanderTimer;
                    botInfo.bEngaging = true;
                    state = NodeState.SUCCESS;
                    return state;
                }
                botInfo.bPlayerInView = false;
                botInfo.bEngaging = false;
                state = NodeState.SUCCESS;
                return state;
            }
            botInfo.bPlayerInView = false;
            perception.ClearFoV();
            state = NodeState.FAILURE;
            return state;
        }
        perception.ClearFoV();
        botInfo.bPlayerInView = false;
        botInfo.bEngaging = false;
        state = NodeState.SUCCESS;
        return state;
    }

}
