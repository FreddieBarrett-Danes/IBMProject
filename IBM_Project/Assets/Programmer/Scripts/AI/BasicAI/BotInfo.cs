using UnityEngine;

public class BotInfo
{
    public static int remainingBots = 4;
    public static int threatLevel;
    
    public static Transform[] patrol;
    public static int destPoint = 0;
    public static bool start;
    // Wander
    public static float wanderRadius;
    public static float wanderTimer;
    public static float timer = wanderTimer;
    
    // LockOn
    public static readonly GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static float viewRadius;
    [Range(0, 360)] 
    public static float viewAngle;
    public static bool engaging;

    // Suspicious
    public static Vector3 lastKnownPos;
    public static float suspiciousRadius;
    public static float susTimer;
    public static float stimer = susTimer;
    public static bool playerInView;
}