using UnityEngine;
using UnityEngine.AI;
using BT;

public class TWander : BT_Node
{
    private readonly NavMeshAgent agent;
    private readonly Transform transform;

    public TWander(NavMeshAgent pAgent, Transform pTransform)
    {
        agent = pAgent;
        transform = pTransform;
    }

    public override NodeState Evaluate()
    {
        BTInfo.timer += Time.deltaTime;
        if (BTInfo.timer >= BTInfo.wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, BTInfo.wanderRadius, -1);
            agent.SetDestination(newPos);
            BTInfo.timer = 0;
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
