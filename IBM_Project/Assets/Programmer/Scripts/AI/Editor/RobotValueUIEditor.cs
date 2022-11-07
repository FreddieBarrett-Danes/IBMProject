using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(BotInfo))]
[CanEditMultipleObjects]
public class RobotValueUIEditor : Editor
{
    private static BotInfo bot;

    private void OnEnable()
    {
        bot = (BotInfo)target;
    }
    private void OnSceneGUI()
    {
        GameObject[] robots = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject t in robots)
        {
            Vector3 position = t.transform.position;
            Handles.color = Color.red;
            Handles.DrawWireArc (position, Vector3.up, Vector3.forward, 360, bot.viewRadius);
            Handles.color = Color.white;
            Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, bot.wanderRadius);
            Handles.color = Color.blue;
            Vector3 viewAngleA = DirFromAngle (t, -bot.viewAngle / 2, false);
            Vector3 viewAngleB = DirFromAngle (t, bot.viewAngle / 2, false);
            Handles.DrawLine (position, position + viewAngleA * bot.viewRadius);
            Handles.DrawLine (position, position + viewAngleB * bot.viewRadius);
            Vector3 linePos = new Vector3(position.x, position.y + 0.1f, position.z);
            Handles.DrawLine(new Vector3(linePos.x, linePos.y, linePos.z + t.GetComponent<NavMeshAgent>().speed), linePos);
        }
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