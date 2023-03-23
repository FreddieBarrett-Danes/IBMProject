using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class elevator : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject quizMaster;
    [SerializeField]
    private ReadTSV reader;

    private GameController gC;

    public Vector3 debug;

    private List<GameObject> enemies = new List<GameObject>();

    //OMG FOR THE LOVE OF GOD WE NEED TO CHANGE THIS LATER. UNLOAD THE SCENE OR SOMETHING. 
    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)).GameObject();
        quizMaster = GameObject.FindGameObjectWithTag("QuizMaster");
        reader = quizMaster.GetComponent<ReadTSV>();
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        FindEnemiesInScene();
        GameObject.Find("LevelCanvas").SendMessage("QuizLoaded"); //For Score System

        if(other.gameObject.tag == player.tag && enemies.Count == 0)
        {
            gC.inQuiz = true;
            reader.questionsInARow = 4;
            reader.find = true;
            cam.GetComponent<Camera>().farClipPlane = 0.5f;
        }
    }

    private void FindEnemiesInScene()
    {
        enemies.Clear();

        //Old method that used the BotInfo script to locate enemies
        /*BotInfo[] botScripts = FindObjectsOfType<BotInfo>();
        enemies = botScripts.Select(t => t.transform.gameObject).ToList();*/

        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList<GameObject>();
    }
}
