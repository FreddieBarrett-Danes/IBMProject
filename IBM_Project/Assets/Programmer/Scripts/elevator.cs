using System;
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
    [SerializeField]
    public GameObject[] enemiesArray;

    private GameObject PC;

    public Vector3 debug;

    [SerializeField]
    private bool allDead;

    //OMG FOR THE LOVE OF GOD WE NEED TO CHANGE THIS LATER. UNLOAD THE SCENE OR SOMETHING. 
    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        enemiesArray = GameObject.FindGameObjectsWithTag("EnemyScript");
        player = FindObjectOfType(typeof(PlayerController)).GameObject();
        quizMaster = GameObject.FindGameObjectWithTag("QuizMaster");
        reader = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        PC = GameObject.FindGameObjectWithTag("Computer");
    }

    private void Update()
    {
        FindEnemiesInScene();

        //GameObject.Find("LevelCanvas").SendMessage("QuizLoaded"); //For Score System

    }
    private void OnTriggerEnter(Collider other)
    {
        //GameObject.Find("LevelCanvas").SendMessage("QuizLoaded"); //For Score System

        if (other.gameObject.tag == player.tag && allDead)
        {
            reader.cloudAskedList.Clear();
            reader.aiAskedList.Clear();
            reader.dataAskedList.Clear();
            reader.quantumAskedList.Clear();
            reader.securityAskedList.Clear();
            
            gC.inQuiz = true;
            reader.questionsInARow = 4;
            reader.find = true;
            cam.GetComponent<Camera>().farClipPlane = 0.5f;
        }
    }

    private void FindEnemiesInScene()
    {
        allDead = true;

        for (int i = 0; i < enemiesArray.Length; i++)
        {
            if (!enemiesArray[i].GetComponent<BotInfo>().bIsDead) // if any element is false
            {
                allDead = false; // set allTrue to false and exit the loop
                break;
            }
        }
        //allMissing = true;
        //for(int i = 0; i < enemies.Count; i++)
        /*enemies.Clear();
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
        Debug.Log("Counter" + counter);
        Debug.Log("Count" + enemies.Count);
        if(counter == enemies.Count)
        {
            enemiesDead = true;
        }*/


    }
}
