using UnityEngine;
using UnityEngine.AI;
using BT;

public class TRangedAttack : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;

    public TRangedAttack(NavMeshAgent pAgent, BotInfo pbotInfo)
    {
        agent = pAgent;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        Vector3 playerPos = botInfo.player.transform.position;
        Vector3 botPos = agent.transform.position;
        float range = Vector3.Distance(playerPos, botPos);
        Vector3 facing = (playerPos - botPos).normalized;
        float dotProd = Vector3.Dot(facing, agent.transform.forward);
        if (range >= (botInfo.viewRadius / 5) && botInfo.NextFire + botInfo.FireRate <= Time.time && dotProd > 0.99)
        {
            botInfo.NextFire = Time.time + botInfo.FireRate;
            botInfo.shooting.Execute();
        }
        
        state = NodeState.SUCCESS;
        return state;
    }
}