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

    private bool enemiesDead = false;
    
    public List<BotInfo> enemies = new List<BotInfo>();

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

    private void Update()
    {
        FindEnemiesInScene();

    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("LevelCanvas").SendMessage("QuizLoaded"); //For Score System
        if(other.gameObject.tag == player.tag)
        {
            //Debug.Log("playerinside ele");
        }

        if (other.gameObject.tag == player.tag && (enemies.Count == 0 || enemiesDead))
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
        enemies.Clear();

        BotInfo[] botScripts = FindObjectsOfType<BotInfo>();
        enemies = botScripts.Select(t => t).ToList();

        int counter = 0;
        for(int i =0; i < enemies.Count; i++)
        {
            if (enemies[i].bIsDead)
            {
                counter++;
            }
        }
        if(counter == enemies.Count)
        {
            enemiesDead = true;
        }

    }
}
