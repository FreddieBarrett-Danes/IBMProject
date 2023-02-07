using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public float startTime;
    public float currentTime;
    public bool TimeUp = false;
    private GameController gC;

    public TextMeshProUGUI countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        gC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
        //countdownText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {   
        if (!TimeUp)
        {
            if (currentTime > 0)
            {
                //in play time
                if (!gC.inMinigame)
                {
                    currentTime -= Time.deltaTime;
                    updateTimer(currentTime);
                }
                else
                {
                    //countdownText.IsActiveAndEnabled(false)
                }
            }
            else
            {
                //time has run out
                currentTime = 0;
                TimeUp = false;
            }
        }
    }
    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        countdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
