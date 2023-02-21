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

    public GameObject chosenMinigame;

    private GameController gameController;

    private GameObject[] doorGame;

    //public bool completedMinigame = false;
    public bool completedQuiz = false;
    //maze bools
    public bool completedMaze = false;
    public bool interactMaze = true;
    //door minigame bools
    public bool completedDoor = false;
    public bool interactDoor = true;
    // Start is called before the first frame update
    void Start()
    {
        rTSV = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
        //mazeMinigame = GameObject.FindGameObjectWithTag("Maze");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        doorGame[0] = discMinigame;
        doorGame[1] = sliderMinigame;
    }
    public void StartMazeMinigame()
    {
        chosenMinigame = mazeMinigame;
        if (!completedMaze && interactMaze)
        {
            if (!chosenMinigame.activeSelf)
            {
                chosenMinigame.SetActive(true);
                gameController.inMinigame = true;
            }
        }
    }
    public void StartDoorMinigame()
    {
        /*int randNumber = Random.Range(0, 1);
        GameObject minigameHolder = doorGame[randNumber];*/
        chosenMinigame = discMinigame; //change this to random
        if(!completedDoor && interactDoor)
        {
            if(!chosenMinigame.activeSelf)
            {
                chosenMinigame.SetActive(true);
                gameController.inMinigame = true;
            }
        }
        
    }
    public void StartQuiz(int numberofQuestions)
    {
        if (!completedQuiz)
        {
            rTSV.questionsInARow = numberofQuestions;
            rTSV.loopNumber = 0;
            rTSV.find = true;
            gameController.inMinigame = true;
        }
    }
}
