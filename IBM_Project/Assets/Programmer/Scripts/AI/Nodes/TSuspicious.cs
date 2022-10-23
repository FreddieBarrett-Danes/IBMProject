using UnityEngine;
using UnityEngine.AI;
using BT;

public class TSuspicious : BT_Node
{
    private readonly NavMeshAgent agent;

    public TSuspicious(NavMeshAgent pAgent)
    {
        agent = pAgent;
    }

    public override NodeState Evaluate()
    {
        if (!BBTInfo.playerInView)
        {
            BBTInfo.stimer += Time.deltaTime;
            if (BBTInfo.stimer >= BBTInfo.susTimer)
            {
                Vector3 newPos = RandomNavSphere(BBTInfo.lastKnownPos, BBTInfo.suspiciousRadius, -1);
                Debug.Log("Searching for player at last known location!");
                agent.SetDestination(newPos);
                BBTInfo.stimer = 0;
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