using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Inheriting from Lewis' Timer if custom functionality needs to be added
public class Minigame_Timer : UITimer
{
    private void OnEnable()
    {
        walGen.OnMazeReady += timerReady;
    }

    private void OnDisable()
    {
        walGen.OnMazeReady -= timerReady;
    }

    void timerReady(bool timerReady)
    {
        playing = timerReady;
    }
    private void Start()
    {
        playing = false;
        //timer += 5;
    }
}
