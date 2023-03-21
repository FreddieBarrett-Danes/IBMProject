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
    public bool showIngameText;
    public bool showPregameTutorial;

    public walGen wG; //Reference the gameobject with the walGen script

    // Start is called before the first frame update
    void Start()
    {
        pregameText.SetActive(true);
        pressStartText.SetActive(true);
        Timer.SetActive(true);
        GameObject.Find("tutorialBackground").GetComponent<MeshRenderer>().enabled = showPregameTutorial;

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
            wG.Timer.SetActive(true);
            //OnMazeReady(true); //Need to create new gameobject for walGen script
            GameObject.Find("tutorialBackground").GetComponent<MeshRenderer>().enabled = false;
            pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
            Timer.GetComponent<TextMeshProUGUI>().enabled = true;
            pressStartText.GetComponent<TextMeshProUGUI>().enabled = false;
            
            
            //Debug.Log("Press 'P' to complete maze instantly");

            //mazePlayer.transform.position = new Vector3(64, 0, 62); //new Vector3(2, 0, 0);
            //goalLocation.transform.position = preGoalLocation;


            //pressedPlay = true;
        }
    }
}
