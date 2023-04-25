using UnityEngine;
using TMPro;

public class GoalLocationScript : MonoBehaviour
{
    private GameController gC;
    private MinigameController mC;
    public ComputerInteraction computerInteraction;
    public MazePlayerScript mPlayer;

    public WallGen wG; //Re-reference the gameobject with the walGen script
    public Minigame_Timer mTimer;
    

    private ScoreSystem scoreSystemGameObject;

    private Vector3 originalCameraPos;

    private Camera camera;
    public bool setCameraPosition;

    public int lives = 6;
    public TextMeshProUGUI mazeLives;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("mazePlayer"))
        {
            //Debug.Log(mPlayer.transform.position + "," + transform.position);
            //Debug.Log("Maze Win");
            //Debug.Log("Refer to goalLocationScript for Maze output");
            mazeLives.GetComponent<TextMeshProUGUI>().enabled = false;
            scoreSystemGameObject.SendMessage("CompletedMinigame", new Vector2(1, mTimer.timer)); //1 = Maze
            //ScoreSystemGameObject.CompletedMinigame(new Vector2(1, mTimer.timer)); //1 = Maze
            //mC.mazeTimerStore = mTimer.timer;
            //ScoreSystemGameObject.Score += 10;

            //GameObject.Find("LevelCanvas").SendMessage("QuizLoaded");
            //ScoreSystemGameObject.SendMessage("CompletedMinigame", new Vector2(1, mTimer.timer));
            //Debug.Log("Complet")
            
            //CameraMaze call - Sets position of the camera
            if (setCameraPosition == false) { CameraMaze(false); };

            mPlayer.timesHit = 0;
            gC.inMinigame = false;
            wG.timer.SetActive(false);
            mC.completedMaze = true;
        }
    }

    void CameraMaze(bool inMaze)
    {
        if (setCameraPosition == true)
        {
            //Debug.Log("repositioned camera for maze, refer to goalLocation to disable");
            if (inMaze == true)
            {
                camera.GetComponent<Camera>().transform.position += new Vector3(82, 0, 80);
                //new Vector3(24, 0, 22)
            }
            else
            {
                camera.GetComponent<Camera>().transform.position = originalCameraPos;
            }
        }
    }


    // Start is called before the first frame update
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        scoreSystemGameObject = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();
        //mazeLives.text = "UISDHF";
        
        //mazeLives.GetComponent<TextMeshProUGUI>().active(true);

        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        mC = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        computerInteraction = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();
        originalCameraPos = camera.GetComponent<Camera>().transform.position;
        CameraMaze(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mazeLives.GetComponent<TextMeshProUGUI>().enabled = true;
        }

        //Debug.Log("TimesHit: " + mPlayer.timesHit + " Lives: " + Lives);
        mazeLives.text = (lives - mPlayer.timesHit).ToString("Lives: " + "0");


        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    mazeLives.GetComponent<TextMeshProUGUI>().enabled = true;
        //}

        //if (mPlayer != null && mPlayer.timesHit >= 6)
        if (mPlayer != null && mPlayer.timesHit >= lives)
        {
            mC.interactMaze = false;
            gC.inMinigame = false;
            if (gC.playerStatus == GameController.Status.HUNTED)
                return;
            else
                gC.playerStatus = GameController.Status.ALERTED;
            wG.timer.SetActive(false);
            gC.failMinigame = true;
            computerInteraction.mazeFailed = true;
            
            //mC.completedMaze = false;
            mPlayer.timesHit = 0;
        }

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    //Debug win of minigame
        //    gC.inMinigame = false;
        //    wG.Timer.SetActive(false);
        //    mC.completedMaze = true;
        //}

        
        
            CameraMaze(true);
        
    }
}
