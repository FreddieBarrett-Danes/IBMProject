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

    private ComputerInteraction PC;

    public Vector3 debug;

    public GameObject[] enemies;
    public List<GameObject> enemiesShutdown = new List<GameObject>();

    //OMG FOR THE LOVE OF GOD WE NEED TO CHANGE THIS LATER. UNLOAD THE SCENE OR SOMETHING. 
    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)).GameObject();
        quizMaster = GameObject.FindGameObjectWithTag("QuizMaster");
        reader = quizMaster.GetComponent<ReadTSV>();
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        PC= GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();
        enemies = GameObject.FindGameObjectsWithTag("EnemyScript");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        FindEnemiesInScene();
        if (PC.enemies == null)
        {
            Debug.Log("can leave");

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("LevelCanvas").SendMessage("QuizLoaded"); //For Score System
        if(other.gameObject.tag == player.tag)
        {
            //Debug.Log("playerinside ele");
        }


        if (other.gameObject.tag == player.tag && PC.enemies == null)
        {
            Debug.Log("playerinside ele");
            gC.inQuiz = true;
            reader.questionsInARow = 4;
            reader.find = true;
            cam.GetComponent<Camera>().farClipPlane = 0.5f;
        }
/*        else
        {
            Debug.Log("lol fuck u freddie");
        }*/
    }

    private void FindEnemiesInScene()
    {
        //enemies.Clear();

        //Old method that used the BotInfo script to locate enemies
        /*BotInfo[] botScripts = FindObjectsOfType<BotInfo>();
        enemies = botScripts.Select(t => t.transform.gameObject).ToList();*/

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = PC.enemies[i];
        }
/*        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<BotInfo>().bIsDead)
            {
                enemiesShutdown.Add(enemies[i]);
            }
        }*/


    }
}
