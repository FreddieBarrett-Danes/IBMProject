using UnityEngine;
using UnityEngine.AI;
using BT;

public class TWander : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly Transform transform;
    private readonly BotInfo botInfo;

    public TWander(NavMeshAgent pAgent, Transform pTransform, BotInfo pbotInfo)
    {
        agent = pAgent;
        transform = pTransform;
        botInfo = pbotInfo;
    }

    public override NodeState Evaluate()
    {
        botInfo.timer += Time.deltaTime;
        if (botInfo.timer >= botInfo.wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, botInfo.wanderRadius, -1);
            agent.SetDestination(newPos);
            botInfo.timer = 0;
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
