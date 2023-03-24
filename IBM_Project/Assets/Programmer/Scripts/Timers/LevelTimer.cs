using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public float startTime;
    public static float currentTime;
    public bool TimeUp = false;
    private GameController gC;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI playerstatusText;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        i++;
        Debug.Log("iteration " + i);
        playerstatusText.text = "SAFE";
        currentTime = startTime;
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
                    UpdateTimer(currentTime);
                    playerstatusText.text = gC.PlayerStatus switch
                    {
                        GameController.Status.SAFE => "SAFE",
                        GameController.Status.HUNTED => "HUNTED",
                        GameController.Status.ALERTED => "ALERT",
                        _ => "SAFE"
                    };
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
                TimeUp = true;
            }
        }
        else if(TimeUp)
        {
            gC.GameOver = true;
        }
    }
    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        countdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
