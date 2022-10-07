using UnityEngine;

public class BasicRobotController : RobotController
{

    protected override void Update()
    {
        switch (stop)
        {
            // Movement
            case false:
            {
                timer += Time.deltaTime;
                var playerPos = player.transform.position;
                var agentPos = agent.transform.position;
                var range = Vector3.Distance(playerPos, agentPos);
                var dirToTarget = (playerPos - agentPos).normalized;
                
                // LockOn
                if (range < viewRadius && Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2)
                {
                    agent.SetDestination(player.transform.position);
                    timer = wanderTimer;
                }

                // Wander
                else if (timer >= wanderTimer)
                {
                    var newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0;
                }

                // Idle
                else if (agent.remainingDistance == 0)
                {
                
                }

                break;
            }
            
            // Stopping
            case true:
                StopMove();
                break;
        }
    }
}


