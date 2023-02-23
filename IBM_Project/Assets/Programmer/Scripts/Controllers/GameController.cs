using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //keeps track of if a bot has alerted, is safe or is hunting maybe keep[ track in player controller
    //keeps track if in lavel/mingame or quiz
    //private LevelTimer levelTimer;
    public GameObject level;
    private GameObject player;
    public MinigameController mC;
    public bool inMinigame = false;

    public GameObject[] levelUI;
    public GameObject[] mazeUI;
    public GameObject[] discUI;
    public GameObject[] tileUI;

    public float LevelTimeBank = 0.0f;
    public bool completedLevel = false;

    public enum Status
    {
        SAFE,
        ALERTED,
        HUNTED
    }

    public Status PlayerStatus;
    
    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);

        //dont use get component here
        
        foreach(GameObject maze in mazeUI)
        {
            maze.SetActive(false);
        }

        foreach (GameObject disc in discUI)
        {
            disc.SetActive(false);
        }
        foreach (GameObject tile in tileUI)
        {
            tile.SetActive(false);
        }

        mC = gameObject.GetComponent<MinigameController>();
        player = FindObjectOfType<PlayerController>().GameObject();
        PlayerStatus = Status.SAFE;
        //levelTimer = GameObject.FindGameObjectWithTag("Level Timer").GetComponent<LevelTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!completedLevel)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if(player == null)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (inMinigame)
            {
                level.SetActive(false);
                for (int i = 0; i < levelUI.Length; i++)
                {
                    levelUI[i].SetActive(false);
                }
            }
            else
            {
                GameObject[] mazeWalls = GameObject.FindGameObjectsWithTag("mazeWall");
                if (mazeWalls != null)
                {
                    for (int i = 0; i < mazeWalls.Length; i++)
                    {
                        Destroy(mazeWalls[i]);
                    }
                }
                mC.inMaze = false;
                mC.inDoor = false;
                level.SetActive(true);
                for (int i = 0; i < levelUI.Length; i++)
                {
                    levelUI[i].SetActive(true);
                }
                if (mC.chosenMinigame != null)
                {
                    mC.chosenMinigame.SetActive(false);
                }
            }
        }
        else
        {
            //LevelTimeBank += levelTimer.currentTime;
        }
    }
}
