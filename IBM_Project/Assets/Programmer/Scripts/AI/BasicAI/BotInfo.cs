using System;
using Unity.VisualScripting;
using UnityEngine;

public class BotInfo : MonoBehaviour
{
    // Misc
    public int threatLevel;
    public int botCount;
    public int remainingBots;
    
    // Throwing
    public Transform ShotStart;
    public float ProjectileSpeed;
    public float FireRate = 0.5f;
    public float NextFire;
    public Shooting shooting;
    private GameObject visuals;

    // Patrol
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
    public float detectionTimer;
    public bool engaging;

    // Suspicious
    public Vector3 lastKnownPos;
    public float suspiciousRadius;
    public float susTimer;
    public float stimer;
    public bool playerInView;

    private void Start()
    {
        visuals = gameObject.transform.GetChild(1).gameObject;
        ProjectileSpeed = 1000;
        shooting = gameObject.AddComponent<Shooting>();
        shooting.SetHost(visuals);
        shooting.bulletSpeed = ProjectileSpeed;
        ShotStart = gameObject.transform.GetChild(2);
        player = GameObject.FindGameObjectWithTag("Player");
        destPoint = 0;
        timer = wanderTimer;
        stimer = susTimer;
        detectionTimer = 0;
    }

    private void LateUpdate()
    {
        GameObject[] botsInGame = GameObject.FindGameObjectsWithTag ("Enemy");
        remainingBots = botsInGame.Length;
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
    }
}