using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//Inheriting from Lewis' Timer if custom functionality needs to be added
public class Minigame_Timer : UITimer
{
    private GameController gC;

    private void OnEnable()
    {
        //walGen.OnMazeReady += timerReady; //Maze script
        
        mazeHandler.OnMazeReady += timerReady;
        Updated_Disc_Rotation.OnDiscAlignmentReady += timerReady; //Disc Alignment script
        genGrid.OnTileRotationReady += timerReady; //Tile Rotation script
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gC.failMinigame = false;


    }

    private void OnDisable()
    {
        //walGen.OnMazeReady -= timerReady;
        mazeHandler.OnMazeReady -= timerReady;
        //Updated_Disc_Rotation.OnDiscAlignmentReady -= timerReady;
        //genGrid.OnTileRotationReady -= timerReady;
        timer = timerOrigin;
    }

    void timerReady(bool timerReady)
    {
        playing = timerReady;
        //Debug.Log("Timer status: " + playing);
    }

    void checkTimer()
    {
        if (timer <= 0)
        {
            //Debug.Log("Times up, fail minigame!");
        }
    }

    private void Start()
    {
        playing = false;
        timerOrigin = timer;
        //timer += 5;
    }

    private void Update()
    {
        if (!playing) return;
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("000");

        if (timer <= 0)
        {
            //Debug.Log("Times up, fail minigame!");
            gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            gC.failMinigame = true;
            if (gC.playerStatus == GameController.Status.HUNTED)
                return;
            else
                gC.playerStatus = GameController.Status.ALERTED;
            gC.inMinigame = false;
            gameObject.SetActive(false);
            //Debug.Log("Minigame failed, exit minigame and set droids to alert state");
            //playing = false;
        }
    }
}
