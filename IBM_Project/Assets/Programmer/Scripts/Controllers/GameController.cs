using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //keeps track of if a bot has alerted, is safe or is hunting maybe keep[ track in player controller
    //keeps track if in lavel/mingame or quiz
    private LevelTimer levelTimer;
    public GameObject level;
    private ComputerInteraction cI;
    public bool inMinigame = false;
    public Canvas canvas;

    private GameObject chosenCanvas = null;

    public RectTransform[] canvasElements;

    

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
        canvasElements = canvas.gameObject.GetComponentsInChildren<RectTransform>();
        /*foreach (Transform child in Canvas.transform)
        {
            canvasElements.Add(child.gameObject);
        }*/
        //level = GameObject.FindGameObjectWithTag("Level");
        cI = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();
        PlayerStatus = Status.SAFE;
        //levelTimer = GameObject.FindGameObjectWithTag("Level Timer").GetComponent<LevelTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!completedLevel)
        {
            if(inMinigame)
            {
                level.SetActive(false);
                if (cI.chosenMinigame != null)
                {
                    for (int i = 0; i < canvasElements.Length; i++)
                    {
                        if (canvasElements[i].tag == cI.chosenMinigame.tag)
                        {
                            //chosenCanvas = canvasElements[i];
                        }
                    }
                    chosenCanvas.SetActive(true);
                    cI.chosenMinigame.SetActive(true);

                }
            }
            else
            {
                GameObject[] mazeWalls = GameObject.FindGameObjectsWithTag("mazeWall");
                for(int i = 0; i < mazeWalls.Length; i++)
                {
                    Destroy(mazeWalls[i]);
                }
                level.SetActive(true);
                if (cI.chosenMinigame != null)
                {
                    cI.chosenMinigame.SetActive(false);
                    chosenCanvas.SetActive(false);
                }
            }
        }
        else
        {
            //LevelTimeBank += levelTimer.currentTime;
        }
    }
}
