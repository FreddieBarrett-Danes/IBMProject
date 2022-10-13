using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(BasicRobotController))]
public class RobotDetectionEditor : Editor
{
    private void OnSceneGUI()
    {
        RobotController robot = (BasicRobotController)target;
        Vector3 position = robot.transform.position;
        Handles.color = Color.red;
        Handles.DrawWireArc (position, Vector3.up, Vector3.forward, 360, robot.viewRadius);
        Handles.color = Color.white;
        Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, robot.wanderRadius);
        Handles.color = Color.blue;
        Vector3 viewAngleA = robot.DirFromAngle (-robot.viewAngle / 2, false);
        Vector3 viewAngleB = robot.DirFromAngle (robot.viewAngle / 2, false);
        Handles.DrawLine (position, position + viewAngleA * robot.viewRadius);
        Handles.DrawLine (position, position + viewAngleB * robot.viewRadius);
        Vector3 linePos = new Vector3(position.x, position.y + 0.1f, position.z);
        Handles.DrawLine(new Vector3(linePos.x, linePos.y, linePos.z + robot.GetComponent<NavMeshAgent>().speed), linePos);
    }
}