using UnityEngine;
using UnityEngine.AI;
using BT;

public class TPatrol : BT_Node
{
    private static NavMeshAgent agent;
    private static readonly GameObject patrolPoints = agent.gameObject.transform.Find("Patrol Path").gameObject;

    public TPatrol(NavMeshAgent pAgent)
    {
        agent = pAgent;
    }

    public override NodeState Evaluate()
    {
        for (int i = 0; i < patrolPoints.transform.childCount; i++)
        {
            BBTInfo.patrol[i] = patrolPoints.transform.GetChild(i).gameObject.transform;
        }
        if (!BBTInfo.start)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                agent.destination = BBTInfo.patrol[BBTInfo.destPoint].position;
                BBTInfo.destPoint = (BBTInfo.destPoint + 1) % BBTInfo.patrol.Length;
            }
        }
        if (BBTInfo.engaging == false)
        {
            if (BBTInfo.patrol.Length == 0)
            {
                state = NodeState.FAILURE;
                return state;
            }
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                agent.destination = BBTInfo.patrol[BBTInfo.destPoint].position;
                BBTInfo.destPoint = (BBTInfo.destPoint + 1) % BBTInfo.patrol.Length;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
