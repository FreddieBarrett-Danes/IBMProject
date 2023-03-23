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

    float Score; //Overall score
    float TimerUp; //Timer counting up (aka TimeTaken)
    float TimeBonus;
    
    float BonusThreshold = 180;
    float BonusModifier = 1;
    
    float QuizScore;
    //LevelTimer.currentTime counts down starting from LevelTimer.startTime
    //public LevelTimeBank TimeBank;

    void AddBonus()
    {
        if (LevelTimer.currentTime >= BonusThreshold)
        {
            TimeBonus = (LevelTimer.startTime - TimerUp) * BonusModifier;
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
                Score += 12;
                break;
            case 2:
                Score += 6;
                break;
            case 3:
                Score += 6;
                break;
            default:
                Debug.LogError("CompletedMinigame() num is " + num + " which isn't a registered minigame number");
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //CompletedMinigame(1);
        //QuizScore = Quiz.rightAnswers * BonusThreshold; //Quiz timer * quiz bonus * modifier
        //LevelTimer.currentTime
        // Quiz.rangeOfQuestionsMax * 2;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(LevelTimer.currentTime);
        TimerUp += Time.deltaTime;
        //TimerUp = (int)TimerUp;

        //if (LevelTimer.currentTime >= BonusThreshold)
        //{
        //    TimeBonus = LevelTimer.startTime - TimerUp;
        //}
        //else
        //{
        //    TimeBonus = 0;
        //}

        //At the end of level
        

        //Score = (LevelTimer.currentTime + TimeBonus /* + QuizTimer + MiscBonus*/);
        
        ScoreText.text = Score.ToString("000");
        Debug.Log("Score: " + Score);

        if (Quiz.completedQuiz == true) { }

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
