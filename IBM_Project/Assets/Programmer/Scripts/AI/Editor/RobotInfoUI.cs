using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BotInfo))]
[CanEditMultipleObjects]
public class RobotInfoUI : Editor
{
    private static BotInfo bot;

    private void OnEnable()
    {
        bot = (BotInfo)target;
    }

    public enum DisplayCategory
    {
        Basic, Sentinel
    }
    
    public DisplayCategory categoryToDisplay;
    
    public override void OnInspectorGUI()
    {
        bot.pointLoop = EditorGUILayout.Toggle("Loop Waypoints?", bot.pointLoop);
        EditorGUI.BeginChangeCheck();
        categoryToDisplay = (DisplayCategory) EditorGUILayout.EnumPopup("EnemyType", categoryToDisplay);
        if (EditorGUI.EndChangeCheck())
        {
            EditorGUILayout.Space();
            switch (categoryToDisplay)
            {
                case DisplayCategory.Basic:
                    DisplayBasicInfo();
                    break;
                case DisplayCategory.Sentinel:
                    DisplaySentinelInfo();
                    break;
                default:
                    Debug.Log("No Section to Display!");
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    private static void DisplayBasicInfo()
    {
        bot.threatLevel = 3;
        bot.start = false;
        bot.wanderRadius = 5f;
        bot.wanderTimer = 10f;
        bot.viewRadius = 2f;
        bot.viewAngle = 90f;
        bot.engaging = false;
        bot.suspiciousRadius = 3f;
        bot.susTimer = 5f;
        bot.playerInView = false;
    }
    
    private static void DisplaySentinelInfo()
    {
        bot.threatLevel = 4;
        bot.start = false;
        bot.wanderRadius = 8f;
        bot.wanderTimer = 8f;
        bot.viewRadius = 3f;
        bot.viewAngle = 120f;
        bot.engaging = false;
        bot.suspiciousRadius = 5f;
        bot.susTimer = 3f;
        bot.playerInView = false;
    }
}
