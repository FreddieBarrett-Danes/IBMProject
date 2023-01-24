using UnityEngine;
using UnityEngine.AI;
using BT;
using static System.Array;

public class TPatrol : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;
    private GameObject patrolPoints;

    public TPatrol(NavMeshAgent pAgent, BotInfo pbotInfo)
    {
        agent = pAgent;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        if (botInfo.createPoints == false)
        {
            patrolPoints = botInfo.transform.root.Find("PatrolPath").gameObject;
            Resize(ref botInfo.patrol, patrolPoints.transform.childCount);
            for (int i = 0; i < patrolPoints.transform.childCount; i++)
            {
                botInfo.patrol[i] = patrolPoints.transform.GetChild(i).transform;
            }
            botInfo.createPoints = true;
        }
        if (!botInfo.start)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.25f)
            {
                agent.destination = botInfo.patrol[botInfo.destPoint].position;
                switch (botInfo.pointLoop)
                {
                    case true:
                        botInfo.destPoint = (botInfo.destPoint + 1) % botInfo.patrol.Length;
                        break;
                    case false:
                        if (botInfo.pointInArray != botInfo.stopArray)
                        {
                            botInfo.destPoint = botInfo.pointInArray;
                            botInfo.pointInArray += botInfo.direction;
                            Debug.Log(botInfo.destPoint);
                            Debug.Log(botInfo.pointInArray);
                            Debug.Log(botInfo.direction);
                        }
                        break;
                }
            }
        }
        if (botInfo.engaging == false)
        {
            botInfo.playerInView = false;
            if (botInfo.patrol.Length == 0)
            {
                state = NodeState.FAILURE;
                return state;
            }
            if (!agent.pathPending && agent.remainingDistance < 0.25f)
            {
                agent.destination = botInfo.patrol[botInfo.destPoint].position;
                switch (botInfo.pointLoop)
                {
                    case true:
                        botInfo.destPoint = (botInfo.destPoint + 1) % botInfo.patrol.Length;
                        break;
                    case false:
                        // Not loop
                        break;
                }
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
