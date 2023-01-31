using UnityEngine;
using UnityEngine.AI;
using BT;

public class TWander : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;

    public TWander(NavMeshAgent pAgent, BotInfo pbotInfo)
    {
        agent = pAgent;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        botInfo.bTimer += Time.deltaTime;
        if (botInfo.bTimer >= botInfo.bWanderTimer)
        {
            Debug.Log("Wander State");
            Vector3 randomPoint = Random.insideUnitCircle * botInfo.bWanderRadius;
            randomPoint += Random.insideUnitSphere * botInfo.bWanderJitter;
            botInfo.bWanderTarget = botInfo.transform.position + botInfo.transform.forward * botInfo.bWanderDistance + randomPoint;
            NavMesh.SamplePosition(botInfo.bWanderTarget, out NavMeshHit navHit, botInfo.bWanderDistance, -1);
            agent.SetDestination(navHit.position);
            botInfo.bTimer = 0;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
