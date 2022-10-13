using UnityEngine;
using UnityEngine.AI;
public abstract class RobotController : MonoBehaviour
{
    // Wander
    [Header("Wander Settings")]
    public float wanderRadius;
    public float wanderTimer;
    private float timer;

    // Lock On
    [Header("Targeting Settings")]
    public GameObject player;
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    private NavMeshAgent agent;
    
    // Control
    public bool stop;
    
    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    private void Start()
    {
        stop = false;
    }

    protected abstract void Update();

    private static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * distance;
        randDir += origin;
        NavMesh.SamplePosition(randDir, out NavMeshHit navHit, distance, layerMask);
        return navHit.position;
    }
    
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) 
    {
        if (!angleIsGlobal) 
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void StopMove()
    {
        agent.SetDestination(agent.transform.position);
    }

    protected int AIMovement()
    {
        switch (stop)
        {
            // Movement
            case false:
            {
                timer += Time.deltaTime;
                Vector3 playerPos = player.transform.position;
                Vector3 agentPos = agent.transform.position;
                float range = Vector3.Distance(playerPos, agentPos);
                Vector3 dirToTarget = (playerPos - agentPos).normalized;
                
                // LockOn
                if (range < viewRadius && Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2)
                {
                    agent.SetDestination(player.transform.position);
                    timer = wanderTimer;
                    return 1;
                }

                // Wander
                if (timer >= wanderTimer)
                {
                    var newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0;
                    return 0;
                }

                // Idle
                if (agent.remainingDistance == 0)
                {
                    return 2;
                }
                break;
            }
            
            // Stopping
            case true:
                StopMove();
                return 3;
        }
        return 3;
    }
}
