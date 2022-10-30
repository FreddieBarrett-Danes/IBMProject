using UnityEngine;
using UnityEngine.AI;
using BT;

public class TSuspicious : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;

    public TSuspicious(NavMeshAgent pAgent, BotInfo pbotInfo)
    {
        agent = pAgent;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Entering Suspicious State!");
        if (!botInfo.playerInView)
        {
            botInfo.stimer += Time.deltaTime;
            if (botInfo.stimer >= botInfo.susTimer)
            {
                Vector3 newPos = RandomNavSphere(botInfo.lastKnownPos, botInfo.suspiciousRadius, -1);
                Debug.Log("Searching for player at last known location!");
                agent.SetDestination(newPos);
                botInfo.stimer = 0;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
    
    private static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * distance;
        randDir += origin;
        NavMesh.SamplePosition(randDir, out NavMeshHit navHit, distance, layerMask);
        return navHit.position;
    }
}