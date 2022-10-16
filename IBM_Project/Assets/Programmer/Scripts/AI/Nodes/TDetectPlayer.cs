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
        if (BTInfo.engaging == false)
        {
            BTInfo.timer += Time.deltaTime;
            Vector3 playerPos = BTInfo.player.transform.position;
            Vector3 agentPos = agent.transform.position;
            float range = Vector3.Distance(playerPos, agentPos);
            Vector3 dirToTarget = (playerPos - agentPos).normalized;
            
            if (range < BTInfo.viewRadius && Vector3.Angle (transform.forward, dirToTarget) < BTInfo.viewAngle / 2)
            {
                BTInfo.timer = BTInfo.wanderTimer;
                BTInfo.engaging = true;
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }

}
