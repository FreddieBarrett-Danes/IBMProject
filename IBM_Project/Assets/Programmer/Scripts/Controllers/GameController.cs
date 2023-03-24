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
    public bool inQuiz = false;
    public GameObject[] levelUI;
    public GameObject[] mazeUI;
    public GameObject[] discUI;
    public GameObject[] tileUI;

    public GameObject levelTimer;

    public float LevelTimeBank = 0.0f;
    public bool completedLevel = false;
    public bool GameOver = false;
    public bool newScene = false;

    public bool playerHit = false;

    public bool Deactivate;
    public GameObject[] bots;

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
        levelUI = GameObject.FindGameObjectsWithTag("LevelUI");
        levelTimer = GameObject.FindGameObjectWithTag("LevelTimer");
        level = GameObject.FindGameObjectWithTag("LevelObject");
        mC = gameObject.GetComponent<MinigameController>();
        player = FindObjectOfType<PlayerController>().GameObject();
        PlayerStatus = Status.SAFE;
        bots = GameObject.FindGameObjectsWithTag("Sprite");
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
            if(playerHit)
            {
                //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
            }
            if (GameOver)
            {
                //transistion to losing scene
                Destroy(levelTimer);
                //this is the correct way of doing this lol
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneManager.GetActiveScene().buildIndex);
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
                //GameObject[] mazeWalls = GameObject.FindGameObjectsWithTag("mazeWall");
                //if (mazeWalls != null)
                //{
                //    for (int i = 0; i < mazeWalls.Length; i++)
                //    {
                //        Destroy(mazeWalls[i]);
                //    }
                //}

                GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
                if (tiles != null)
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        Destroy(tiles[i]);
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
            switch (Deactivate)
            {
                case true:
                {
                    if (bots != null)
                    {
                        foreach (GameObject bot in bots)
                        {
                            bot.transform.gameObject.SetActive(false);
                        }

                        player.transform.parent.GetChild(1).gameObject.SetActive(false);
                        player.transform.parent.GetChild(2).gameObject.SetActive(false);
                        player.transform.parent.GetChild(3).gameObject.SetActive(false);
                    }
                    break;
                }
                case false:
                {
                    if(bots != null)
                    {
                        foreach (GameObject bot in bots)
                        {
                            if (bot)
                                bot.transform.gameObject.SetActive(true);
                        }

                        if (!player) return;
                        if (player.GetComponent<PlayerController>().threatLevel == 0)
                        {
                            player.transform.parent.GetChild(1).gameObject.SetActive(true);
                            player.transform.parent.GetChild(2).gameObject.SetActive(false);
                            player.transform.parent.GetChild(3).gameObject.SetActive(false);
                        }
                        else if (player.GetComponent<PlayerController>().threatLevel == 2)
                        {
                            player.transform.parent.GetChild(1).gameObject.SetActive(false);
                            player.transform.parent.GetChild(2).gameObject.SetActive(true);
                            player.transform.parent.GetChild(3).gameObject.SetActive(false);
                        }
                        else if (player.GetComponent<PlayerController>().threatLevel == 3)
                        {
                            player.transform.parent.GetChild(1).gameObject.SetActive(false);
                            player.transform.parent.GetChild(2).gameObject.SetActive(false);
                            player.transform.parent.GetChild(3).gameObject.SetActive(true);
                        }
                        else
                        {
                            player.transform.parent.GetChild(1).gameObject.SetActive(true);
                            player.transform.parent.GetChild(2).gameObject.SetActive(false);
                            player.transform.parent.GetChild(3).gameObject.SetActive(false);
                        }
                    }
                
                    break;
                }
            }
        }
        else if (completedLevel)
        {
            Destroy(levelTimer);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            //LevelTimeBank += levelTimer.currentTime;
        }
    }
}
