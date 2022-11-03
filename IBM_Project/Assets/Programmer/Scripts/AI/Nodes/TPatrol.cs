using UnityEngine;
using UnityEngine.AI;
using BT;
using static System.Array;

public class TPatrol : BT_Node
{
    private static NavMeshAgent agent;
    private static BotInfo botInfo;
    private static GameObject patrolPoints;
    public TPatrol(NavMeshAgent pAgent, BotInfo pbotInfo)
    {
        agent = pAgent;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        patrolPoints = botInfo.transform.root.Find("PatrolPath").gameObject;
        Resize(ref botInfo.patrol, patrolPoints.transform.childCount);
        for (int i = 0; i < patrolPoints.transform.childCount; i++)
        {
            botInfo.patrol[i] = patrolPoints.transform.GetChild(i).transform;
        }
        if (!botInfo.start)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                agent.destination = botInfo.patrol[botInfo.destPoint].position;
                botInfo.destPoint = (botInfo.destPoint + 1) % botInfo.patrol.Length;
            }
        }
        if (botInfo.engaging == false)
        {
            if (botInfo.patrol.Length == 0)
            {
                state = NodeState.FAILURE;
                return state;
            }
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                agent.destination = botInfo.patrol[botInfo.destPoint].position;
                botInfo.destPoint = (botInfo.destPoint + 1) % botInfo.patrol.Length;
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
