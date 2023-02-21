using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BotInfo : MonoBehaviour
{
    // Misc
    [Header("Misc Settings")]
    public int bThreatLevel;
    public int bBotCount;
    public int bRemainingBots;
    public GameController bGameControl;
    public LayerMask bObstacleLayer;
    public List<Component> bAbilitiesList;
    private bool bAbilityAdd;
    public float bRotationSpeed;
    public GameObject Computer;

    // Range Attack
    [Header("Ranged Attack Settings")] 
    public float bFireRate;
    [HideInInspector]
    public float bProjectileSpeed;
    [HideInInspector]
    public float bNextFire;
    [HideInInspector]
    public Shooting bShooting;
    public float bMaxFireDist;
    private GameObject bVisuals;

    // Patrol
    [Header("Patrol Settings")] 
    public GameObject bPath;
    public bool bPointLoop;
    [HideInInspector]
    public bool bCreatePoints;
    [HideInInspector]
    public Transform[] bPatrol;
    [HideInInspector]
    public int bDestPoint;
    [HideInInspector]
    public bool bDirection;

    // Wander
    [Header("Wander Settings")] 
    [HideInInspector]
    public bool bRecentlyChase;
    public float bWanderRadius;
    public float bWanderTimer;
    public float bWanderDistance;
    public float bWanderJitter;
    [HideInInspector]
    public Vector3 bWanderTarget;
    [HideInInspector]
    public float bTimer;
    
    // LockOn
    [Header("Player Chase Settings")]
    public GameObject bPlayer;
    [HideInInspector]
    public float bViewRadius;
    [HideInInspector]
    public float bInnerViewRadius;
    public float bDefaultViewRadius;
    public float bDefaultInnerViewRadius;
    public float bSusViewRadius;
    public float bSusInnerViewRadius;
    [Range(0, 360)] 
    public float bViewAngle;
    public float bDetectionTimer;
    public int bTimeBeforeDetect;
    [HideInInspector]
    public bool bEngaging;
    public float bPredictionTime;
    public float bRecentChaseTimer;
    [HideInInspector]
    public float bRecentChaseTimerN;
    
    // Suspicious
    [Header("Suspicious Settings")]
    public float bSuspiciousRadius;
    public float bSuspiciousTimer;
    public int bSearchTime;
    [HideInInspector]
    public Vector3 bDebugLastKnownPos;
    [HideInInspector]
    public float bSusTimer;
    [HideInInspector]
    public bool bPlayerInView;
    
    //ViewCone
    [Header("ViewCone Settings")]
    private GameObject bViewCone;
    private Vector3 bViewConePos;

    private void Start()
    {
        // ViewCone
        bViewConePos = gameObject.transform.GetChild(0).GetChild(1).transform.position;
        bViewCone = Instantiate(Resources.Load<GameObject>("ViewCone"), gameObject.transform.GetChild(0).GetChild(1), false);
        bViewCone.transform.position = bViewConePos;
        bViewCone.transform.forward = gameObject.transform.GetChild(0).GetChild(1).forward;
        // Misc
        bPlayer = FindObjectOfType(typeof(PlayerController)).GameObject();
        bBotCount = BotCalc();
        bRemainingBots = BotCalc();
        bGameControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //bObstacleLayer = LayerMask.NameToLayer("Obstacle");
        bObstacleLayer = LayerMask.GetMask("Obstacle");
        bAbilitiesList = new List<Component>();
        bAbilityAdd = false;
        // Ranged Attack
        bVisuals = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        bProjectileSpeed = 1000.0f;
        bShooting = gameObject.AddComponent<Shooting>();
        bShooting.SetHost(bVisuals);
        bShooting.bulletSpeed = bProjectileSpeed;
        bNextFire = bFireRate;
        // Patrol
        bDestPoint = 0;
        // Wander
        bTimer = bWanderTimer;
        // LockOn
        bViewRadius = bDefaultViewRadius;
        bInnerViewRadius = bDefaultInnerViewRadius;
        bDetectionTimer = 0;
        bRecentChaseTimerN = bRecentChaseTimer;
        // Suspicious
        bSusTimer = bSuspiciousTimer;
        bPlayerInView = false;
        //
    }

    private void Update()
    {
        bViewCone.GetComponent<Light>().range = bViewRadius;
        Vector3 movementDirection = GetComponent<NavMeshAgent>().velocity;
        if (movementDirection.magnitude > 0) {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, bRotationSpeed * Time.deltaTime);
        }
        GetComponent<NavMeshAgent>().updateRotation = false;
        Vector3 bVelocity = GetComponent<NavMeshAgent>().velocity;
        //Debug.Log("Velocity: " + bVelocity);
        if (bDetectionTimer == 0) return;
        DateTime now = DateTime.Now;
        if (gameObject.GetComponent<Perception>().sensedRecord.Length == 0) return;
        if (bPlayerInView ||
            GetComponent<Perception>().sensedRecord[0].timeLastSensed >=
            now.Subtract(new TimeSpan(0, 0, bSearchTime))) return;
        bGameControl.PlayerStatus = GameController.Status.SAFE;
        bDetectionTimer = 0;
        if (bRemainingBots ! >= bBotCount / 2) return;
        bViewRadius = bDefaultViewRadius;
        bInnerViewRadius = bDefaultInnerViewRadius;
    }
    private void LateUpdate()
    {
        bRemainingBots = BotCalc();
        if (bAbilityAdd) return;
        bAbilitiesList.AddRange(GetComponents(typeof(Ability)));
        bAbilityAdd = true;
    }

    private static int BotCalc()
    {
        BotInfo[] botScripts = FindObjectsOfType<BotInfo>();
        List<GameObject> bots = botScripts.Select(t => t.transform.root.gameObject).ToList();
        return bots.Count;
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, GetComponent<NavMeshAgent>().destination);
    }
}