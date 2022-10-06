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
                var range = Vector3.Distance(player.transform.position, agent.transform.position);
                var facing = (player.transform.position - agent.transform.position).normalized;
                var dotProd = Vector3.Dot(facing, agent.transform.forward);

                // LockOn
                if (range < playerRadius)
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


