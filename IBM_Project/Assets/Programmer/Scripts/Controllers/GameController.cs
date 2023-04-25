using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //keeps track of if a bot has alerted, is safe or is hunting maybe keep[ track in player controller
    //keeps track if in lavel/mingame or quiz
    //private LevelTimer levelTimer;
    [Header("Cloud")]
    public bool ship1;
    [Header("AI")]
    public bool ship2;
    [Header("Data")]
    public bool ship3;
    [Header("Quantum")]
    public bool ship4;
    [Header("Security")]
    public bool ship5;
    [Header("Level")]
    public bool level5;
    public GameObject level;
    private GameObject player;
    [Header("Minigame")] 
    public bool noComputerInScene;
    public MinigameController mC;
    public bool inMinigame = false;
    public bool inQuiz = false;
    public float computerDoorTimer = 0.0f;
    private float computerTimerOrigin;

    [Header("UI")]
    public GameObject[] levelUI;
    public GameObject[] mazeUI;
    public GameObject[] discUI;
    public GameObject[] tileUI;

    [Header("Timer")]
    public GameObject levelTimer;

    public float levelTimeBank = 0.0f;

    [Header("Level Bools")]
    public bool completedLevel = false;
    public bool gameOver = false;
    public bool newScene = false;
    public bool playerHit = false;
    public bool deactivate;

    [Header("Enemies")]
    public GameObject[] bots;
    [Header("Audio")]
    public AudioSource minigameWin;
    public AudioSource minigameLose;
    public bool failMinigame;
    public bool loseSoundPlayed;
    public bool winSoundPlayed;

    [SerializeField]
    private GameObject menu;
    public enum Status
    {
        SAFE,
        ALERTED,
        HUNTED
    }

    public PlayerController playerControl;
    public Status playerStatus;
    public TextMeshProUGUI playerStatusText;
    public Image shootAbilityIconImage;
    public Image moveAbilityIconImage;
    public Image canHackIconImage;
    public Image computerAvailableImage;
    private Sprite greyShooting;
    private Sprite greyMove;
    private Sprite greyHack;
    private Sprite greyComputer;
    private Sprite greenShooting;
    private Sprite greenMove;
    private Sprite greenHack;
    private Sprite greenComputer;

    public GameObject[] deadDroids;

    public ScoreSystem scoreSystem;

    public ComputerInteraction computerObj;
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
        menu = GameObject.Find("Menu");
        scoreSystem = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreSystem>();
        levelUI = GameObject.FindGameObjectsWithTag("LevelUI");
        levelTimer = GameObject.FindGameObjectWithTag("LevelTimer");
        level = GameObject.FindGameObjectWithTag("LevelObject");
        mC = gameObject.GetComponent<MinigameController>();
        player = FindObjectOfType<PlayerController>().GameObject();
        playerStatus = Status.SAFE;
        bots = GameObject.FindGameObjectsWithTag("Sprite");
        computerTimerOrigin = computerDoorTimer;
        //levelTimer = GameObject.FindGameObjectWithTag("Level Timer").GetComponent<LevelTimer>();

        playerControl = player.GetComponent<PlayerController>();
        
        greyShooting = Resources.Load<Sprite>("GunIconGrey");
        greyMove = Resources.Load<Sprite>("Speed_Icon_Grey");
        greyHack = Resources.Load<Sprite>("RobotIconGrey");
        greyComputer = Resources.Load<Sprite>("ComputerIconGrey");
        greenShooting = Resources.Load<Sprite>("GunIcon");
        greenMove = Resources.Load<Sprite>("Speed_Icon");
        greenHack = Resources.Load<Sprite>("RobotIcon");
        greenComputer = Resources.Load<Sprite>("ComputerIcon");
        
        playerStatusText = FindObjectOfType<FinderScript>().transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        playerStatusText.text = "SAFE";
        shootAbilityIconImage = FindObjectOfType<FinderScript>().transform.GetChild(3).GetComponent<Image>();
        shootAbilityIconImage.sprite = greyShooting;
        moveAbilityIconImage = FindObjectOfType<FinderScript>().transform.GetChild(4).GetComponent<Image>();
        moveAbilityIconImage.sprite = greyMove;
        canHackIconImage = FindObjectOfType<FinderScript>().transform.GetChild(5).GetComponent<Image>();
        canHackIconImage.sprite = greyHack;
        computerAvailableImage = FindObjectOfType<FinderScript>().transform.GetChild(6).GetComponent<Image>();
        computerAvailableImage.sprite = greyComputer;

        computerObj = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();
        scoreSystem.restarted = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!completedLevel)
        {
            menu.GetComponent<MenuController>().quiz = inQuiz;
            deadDroids = GameObject.FindGameObjectsWithTag("DeadEnemy");
            if (playerControl.canShoot)
                shootAbilityIconImage.sprite = greenShooting;
            else
                shootAbilityIconImage.sprite = greyShooting;
            if (playerControl.canSpeed)
                moveAbilityIconImage.sprite = greenMove;
            else
                moveAbilityIconImage.sprite = greyMove;
            if (playerControl.isBehindEnemy)
                canHackIconImage.sprite = greenHack;
            else
                canHackIconImage.sprite = greyHack;
            
            switch(noComputerInScene)
            {
                case false:
                {
                    switch (computerObj.mazeFailed || computerObj.mazeDONE)
                    {
                        case true:
                        {
                            computerAvailableImage.sprite = greyComputer;
                            break;
                        }
                        case false:
                        {
                            computerAvailableImage.sprite = greenComputer;
                            break;
                        }
                    }
                    break;
                }
                case true:
                {
                    computerAvailableImage.sprite = greyComputer;
                    break;
                }
            }

            if (deadDroids.Length > 0)
            {
                foreach (GameObject droid in deadDroids)
                {
                    if (droid)
                    {
                        if (!inMinigame)
                        {
                            droid.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        else
                        {
                            droid.transform.GetChild(0).gameObject.SetActive(false);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (inQuiz != true && inMinigame != true)
                {
                    //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                    scoreSystem.restarted = true;
                    scoreSystem.score = scoreSystem.scorePool;
                    levelTimer.GetComponent<LevelTimer>().currentTime = levelTimer.GetComponent<LevelTimer>().startTime;
                    Debug.Log("Score " + scoreSystem.scorePool);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            if(playerHit)
            {
                //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (gameOver)
            {
                //transistion to losing scene
                Destroy(levelTimer);
                //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                if (ship1)
                {
                    SceneManager.LoadScene(28);
                }
                else if (ship2)
                {
                    SceneManager.LoadScene(26);
                }
                else if (ship3)
                {
                    SceneManager.LoadScene(30);
                }
                else if (ship4)
                {
                    SceneManager.LoadScene(32);
                }
                else if (ship5)
                {
                    SceneManager.LoadScene(34);
                }
            }

            if (playerStatusText == null)
            {
                playerStatusText.text = "SAFE";
                playerStatusText = GameObject.FindGameObjectWithTag("LevelUI").transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            }
            if (inMinigame)
            {
                //Debug.Log("in minigame");
                levelTimer.SetActive(false);
                level.SetActive(false);
                for (int i = 0; i < levelUI.Length; i++)
                {
                    levelUI[i].SetActive(false);
                }
            }
            else
            {
                //Debug.Log("no minigame");
                //GameObject[] mazeWalls = GameObject.FindGameObjectsWithTag("mazeWall");
                //if (mazeWalls != null)
                //{
                //    for (int i = 0; i < mazeWalls.Length; i++)
                //    {
                //        Destroy(mazeWalls[i]);
                //    }
                //}
                playerStatusText.text = playerStatus switch
                {
                    Status.SAFE => "SAFE",
                    Status.HUNTED => "HUNTED",
                    Status.ALERTED => "ALERT",
                    _ => "SAFE"
                };
                /*GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
                if (tiles != null)
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        tiles[i].SetActive(false);
                    }
                }*/

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
            else
            {
                computerDoorTimer -= Time.deltaTime;
                if(computerDoorTimer < 0)
                {
                    failMinigame = false;
                    computerDoorTimer = computerTimerOrigin;
                }
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

            switch (deactivate)
            {
                case true:
                {
                    if (bots != null)
                    {
                        foreach (GameObject bot in bots)
                        {
                            if(bot)
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
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            if (ship1 && level5)
            {
                SceneManager.LoadScene(29);
            }
            else if (ship2 && level5)
            {
                SceneManager.LoadScene(27);
            }
            else if (ship3 && level5)
            {
                SceneManager.LoadScene(31);
            }
            else if (ship4 && level5)
            {
                SceneManager.LoadScene(33);
            }
            else if (ship5 && level5)
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
