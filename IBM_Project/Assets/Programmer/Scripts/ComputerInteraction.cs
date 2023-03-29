using UnityEngine;
using Unity.VisualScripting;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.Analytics;

public class ComputerInteraction : MonoBehaviour
{
    private GameObject player;


    public GameObject[] enemiesArray;
    public GameObject chosenMinigame;

    private GameController gameController;
    private MinigameController miniController;

    [SerializeField]
    private ReadTSV reader;

    private GameController gC;
    private GameObject cam;

    public bool mazeFailed = false;
    public bool mazeDONE = false;

    private bool isTouching;
    [SerializeField]
    private bool allDead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)).GameObject();
        enemiesArray = GameObject.FindGameObjectsWithTag("EnemyScript");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        miniController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        reader = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        FindEnemiesInScene();
        if(allDead)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if (isTouching && !gameController.inMinigame && Input.GetKey(KeyCode.E ))
        {
            if (!miniController.completedMaze)
            {
                miniController.StartMazeMinigame();
            }

        }

        if (gC.Level5 && allDead)
        {
            gC.inQuiz = true;
            reader.questionsInARow = 4;
            reader.find = true;
            cam.GetComponent<Camera>().farClipPlane = 0.5f;

        }

        if (miniController.completedMaze)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            isTouching = false;
            foreach (GameObject enemy in enemiesArray)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<BotInfo>().bIsDead = true;
                }
            }
            mazeDONE = true;
            //enemiesArray = null;
            miniController.completedMaze = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isTouching = false;
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
        /*enemieslist.Clear();
        BotInfo[] botScripts = FindObjectsOfType<BotInfo>();
        enemieslist = botScripts.Select(t => t).ToList();

        int counter = 0;
        for (int i = 0; i < enemieslist.Count; i++)
        {
            if (enemieslist[i].bIsDead)
            {
                counter++;
            }
        }
        if (counter == enemieslist.Count)
        {
            enemiesDead = true;
        }*/
    }
}
