using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    [Header("Scripts")]
    private LevelTimer LevelTimer;
    public ReadTSV Quiz;
    public TextMeshProUGUI ScoreText;
    public elevator elevatortest;

    private GameController Gc;

    float Score; //Overall score
    float TimerUp; //Timer counting up (aka TimeTaken)
    float TimeBonus;

    [Header("Bonus, Level and Quiz Modifiers")]
    public float TimeBonusThreshold = 180;
    public float LevelTimeScoreModifier = 1;
    
    public float QuizScoreModifier = 1;
    
    float QuizScore;

    [Header("Minigame Points")]
    public bool TimerAwardsPoints = true; //If false, will reward the same amount of points regardless of the time of completion (use StaticPointsX_MazeY_DiscZ_Tile) | If true, the amount of points rewarded is based on the time remaining and ScoreModifier (use uiTimers and MinigameTimeScoreModifier)
    public Vector3 StaticPointsX_MazeY_DiscZ_Tile = new Vector3(100, 20, 20); //X = Maze, Y = DiscAlignment, Z = TileRotation | This awards a static amount of points after completing the respective minigame (if TimeAwardsPoints is set to false)
    public Vector3 MinigameTimeScoreModifier = new Vector3(1, 1, 1); //X = Maze, Y = DiscAlignment, Z = TileRotation | The points awarded after completing a minigame: time remaining * MinigameTimerScoreModifier (for the respective minigame)
    private Vector3 MinigamePoints; //Stores values of MinigameTimeScoreModifier for easier referencing (whilst maintaining clarity in inspector)

    //LevelTimer.currentTime counts down starting from LevelTimer.startTime
    //public LevelTimeBank TimeBank;

    void AddLevelBonus()
    {
        //if (LevelTimer.currentTime >= TimeBonusThreshold)
        //{
        //    TimeBonus = (LevelTimer.startTime - TimerUp) * LevelTimeScoreModifier;
        //}
        //else
        //{
        //    TimeBonus = 0;
        //}
        //Score += TimeBonus;
        //Score = (LevelTimer.currentTime + TimeBonus /* + QuizTimer + MiscBonus*/);

        Score += (LevelTimer.currentTime * LevelTimeScoreModifier);

    }

    void QuizLoaded()
    {
        Debug.Log("Quiz Loaded message recieved!");
        AddLevelBonus();
    }

    void CompletedMinigame(Vector2 values) //int num, int remainingTime)
    {
        int num = (int)values.x;
        int remainingTime = (int)values.y;
        Debug.Log("CompletedMinigame message recieved!");
        //Minigames:
        //1 = Maze
        //2 = DiscAlignment
        //3 = TileRotation


        switch (num, TimerAwardsPoints)
        {
            case (1, false):
                Debug.Log("Maze completed, static points awarded");
                Score += MinigamePoints.x;
                break;
            case (2, false):
                Debug.Log("DiscAlignment completed, static points awarded");
                Score += MinigamePoints.y;
                break;
            case (3, false):
                Debug.Log("TileRotation completed, static points awarded");
                Score += MinigamePoints.z;
                break;
            case (1, true):
                Debug.Log("Maze completed, points awarded based on time");
                Score += (remainingTime * MinigameTimeScoreModifier.x);
                break;
            case (2, true):
                Debug.Log("DiscAlignment completed, points awarded based on time" + remainingTime + "| " + MinigameTimeScoreModifier.y);
                Score += (remainingTime * MinigameTimeScoreModifier.y);
                break;
            case (3, true):
                Debug.Log("TileRotation completed, points awarded based on time");
                Score += (remainingTime * MinigameTimeScoreModifier.z);
                break;
            default:
                Debug.LogError("CompletedMinigame() num is " + num + " which isn't a registered minigame number");
                break;
        }
    }

    void CompletedLevel()
    {
        //Original formula from confluence

        //if (LevelTimer.currentTime >= TimeBonusThreshold)
        //{
        //    TimeBonus = (LevelTimer.startTime - TimerUp) * BonusModifier;
        //}
        //else
        //{
        //    TimeBonus = 0;
        //}
        ////Score = (LevelTimer.currentTime + TimeBonus /* + QuizTimer + MiscBonus*/);
        //Score = (LevelTimer.currentTime + TimeBonus);


        Score += LevelTimer.currentTime * LevelTimeScoreModifier;

    }

    void CompletedQuiz()
    {
        Score += Quiz.totalPoints * QuizScoreModifier;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("push test");
        MinigamePoints = StaticPointsX_MazeY_DiscZ_Tile;
        Gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        LevelTimer = GameObject.FindGameObjectWithTag("LevelTimer").GetComponent<LevelTimer>();
        //CompletedMinigame(1);
        //QuizScore = Quiz.rightAnswers * BonusThreshold; //Quiz timer * quiz bonus * modifier
        //LevelTimer.currentTime
        // Quiz.rangeOfQuestionsMax * 2;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!Gc.inMinigame)
        //{
            ScoreText.text = Score.ToString("Score: " + "000");
            //countdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
            //Debug.Log("Score: " + Score);

            if (Quiz.completedQuiz == true) { CompletedQuiz(); } //Score += Quiz.totalPoints;
        //}

        if (Gc.inQuiz || Gc.inMinigame)
        {
            ScoreText.enabled = false;
        }
        else { ScoreText.enabled = true; }

        //if (Gc.minigameWin || Gc.minigameLose || Gc.failMinigame || Gc.)
        //{
        //    ScoreText.enabled = true;
        //}


        ////At the end of level
        ////Debug.Log(LevelTimer.currentTime);
        //TimerUp += Time.deltaTime;
        ////TimerUp = (int)TimerUp;

        //if (LevelTimer.currentTime >= TimeBonusThreshold)
        //{
        //    TimeBonus = LevelTimer.startTime - TimerUp;
        //}
        //else
        //{
        //    TimeBonus = 0;
        //}




        //Score = (LevelTimer.currentTime + TimeBonus /* + QuizTimer + MiscBonus*/);


        //if (Quiz.hackSuccessful == true) {  }
        //if (Hit against target == true) { Score += 4; }

        //if (Hit by a target == true) { Score -= 12; }

        //if (ComputerInteractionSuccessful == true) { Score += 12; } //Temp value
        //Player succeeds maze minigame

        //if (ComputerInteractionSuccessful == false) { Score -= 12; } //Temp value
        //Player fails maze minigame

        //if (DoorInteractionSuccessful == true) { Score += 6; } //Temp value, subject to removal
        //Player succeeds one of the door minigames

        //if (DoorInteractionSuccessful == true) { Score -= 6; } //Temp value, subject to removal
        //Player fails one of the door minigames


    }
}
