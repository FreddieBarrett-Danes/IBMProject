using Unity.VisualScripting;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    //bools to check game status

    //public bool quizComplete = false;

    private ReadTSV rTSV;

    public GameObject mazeMinigame;
    public GameObject discMinigame;
    public GameObject sliderMinigame;
    public float mazeTimerStore;
    public float doorTimerStore;
    public GameObject chosenMinigame;

    private GameController gameController;

    private ScoreSystem ScoreSystemGameObject;

    [SerializeField]
    private GameObject[] doorGame;

    //public bool completedMinigame = false;
    public bool completedQuiz = false;
    //maze bools
    public bool completedMaze = false;
    public bool inMaze = false;
    public bool interactMaze = true;
    //door minigame bools
    public bool completedDoor = false;
    public bool inDoor = false;
    public bool interactDoor = true;

    // Start is called before the first frame update
    void Start()
    {
        rTSV = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
        //mazeMinigame = GameObject.FindGameObjectWithTag("Maze");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        doorGame = new GameObject[2];
        doorGame[0] = discMinigame;
        doorGame[1] = sliderMinigame;
    }
    public void Update()
    {
        /*if(inMaze)
        {
            gameController.inMinigame = true;
        }*/
        if (completedMaze)
        {

        }
    }
    public void StartMazeMinigame()
    {
        chosenMinigame = mazeMinigame;
        //Debug.Log("maze Set");
        if (!completedMaze && interactMaze)
        {
            if (!chosenMinigame.activeSelf)
            {
                if(chosenMinigame != null)
                chosenMinigame.SetActive(true);
                inMaze = true;
                gameController.inMinigame = true;
            }
        }
    }
    public void StartDoorMinigame()
    {
        gameController.inMinigame = true;
        doorGame[0] = discMinigame;
        doorGame[1] = sliderMinigame;
        int randNumber = Mathf.RoundToInt(Random.Range(0, 2));
        GameObject minigameHolder = doorGame[randNumber];
        chosenMinigame = minigameHolder;
        //Debug.Log("door Set");
        if (!completedDoor && interactDoor)
        {
            if(!chosenMinigame.activeSelf)
            {
                chosenMinigame.SetActive(true);
                inDoor = true;
                
            }
        }
        
    }
    public void StartQuiz(int numberofQuestions)
    {
        if (!completedQuiz)
        {
            gameController.inQuiz = true;
            gameController.inMinigame = true;
            rTSV.questionsInARow = numberofQuestions;
            rTSV.loopNumber = 0;
            rTSV.find = true;
            
        }
    }
}
