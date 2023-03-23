using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreSystem : MonoBehaviour
{
    public LevelTimer LevelTimer;
    public ReadTSV Quiz;
    public TextMeshProUGUI ScoreText;
    public elevator elevatortest;

    private GameController Gc;

    float Score; //Overall score
    float TimerUp; //Timer counting up (aka TimeTaken)
    float TimeBonus;
    
    public float TimeBonusThreshold = 180;
    public float TimeScoreModifier = 1;
    public float QuizScoreModifier = 1;
    
    float QuizScore;

    public Vector3 PointsX_MazeY_DiscZ_Tile;
    private Vector3 MinigamePoints;
    //LevelTimer.currentTime counts down starting from LevelTimer.startTime
    //public LevelTimeBank TimeBank;

    void AddBonus()
    {
        if (LevelTimer.currentTime >= TimeBonusThreshold)
        {
            TimeBonus = (LevelTimer.startTime - TimerUp) * TimeScoreModifier;
        }
        else
        {
            TimeBonus = 0;
        }
        Score += TimeBonus;
        //Score = (LevelTimer.currentTime + TimeBonus /* + QuizTimer + MiscBonus*/);
    }

    void QuizLoaded()
    {
        Debug.Log("Quiz Loaded message recieved!");
        //AddBonus();
    }

    void CompletedMinigame(int num)
    {
        Debug.Log("CompletedMinigame message recieved!");
        //Minigames:
        //1 = Maze
        //2 = DiscAlignment
        //3 = TileRotation
        switch (num)
        {
            case 1:
                Score += MinigamePoints.x;
                break;
            case 2:
                Score += MinigamePoints.y;
                break;
            case 3:
                Score += MinigamePoints.z;
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


        Score += LevelTimer.currentTime * TimeScoreModifier;

    }

    void CompletedQuiz()
    {
        Score += Quiz.totalPoints * QuizScoreModifier;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("push test");
        MinigamePoints = PointsX_MazeY_DiscZ_Tile;
        //CompletedMinigame(1);
        //QuizScore = Quiz.rightAnswers * BonusThreshold; //Quiz timer * quiz bonus * modifier
        //LevelTimer.currentTime
        // Quiz.rangeOfQuestionsMax * 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(LevelTimer.currentTime);
        TimerUp += Time.deltaTime;
        //TimerUp = (int)TimerUp;

        if (LevelTimer.currentTime >= TimeBonusThreshold)
        {
            TimeBonus = LevelTimer.startTime - TimerUp;
        }
        else
        {
            TimeBonus = 0;
        }

        //At the end of level


        //Score = (LevelTimer.currentTime + TimeBonus /* + QuizTimer + MiscBonus*/);

        ScoreText.text = Score.ToString("000");
        Debug.Log("Score: " + Score);

        if (Quiz.completedQuiz == true) { CompletedQuiz();  } //Score += Quiz.totalPoints;
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
