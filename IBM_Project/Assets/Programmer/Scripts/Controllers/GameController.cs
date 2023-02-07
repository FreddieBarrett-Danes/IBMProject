using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //keeps track of if a bot has alerted, is safe or is hunting maybe keep[ track in player controller
    //keeps track if in lavel/mingame or quiz
    //private LevelTimer levelTimer;
    public GameObject level;
    private ComputerInteraction cI;
    public bool inMinigame = false;

    public GameObject[] levelUI;
    public GameObject[] mazeUI;

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
        DontDestroyOnLoad(this);

        //dont use get component here
        
        foreach(GameObject maze in mazeUI)
        {
            maze.SetActive(false);
        }

        cI = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();
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

            if (inMinigame)
            {
                level.SetActive(false);
                for(int i = 0; i < levelUI.Length; i++)
                {
                    levelUI[i].SetActive(false);
                }
                /*foreach (GameObject maze in mazeUI)
                {
                    maze.SetActive(true);
                }*/

            }
            else
            {
                GameObject[] mazeWalls = GameObject.FindGameObjectsWithTag("mazeWall");
                for(int i = 0; i < mazeWalls.Length; i++)
                {
                    Destroy(mazeWalls[i]);
                }
                level.SetActive(true);
                for (int i = 0; i < levelUI.Length; i++)
                {
                    levelUI[i].SetActive(true);
                }
                if (cI.chosenMinigame != null)
                {
                    cI.chosenMinigame.SetActive(false);
                }
                /*foreach (GameObject maze in mazeUI)
                {
                    maze.SetActive(false);
                }*/
            }
        }
        else
        {
            //LevelTimeBank += levelTimer.currentTime;
        }
    }
}
