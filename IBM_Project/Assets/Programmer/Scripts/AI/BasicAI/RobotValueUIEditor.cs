using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using BT;

[CustomEditor(typeof(BasicBT))]
public class RobotValueUIEditor : Editor
{
    private void OnSceneGUI()
    {
        GameObject robot = GameObject.FindGameObjectWithTag("Enemy");
        Vector3 position = robot.transform.position;
        Handles.color = Color.red;
        Handles.DrawWireArc (position, Vector3.up, Vector3.forward, 360, BTInfo.viewRadius);
        Handles.color = Color.white;
        Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, BTInfo.wanderRadius);
        Handles.color = Color.blue;
        Vector3 viewAngleA = DirFromAngle (robot, -BTInfo.viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle (robot, BTInfo.viewAngle / 2, false);
        Handles.DrawLine (position, position + viewAngleA * BTInfo.viewRadius);
        Handles.DrawLine (position, position + viewAngleB * BTInfo.viewRadius);
        Vector3 linePos = new Vector3(position.x, position.y + 0.1f, position.z);
        Handles.DrawLine(new Vector3(linePos.x, linePos.y, linePos.z + robot.GetComponent<NavMeshAgent>().speed), linePos);
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