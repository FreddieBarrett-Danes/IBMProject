using UnityEngine;
using Unity.VisualScripting;

public class ComputerInteraction : MonoBehaviour
{
    private GameObject player;


    public GameObject[] enemies;
    public GameObject chosenMinigame;

    private GameController gameController;
    private MinigameController miniController;

    public bool mazeFailed = false;

    private bool isTouching;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)).GameObject();
        enemies = GameObject.FindGameObjectsWithTag("EnemyScript");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        miniController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
    }

    void Update()
    {
        
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
                enemy.GetComponent<BotInfo>().bIsDead = true;
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
}
