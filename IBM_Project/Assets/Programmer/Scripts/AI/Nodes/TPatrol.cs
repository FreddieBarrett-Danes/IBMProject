using UnityEngine;
using UnityEngine.AI;
using BT;
using static System.Array;

public class TPatrol : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;

    public TPatrol(NavMeshAgent pAgent, BotInfo pbotInfo)
    {
        agent = pAgent;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        if (botInfo.bCreatePoints == false)
        {
            Resize(ref botInfo.bPatrol, botInfo.bPath.transform.childCount);
            for (int i = 0; i < botInfo.bPath.transform.childCount; i++)
            {
                botInfo.bPatrol[i] = botInfo.bPath.transform.GetChild(i).transform;
            }
            botInfo.bCreatePoints = true;
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
                        switch (botInfo.bDirection)
                        {
                            case true:
                                if (botInfo.bDestPoint > 0)
                                    botInfo.bDestPoint--;
                                else if (botInfo.bDestPoint == 0)
                                    botInfo.bDirection = false;
                                break;
                            case false:
                                if (botInfo.bDestPoint < botInfo.bPatrol.Length - 1)
                                    botInfo.bDestPoint++;
                                else if (botInfo.bDestPoint == botInfo.bPatrol.Length - 1)
                                    botInfo.bDirection = true;
                                break;
                        }
                        break;
                }
            }
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
