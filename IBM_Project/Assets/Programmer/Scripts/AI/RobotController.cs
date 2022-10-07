using UnityEngine;
using UnityEngine.AI;
public abstract class RobotController : MonoBehaviour
{
    // Wander
    [Header("Wander Settings")]
    public float wanderRadius;
    public float wanderTimer;
    protected float timer;

    // Lock On
    [Header("Targeting Settings")]
    public GameObject player;
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    protected NavMeshAgent agent;
    
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

    protected static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        var randDir = Random.insideUnitSphere * distance;
        randDir += origin;
        NavMesh.SamplePosition(randDir, out var navHit, distance, layerMask);
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

    protected void StopMove()
    {
        agent.SetDestination(agent.transform.position);
    }
}
