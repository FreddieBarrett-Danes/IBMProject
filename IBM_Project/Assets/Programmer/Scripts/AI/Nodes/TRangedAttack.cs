using UnityEngine;
using UnityEngine.AI;
using BT;

public class TRangedAttack : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly BotInfo botInfo;

    public TRangedAttack(NavMeshAgent pAgent, BotInfo pbotInfo)
    {
        agent = pAgent;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        foreach (Component t in botInfo.abilitiesList)
        {
            if (t.GetType().ToString() == "Shooting")
            {
                Vector3 playerPos = botInfo.player.transform.position;
                Vector3 botPos = agent.transform.position;
                float range = Vector3.Distance(playerPos, botPos);
                Vector3 facing = (playerPos - botPos).normalized;
                float dotProd = Vector3.Dot(facing, agent.transform.forward);
                if (range >= (botInfo.viewRadius / 4) && botInfo.nextFire <= Time.time && dotProd > 0.95)
                {
                    botInfo.nextFire = Time.time + botInfo.fireRate;
                    Debug.Log(botInfo.abilitiesList[0].GetComponent<Ability>().name);
                    botInfo.abilitiesList[0].GetComponent<Ability>().Execute();
                }
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}