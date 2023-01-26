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
        if (botInfo.bCreatePoints == false)
        {
            patrolPoints = botInfo.transform.GetChild(1).gameObject;
            Resize(ref botInfo.bPatrol, patrolPoints.transform.childCount);
            for (int i = 0; i < patrolPoints.transform.childCount; i++)
            {
                botInfo.bPatrol[i] = patrolPoints.transform.GetChild(i).transform;
            }
            botInfo.bCreatePoints = true;
        }
        if (!botInfo.bStart)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.25f)
            {
                agent.destination = botInfo.bPatrol[botInfo.bDestPoint].position;
                switch (botInfo.bPointLoop)
                {
                    case true:
                        botInfo.bDestPoint = (botInfo.bDestPoint + 1) % botInfo.bPatrol.Length;
                        break;
                    case false:
                        if (botInfo.bPointInArray != botInfo.bStopArray)
                        {
                            botInfo.bDestPoint = botInfo.bPointInArray;
                            botInfo.bPointInArray += botInfo.bDirection;
                            Debug.Log(botInfo.bDestPoint);
                            Debug.Log(botInfo.bPointInArray);
                            Debug.Log(botInfo.bDirection);
                        }
                        break;
                }
            }
        }
        if (botInfo.bEngaging == false)
        {
            botInfo.bPlayerInView = false;
            if (botInfo.bPatrol.Length == 0)
            {
                state = NodeState.FAILURE;
                return state;
            }
            if (!agent.pathPending && agent.remainingDistance < 0.25f)
            {
                agent.destination = botInfo.bPatrol[botInfo.bDestPoint].position;
                switch (botInfo.bPointLoop)
                {
                    case true:
                        botInfo.bDestPoint = (botInfo.bDestPoint + 1) % botInfo.bPatrol.Length;
                        break;
                    case false:
                        if (botInfo.bPointInArray != botInfo.bStopArray)
                        {
                            botInfo.bDestPoint = botInfo.bPointInArray;
                            botInfo.bPointInArray += botInfo.bDirection;
                            Debug.Log(botInfo.bDestPoint);
                            Debug.Log(botInfo.bPointInArray);
                            Debug.Log(botInfo.bDirection);
                        }
                        break;
                }
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
