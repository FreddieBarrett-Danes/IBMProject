public class BasicRobotController : RobotController
{
    protected override void Update()
    {
        int movementReturn = AIMovement();

        switch (movementReturn)
        {
            case 0: // Wander
            {
                break;
            }
            case 1: // Lock On
            {
                break;
            }
            case 2: // Idle
            {
                break;
            }
            case 3: // Stopping
            {
                break;
            }
        }
    }
}

