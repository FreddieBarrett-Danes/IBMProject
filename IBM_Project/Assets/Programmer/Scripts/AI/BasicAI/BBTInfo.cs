using UnityEngine;

namespace BT
{
    public class BBTInfo : BotInfo
    {
        public BBTInfo()
        {
            threatLevel = 3;
            start = false;
            // Wander
            wanderRadius = 5f;
            wanderTimer = 10f;

            // Vision
            viewRadius = 2f;
            viewAngle = 90f;

            // Suspicious
            suspiciousRadius = 3f;
            susTimer = 5f;
        }
    }
}