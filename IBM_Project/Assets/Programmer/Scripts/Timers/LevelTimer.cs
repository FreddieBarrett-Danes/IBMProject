using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public float startTime;
    private float currentTime;

    public TextMeshProUGUI countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        
        if(currentTime > 0)
        {
            //in play time
        }
        else
        {
            //time has run out
            currentTime = 0;
        }
    }
    void updatetimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        countdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
