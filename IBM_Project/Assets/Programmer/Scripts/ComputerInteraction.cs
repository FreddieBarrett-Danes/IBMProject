using UnityEngine;
using Unity.VisualScripting;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine.Analytics;

public class ComputerInteraction : MonoBehaviour
{
    private GameObject player;


    public GameObject[] enemies;
    public GameObject chosenMinigame;

    private GameController gameController;
    private MinigameController miniController;

    [SerializeField]
    private ReadTSV reader;

    private GameController gC;
    private GameObject cam;

    public bool mazeFailed = false;

    private bool isTouching;
    private bool enemiesDead = false;

    public List<BotInfo> enemieslist = new List<BotInfo>();
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)).GameObject();
        enemies = GameObject.FindGameObjectsWithTag("EnemyScript");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        miniController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        reader = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        FindEnemiesInScene();
        if ((enemieslist.Count == 0 || enemiesDead) && gameController.Level5)
        {
            gC.inQuiz = true;
            reader.questionsInARow = 4;
            reader.find = true;
            cam.GetComponent<Camera>().farClipPlane = 0.5f;
        }
        if (isTouching && !gameController.inMinigame && Input.GetKey(KeyCode.E ))
        {
            if (!miniController.completedMaze)
            {
                miniController.StartMazeMinigame();
            }

        }

        if (miniController.completedMaze)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<BotInfo>().bIsDead = true;
                }
            }
            enemies = null;
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
        enemieslist.Clear();

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
        }

    }
}
