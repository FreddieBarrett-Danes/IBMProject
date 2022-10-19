using UnityEngine;

namespace BT
{
    public class BTInfo
    {
        // Wander
        public const float wanderRadius = 5f;
        public const float wanderTimer = 10f;
        public static float timer = wanderTimer;
        
        // LockOn
        public static readonly GameObject player = GameObject.Find("Player");
        public const float viewRadius = 2f;
        [Range(0, 360)] 
        public const float viewAngle = 90f;
        public static bool engaging;

        //Combat
        public int threatLevel = 1;
    }
}