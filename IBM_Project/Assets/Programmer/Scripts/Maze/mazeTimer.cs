using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Inheriting from Lewis' Timer if custom functionality needs to be added
public class mazeTimer : UITimer
{

    private void Start()
    {
        timer += 5;
    }
}
