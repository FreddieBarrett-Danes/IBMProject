using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(BasicRobotController))]
public class RobotDetectionEditor : Editor
{
    private void OnSceneGUI()
    {
        var robot = (BasicRobotController)target;
        var position = robot.transform.position;
        Handles.color = Color.red;
        Handles.DrawWireArc (position, Vector3.up, Vector3.forward, 360, robot.playerRadius);
        Handles.color = Color.white;
        Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, robot.wanderRadius);
        Handles.color = Color.blue;
        var linePos = new Vector3(position.x, position.y + 0.1f, position.z);
        Handles.DrawLine(new Vector3(linePos.x, linePos.y, linePos.z + robot.GetComponent<NavMeshAgent>().speed), linePos);
    }
}