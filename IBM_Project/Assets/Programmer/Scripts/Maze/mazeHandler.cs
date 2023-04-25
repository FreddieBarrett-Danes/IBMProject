using UnityEngine;
using TMPro;


//Attach to mazeHandler gameobject (formerly mazeGeneration object)
//Dosen't activate until minigame is interacted by player
//Handles pre-game tutorial, space bar input etc

public class MazeHandler : MonoBehaviour
{

    //UI
    public GameObject pregameText;
    //public GameObject pressStartText;
    public GameObject timer;
    //public GameObject mazeLives;

    public GameObject mazePlayer;
    public GameObject goalLocation;

    public bool showIngameText;
    public bool showPregameTutorial;
    bool mazeReady;

    public WallGen wG; //Reference the gameobject with the walGen script

    public delegate void DelType1(bool mazeReady); //Delegate type
    public static event DelType1 OnMazeReady; //Event variable

    // Start is called before the first frame update
    void Start()
    {
        mazeReady = false;
        //OnMazeReady(false);
        pregameText.SetActive(true);
        //pressStartText.SetActive(true);
        timer.SetActive(true);
        GameObject.Find("tutorialBackground").GetComponent<MeshRenderer>().enabled = showPregameTutorial;
        mazePlayer.transform.position = new Vector3(64, 0, 62); //new Vector3(2, 0, 0);
        goalLocation.transform.position = wG.preGoalLocation;

        //GameObject.FindGameObjectWithTag("preGame").GetComponent<Renderer>().enabled = showPregameTutorial;
        //GameObject.FindGameObjectWithTag("preGame").GetComponent<Renderer>().transform.position = new Vector3(cameraPosition.x, cameraPosition.y-2, cameraPosition.z);
        //pressStartText.GetComponent<TextMeshProUGUI>().enabled = false;
        timer.GetComponent<TextMeshProUGUI>().enabled = false;
        //mazeLives.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //public GameObject mazeLives
        //countdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        //timerText.text = timer.ToString("000");

        if (Input.GetKeyDown("space")) //&& pressedPlay == false
        {
            //mazeReady = true;
            OnMazeReady(true);
            //wG.Timer.SetActive(true);
            //OnMazeReady(true); //Need to create new gameobject for walGen script
            GameObject.Find("tutorialBackground").GetComponent<MeshRenderer>().enabled = false;
            pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
            timer.GetComponent<TextMeshProUGUI>().enabled = true;
            //pressStartText.GetComponent<TextMeshProUGUI>().enabled = false;
            mazePlayer.transform.position = new Vector3(84, 0, 82); //new Vector3(2, 0, 0);
            goalLocation.transform.position = wG.preGoalLocation;
            //Debug.Log(mazePlayer.transform.position + "," + goalLocation.transform.position);
            //mazeLives.GetComponent<TextMeshProUGUI>().enabled = true;

            //Debug.Log("Press 'P' to complete maze instantly");

            //mazePlayer.transform.position = new Vector3(64, 0, 62); //new Vector3(2, 0, 0);
            //goalLocation.transform.position = preGoalLocation;


            //pressedPlay = true;
        }
    }
}
