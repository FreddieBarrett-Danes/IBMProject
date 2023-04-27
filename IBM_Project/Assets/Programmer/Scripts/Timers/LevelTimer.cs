using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public float startTime;
    public float currentTime;
    public bool timeUp = false;
    private GameController gC;

    public TextMeshProUGUI countdownText;

    private static LevelTimer timerInstance;
    // Start is called before the first frame update

    private void Awake()
    {
        gC = GameObject.Find("GameController").GetComponent<GameController>();
        DontDestroyOnLoad(this);
        if (timerInstance == null) 
        { 
            timerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        countdownText = FindObjectOfType<FinderScript>().transform.GetChild(1).GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        currentTime = startTime;
        //countdownText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this.gameObject);
        }
        if(countdownText == null)
        {
            countdownText = FindObjectOfType<FinderScript>().transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
        if (!timeUp)
        {
            if (currentTime > 0)
            {
                //in play time
                if (!gC.inMinigame)
                {
                    currentTime -= Time.deltaTime;
                    UpdateTimer(currentTime);
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
                timeUp = true;
            }
        }
        else
        {
            gC.gameOver = true;
        }
    }
    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
