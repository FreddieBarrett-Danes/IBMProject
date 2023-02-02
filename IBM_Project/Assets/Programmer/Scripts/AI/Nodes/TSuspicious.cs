using UnityEngine;
using UnityEngine.AI;
using BT;

public class TSuspicious : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;
    private readonly Perception percep;

    public TSuspicious(NavMeshAgent pAgent, BotInfo pbotInfo, Perception pPercep)
    {
        agent = pAgent;
        botInfo = pbotInfo;
        percep = pPercep;
    }

    public override NodeState Evaluate()
    {
        botInfo.bViewRadius = botInfo.bSusViewRadius;
        botInfo.bSusTimer += Time.deltaTime;
        if (botInfo.bSusTimer >= botInfo.bSuspiciousTimer)
        {
            Vector3 randDir = Random.insideUnitSphere * botInfo.bSuspiciousRadius;
            randDir += percep.sensedRecord[0].lastSensedPosition;
            agent.SetDestination(randDir);
            botInfo.bSusTimer = 0;
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}