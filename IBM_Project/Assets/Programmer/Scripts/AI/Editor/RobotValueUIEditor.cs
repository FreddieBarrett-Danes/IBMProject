using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BotInfo))]
public class RobotValueUIEditor : Editor
{
    private static BotInfo bot;

    private void OnEnable()
    {
        bot = (BotInfo)target;
    }
    private void OnSceneGUI()
    {
        GameObject robot = bot.gameObject;
        Vector3 position = robot.transform.position;
        Handles.color = Color.red;
        Handles.DrawWireArc (position, Vector3.up, Vector3.forward, 360, bot.bDefaultViewRadius);
        Handles.color = Color.green;
        Handles.DrawWireArc (position, Vector3.up, Vector3.forward, 360, bot.bViewRadius);
        Handles.color = Color.white;
        Handles.DrawWireArc(position + bot.transform.forward * bot.bWanderDistance, Vector3.up, Vector3.forward, 360, bot.bWanderRadius);
        Handles.color = Color.blue;
        Vector3 viewAngleA = DirFromAngle (robot, -bot.bViewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle (robot, bot.bViewAngle / 2, false);
        Handles.DrawLine (position, position + viewAngleA * bot.bDefaultViewRadius);
        Handles.DrawLine (position, position + viewAngleB * bot.bDefaultViewRadius);
        Handles.color = Color.cyan;
        Vector3 viewAngleC = DirFromAngle (robot, -bot.bInnerViewAngle / 2, false);
        Vector3 viewAngleD = DirFromAngle (robot, bot.bInnerViewAngle / 2, false);
        Handles.DrawLine (position, position + viewAngleC * bot.bInnerViewRadius);
        Handles.DrawLine (position, position + viewAngleD * bot.bInnerViewRadius);
        Handles.color = Color.yellow;
        Handles.DrawWireArc(bot.bDebugLastKnownPos, Vector3.up, Vector3.forward, 360, bot.bSuspiciousRadius);
        Handles.color = Color.magenta;
        Handles.DrawWireArc (position, Vector3.up, Vector3.forward, 360, bot.bDefaultInnerViewRadius);
        if (bot.bComputer == null) return;
        Handles.color = Color.yellow;
        Handles.DrawWireArc (bot.bComputer.transform.position, Vector3.up, Vector3.forward, 360, bot.bComputerSusRadius);
    }

    private static Vector3 DirFromAngle(GameObject robot, float angleInDegrees, bool angleIsGlobal) 
    {
        if (!angleIsGlobal) 
        {
            angleInDegrees += robot.transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}