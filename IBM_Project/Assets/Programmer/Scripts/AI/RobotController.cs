using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public abstract class RobotController : MonoBehaviour
{
    // Animation
    //protected Animator animator;

    // Wander
    [Header("Wander Settings")]
    public float wanderRadius;
    public float wanderTimer;
    protected float timer;

    // Lock On
    [Header("Targeting Settings")]
    public GameObject player;
    public float playerRadius;
    protected NavMeshAgent agent;

    // Health
    /*
    [Header("Health Settings")]
    public float health;
    public float maxHealth;
    public float projectileDMG;
    public GameObject healthBarUI;
    public Slider slider;*/
    protected bool stop;
    

    //protected AudioSource audioSource;

    void OnEnable()
    {
        //animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //audioSource = GetComponent<AudioSource>();
        timer = wanderTimer;
    }

    void Start()
    {
        //health = maxHealth;
        //slider.value = health / maxHealth;
        stop = false;
    }

    protected abstract void Update();
    /*
    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "Projectile(Clone)")
        {
            health =- projectileDMG;
            animator.SetBool("isDMG", true);
            stop = true;
        }
    }
    */
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randDir = Random.insideUnitSphere * distance;
        randDir += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, distance, layermask);
        return navHit.position;
    }


    protected void StopMove()
    {
        agent.SetDestination(agent.transform.position);
    }

    public void moveStart()
    {
        stop = false;
    }

    public void moveStop()
    {
        stop = true;
    }

    public void takeDMG()
    {
        //animator.SetBool("isDMG", false);
        stop = false;
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
