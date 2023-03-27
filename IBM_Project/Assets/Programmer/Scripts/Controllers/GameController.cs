using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //keeps track of if a bot has alerted, is safe or is hunting maybe keep[ track in player controller
    //keeps track if in lavel/mingame or quiz
    //private LevelTimer levelTimer;
    [Header("Cloud")]
    public bool Ship1;
    [Header("AI")]
    public bool Ship2;
    [Header("Data")]
    public bool Ship3;
    [Header("Quantum")]
    public bool Ship4;
    [Header("Security")]
    public bool Ship5;
    [Header("Level")]
    public bool Level5;
    public GameObject level;
    private GameObject player;
    [Header("Minigame")]
    public MinigameController mC;
    public bool inMinigame = false;
    public bool inQuiz = false;

    [Header("UI")]
    public GameObject[] levelUI;
    public GameObject[] mazeUI;
    public GameObject[] discUI;
    public GameObject[] tileUI;

    [Header("Timer")]
    public GameObject levelTimer;

    public float LevelTimeBank = 0.0f;

    [Header("Level Bools")]
    public bool completedLevel = false;
    public bool GameOver = false;
    public bool newScene = false;
    public bool playerHit = false;
    public bool Deactivate;

    [Header("Enemies")]
    public GameObject[] bots;
    [Header("Audio")]
    public AudioSource minigameWin;
    public AudioSource minigameLose;
    public bool failMinigame;
    public bool loseSoundPlayed;
    public bool winSoundPlayed;

    public enum Status
    {
        SAFE,
        ALERTED,
        HUNTED
    }

    public Status PlayerStatus;
    public TextMeshProUGUI playerstatusText;
    
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
        playerstatusText = GameObject.FindGameObjectWithTag("LevelUI").transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        playerstatusText.text = "SAFE";
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
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                if (Ship1)
                {
                    SceneManager.LoadScene(28);
                }
                else if (Ship2)
                {
                    SceneManager.LoadScene(26);
                }
                else if (Ship3)
                {
                    SceneManager.LoadScene(30);
                }
                else if (Ship4)
                {
                    SceneManager.LoadScene(32);
                }
                else if (Ship5)
                {
                    SceneManager.LoadScene(34);
                }
            }

            if (playerstatusText == null)
            {
                playerstatusText.text = "SAFE";
                playerstatusText = GameObject.FindGameObjectWithTag("LevelUI").transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            }
            if (inMinigame)
            {
                levelTimer.SetActive(false);
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
                playerstatusText.text = PlayerStatus switch
                {
                    Status.SAFE => "SAFE",
                    Status.HUNTED => "HUNTED",
                    Status.ALERTED => "ALERT",
                    _ => "SAFE"
                };
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
                levelTimer.SetActive(true);
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
            if (!failMinigame)
            {
                loseSoundPlayed = false;
            }

            if (failMinigame && !inMinigame)//minigame fail sound
            {
                if (!loseSoundPlayed)
                {

                    if (failMinigame)
                    {
                        minigameLose.Play();
                        loseSoundPlayed = true;
                    }
                }
            }
            else if (mC.completedMaze || mC.completedDoor)//minigamem win sound
            {
                if (!winSoundPlayed)
                {
                    minigameWin.Play();
                    winSoundPlayed = true;
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
            if (Ship1 && Level5)
            {
                SceneManager.LoadScene(29);
            }
            else if (Ship2 && Level5)
            {
                SceneManager.LoadScene(27);
            }
            else if (Ship3 && Level5)
            {
                SceneManager.LoadScene(31);
            }
            else if (Ship4 && Level5)
            {
                SceneManager.LoadScene(33);
            }
            else if (Ship5 && Level5)
            {
                SceneManager.LoadScene(35);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //LevelTimeBank += levelTimer.currentTime;
            }
        }
    }
}
