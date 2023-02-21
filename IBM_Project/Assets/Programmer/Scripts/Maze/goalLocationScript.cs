using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalLocationScript : MonoBehaviour
{
    private GameController gC;
    private MinigameController mC;
    private ComputerInteraction computerInteraction;
    public mazePlayerScript mPlayer;

    public walGen wG;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("mazePlayer"))
        {
            Debug.Log("Maze Win");
            Debug.Log("Refer to goalLocationScript for Maze output");
            mPlayer.timesHit = 0;
            gC.inMinigame = false;
            wG.Timer.SetActive(false);
            mC.completedMaze = true;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        mC = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        computerInteraction = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();
    }
    private void Update()
    {
        Debug.Log(mPlayer.timesHit);
        if (mPlayer != null && mPlayer.timesHit >= 6)
        {
            mC.interactMaze = false;
            gC.inMinigame = false;
            gC.PlayerStatus = GameController.Status.ALERTED;
            wG.Timer.SetActive(false);
            computerInteraction.mazeFailed = true;
            //mC.completedMaze = false;
            mPlayer.timesHit = 0;
        }
    }
}
