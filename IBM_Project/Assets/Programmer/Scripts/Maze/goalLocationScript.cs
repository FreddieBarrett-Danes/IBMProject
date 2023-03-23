using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalLocationScript : MonoBehaviour
{
    private GameController gC;
    private MinigameController mC;
    private ComputerInteraction computerInteraction;
    public mazePlayerScript mPlayer;

    public walGen wG; //Re-reference the gameobject with the walGen script

    private Vector3 originalCameraPos;

    public Camera camera;
    public bool setCameraPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("mazePlayer"))
        {
            Debug.Log(mPlayer.transform.position + "," + transform.position);
            Debug.Log("Maze Win");
            Debug.Log("Refer to goalLocationScript for Maze output");
            //CameraMaze call - Sets position of the camera
            if (setCameraPosition == false) { cameraMaze(false); };

            mPlayer.timesHit = 0;
            gC.inMinigame = false;
            wG.Timer.SetActive(false);
            mC.completedMaze = true;
        }
    }

    void cameraMaze(bool inMaze)
    {
        if (setCameraPosition == true)
        {
            //Debug.Log("repositioned camera for maze, refer to goalLocation to disable");
            if (inMaze == true)
            {
                camera.GetComponent<Camera>().transform.position += new Vector3(62, 0, 60);
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
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        mC = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        computerInteraction = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();
        originalCameraPos = camera.GetComponent<Camera>().transform.position;
        cameraMaze(true);
    }
    private void Update()
    {
        if (mPlayer != null && mPlayer.timesHit >= 6)
        {
            mC.interactMaze = false;
            gC.inMinigame = false;
            if (gC.PlayerStatus == GameController.Status.HUNTED)
                return;
            else
                gC.PlayerStatus = GameController.Status.ALERTED;
            wG.Timer.SetActive(false);
            computerInteraction.mazeFailed = true;
            //mC.completedMaze = false;
            mPlayer.timesHit = 0;
        }

        
        
            cameraMaze(true);
        
    }
}
