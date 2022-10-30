using UnityEngine;

public class BotInfo : MonoBehaviour
{
    public int threatLevel;
    
    public Transform[] patrol;
    public int destPoint;
    public bool start;
    // Wander
    public float wanderRadius;
    public float wanderTimer;
    public float timer;
    
    // LockOn
    public GameObject player;
    public float viewRadius;
    [Range(0, 360)] 
    public float viewAngle;
    public bool engaging;

    // Suspicious
    public Vector3 lastKnownPos;
    public float suspiciousRadius;
    public float susTimer;
    public float stimer;
    public bool playerInView;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        destPoint = 0;
        timer = wanderTimer;
        stimer = susTimer;
    }

    public BotInfo()
    {
        threatLevel = 1;
        start = false;
        wanderRadius = 1f;
        wanderTimer = 1f;
        viewRadius = 1f;
        viewAngle = 1f;
        engaging = false;
        suspiciousRadius = 1f;
        susTimer = 1f;
        playerInView = false;
        Debug.Log("Constructor Call");
    }
}