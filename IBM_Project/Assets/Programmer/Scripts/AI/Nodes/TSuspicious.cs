using UnityEngine;
using UnityEngine.AI;
using BT;

public class TSuspicious : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;
    private readonly Perception percep;

    public TSuspicious(NavMeshAgent pAgent, BotInfo pBotInfo, Perception pPercep)
    {
        agent = pAgent;
        botInfo = pBotInfo;
        percep = pPercep;
    }

    public override NodeState Evaluate()
    {
        botInfo.bViewRadius = botInfo.bSusViewRadius;
        botInfo.bInnerViewRadius = botInfo.bSusInnerViewRadius;
        botInfo.bViewCone.GetComponent<Light>().color = Color.yellow;
        botInfo.bSusTimer += Time.deltaTime;
        if (botInfo.bSusTimer >= botInfo.bSuspiciousTimer || agent.remainingDistance < 0.05)
        {
            Vector3 randDir;
            if (botInfo.bComputer.GetComponent<ComputerInteraction>().mazeFailed == false)
            {
                if (percep.sensedRecord.Length != 0)
                {
                    randDir = Random.insideUnitSphere * botInfo.bSuspiciousRadius;
                    randDir += percep.sensedRecord[0].lastSensedPosition;
                }
                else
                {
                    randDir = Random.insideUnitSphere * botInfo.bSuspiciousRadius;
                    randDir += botInfo.transform.position;
                }
            }
            else
            {
                randDir = Random.insideUnitSphere * botInfo.bComputerSusRadius;
                randDir += botInfo.bComputer.transform.position;
            }
            agent.SetDestination(randDir);
            botInfo.bSusTimer = 0;
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}