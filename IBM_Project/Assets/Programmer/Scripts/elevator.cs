using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class elevator : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject quizMaster;
    [SerializeField]
    private ReadTSV reader;

    private MinigameController mC;

    public Vector3 debug;

    private List<GameObject> enemies = new List<GameObject>();

    //OMG FOR THE LOVE OF GOD WE NEED TO CHANGE THIS LATER. UNLOAD THE SCENE OR SOMETHING. 
    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        quizMaster = GameObject.FindGameObjectWithTag("QuizMaster");
        reader = quizMaster.GetComponent<ReadTSV>();
        //mC = GameObject.FindGameObjectWithTag("GameController").GetComponent<Mini>
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        FindEnemiesInScene();

        if(other.gameObject.tag == player.tag && enemies.Count == 0)
        {
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
