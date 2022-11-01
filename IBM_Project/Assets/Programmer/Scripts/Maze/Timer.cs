using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Copying Lewis' Timer to a new script as a placeholder. When possible, pull git project to use completed timer
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public bool playing;
    public float timer = 180;

    private void Start()
    {
        timer += 5;
    }

    private void Update()
    {
        if (!playing) return;
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("000");
        //timerText.GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
