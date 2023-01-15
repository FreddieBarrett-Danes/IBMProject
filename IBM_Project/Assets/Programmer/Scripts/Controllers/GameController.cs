using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //keeps track of if a bot has alerted, is safe or is hunting maybe keep[ track in player controller
    //keeps track if in lavel/mingame or quiz
    private LevelTimer levelTimer;

    public float LevelTimeBank = 0.0f;
    public bool completedLevel = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        levelTimer = GameObject.FindGameObjectWithTag("Level Timer").GetComponent<LevelTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!completedLevel)
        {
            //playing level
        }
        else
        {
            LevelTimeBank += levelTimer.currentTime;
        }
    }
}
