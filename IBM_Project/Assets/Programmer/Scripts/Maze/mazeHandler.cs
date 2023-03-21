using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//Attach to mazeHandler gameobject (formerly mazeGeneration object)
//Dosen't activate until minigame is interacted by player
//Handles pre-game tutorial, space bar input etc

public class mazeHandler : MonoBehaviour
{

    //UI
    public GameObject pregameText;
    public GameObject pressStartText;
    public GameObject Timer;

    public GameObject mazePlayer;
    public GameObject goalLocation;

    public bool showIngameText;
    public bool showPregameTutorial;
    bool mazeReady;

    public walGen wG; //Reference the gameobject with the walGen script

    public delegate void DelType1(bool mazeReady); //Delegate type
    public static event DelType1 OnMazeReady; //Event variable

    // Start is called before the first frame update
    void Start()
    {
        mazeReady = false;
        //OnMazeReady(false);
        pregameText.SetActive(true);
        pressStartText.SetActive(true);
        Timer.SetActive(true);
        GameObject.Find("tutorialBackground").GetComponent<MeshRenderer>().enabled = showPregameTutorial;
        mazePlayer.transform.position = new Vector3(64, 0, 62); //new Vector3(2, 0, 0);
        goalLocation.transform.position = wG.preGoalLocation;

        //GameObject.FindGameObjectWithTag("preGame").GetComponent<Renderer>().enabled = showPregameTutorial;
        //GameObject.FindGameObjectWithTag("preGame").GetComponent<Renderer>().transform.position = new Vector3(cameraPosition.x, cameraPosition.y-2, cameraPosition.z);
        pressStartText.GetComponent<TextMeshProUGUI>().enabled = false;
        Timer.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) //&& pressedPlay == false
        {
            //mazeReady = true;
            OnMazeReady(true);
            //wG.Timer.SetActive(true);
            //OnMazeReady(true); //Need to create new gameobject for walGen script
            GameObject.Find("tutorialBackground").GetComponent<MeshRenderer>().enabled = false;
            pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
            Timer.GetComponent<TextMeshProUGUI>().enabled = true;
            pressStartText.GetComponent<TextMeshProUGUI>().enabled = false;
            mazePlayer.transform.position = new Vector3(64, 0, 62); //new Vector3(2, 0, 0);
            goalLocation.transform.position = wG.preGoalLocation;
            Debug.Log(mazePlayer.transform.position + "," + goalLocation.transform.position);


            //Debug.Log("Press 'P' to complete maze instantly");

            //mazePlayer.transform.position = new Vector3(64, 0, 62); //new Vector3(2, 0, 0);
            //goalLocation.transform.position = preGoalLocation;


            //pressedPlay = true;
        }
    }
}
