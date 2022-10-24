using UnityEngine;
using UnityEngine.AI;
using BT;

public class TDetectPlayer : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly Transform transform;
    
    public TDetectPlayer(NavMeshAgent pAgent, Transform pTransform)
    {
        agent = pAgent;
        transform = pTransform;
    }

    public override NodeState Evaluate()
    {
        if (BBTInfo.engaging == false)
        {
            BBTInfo.timer += Time.deltaTime;
            Vector3 playerPos = BBTInfo.player.transform.position;
            Vector3 agentPos = agent.transform.position;
            float range = Vector3.Distance(playerPos, agentPos);
            Vector3 dirToTarget = (playerPos - agentPos).normalized;

            if (range < BBTInfo.viewRadius && Vector3.Angle(transform.forward, dirToTarget) < BBTInfo.viewAngle / 2)
            {
                BBTInfo.timer = BBTInfo.wanderTimer;
                BBTInfo.engaging = true;
                BBTInfo.lastKnownPos = playerPos;
                BBTInfo.playerInView = true;
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        BBTInfo.playerInView = false;
        state = NodeState.SUCCESS;
        return state;
    }

}
