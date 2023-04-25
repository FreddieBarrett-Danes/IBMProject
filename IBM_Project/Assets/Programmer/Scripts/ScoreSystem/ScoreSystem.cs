using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScoreSystem : MonoBehaviour
{
    [Header("Scripts")]
    private LevelTimer levelTimer;
    private ReadTSV quiz;
    private TextMeshProUGUI scoreText;
    private elevator elevatorTest;  

    private GameController Gc;

    public float score; //Overall score
    float timerUp; //Timer counting up (aka TimeTaken)
    float timeBonus;

    [Header("Bonus, Level and Quiz Modifiers")]
    public float timeBonusThreshold = 180;
    public float levelTimeScoreModifier = 1;
    
    public float quizScoreModifier = 1;

    public bool losePointsOnIncorrectAnswer;

    public int pointsPerQuestion;
    public bool restarted = false;
    public float scorePool;
    private float quizScore;

    [Header("Minigame Points")]
    public bool timerAwardsPoints = true; //If false, will reward the same amount of points regardless of the time of completion (use StaticPointsX_MazeY_DiscZ_Tile) | If true, the amount of points rewarded is based on the time remaining and ScoreModifier (use uiTimers and MinigameTimeScoreModifier)
    public Vector3 staticPointsXMazeYDiscZTile = new Vector3(100, 20, 20); //X = Maze, Y = DiscAlignment, Z = TileRotation | This awards a static amount of points after completing the respective minigame (if TimeAwardsPoints is set to false)
    public Vector3 minigameTimeScoreModifier = new Vector3(1, 1, 1); //X = Maze, Y = DiscAlignment, Z = TileRotation | The points awarded after completing a minigame: time remaining * MinigameTimerScoreModifier (for the respective minigame)
    private Vector3 minigamePoints; //Stores values of MinigameTimeScoreModifier for easier referencing (whilst maintaining clarity in inspector)
    public MinigameController minigameCont;
    
    private static ScoreSystem scoreInstance;

    private int askedUpdate;

    private int askedListTotal;

    private int tempQuizPoints;

    //LevelTimer.currentTime counts down starting from LevelTimer.startTime
    //public LevelTimeBank TimeBank;

    
    private void Awake()
    {
        
        //DontDestroyOnLoad(this);
        //if (ScoreInstance == null)
        //{
        //    ScoreInstance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    

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

        score += (levelTimer.currentTime * levelTimeScoreModifier);
        //Debug.Log(Score);

    }

    void QuizLoaded()
    {
        //Debug.Log("Quiz Loaded message recieved!");
        AddLevelBonus();
    }

    public void CompletedMinigame(Vector2 values) //int num, int remainingTime)
    {
        int num = (int)values.x;
        int remainingTime = (int)values.y;
        //Debug.Log("CompletedMinigame message recieved!");
        //Minigames:
        //1 = Maze
        //2 = DiscAlignment
        //3 = TileRotation


        switch (num, TimerAwardsPoints: timerAwardsPoints)
        {
            case (1, false):
                //Debug.Log("Maze completed, static points awarded");
                score += minigamePoints.x;
                break;
            case (2, false):
                //Debug.Log("DiscAlignment completed, static points awarded");
                score += minigamePoints.y;
                break;
            case (3, false):
                //Debug.Log("TileRotation completed, static points awarded");
                score += minigamePoints.z;
                break;
            case (1, true):
                //Debug.Log("Maze completed, points awarded based on time remainingTime: " + remainingTime + " MinigameTimeScoreModifier x: " + MinigameTimeScoreModifier.x);
                score += (remainingTime * minigameTimeScoreModifier.x);
                break;
            case (2, true):
                //Debug.Log("DiscAlignment completed, points awarded based on time" + remainingTime + "| " + MinigameTimeScoreModifier.y);
                score += (remainingTime * minigameTimeScoreModifier.y);
                //Debug.Log(Score);
                break;
            case (3, true):
                //Debug.Log("TileRotation completed, points awarded based on time");
                score += (remainingTime * minigameTimeScoreModifier.z);
                break;
            default:
                //Debug.LogError("CompletedMinigame() num is " + num + " which isn't a registered minigame number");
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


        score += levelTimer.currentTime * levelTimeScoreModifier;

    }

    public void QuizQuestionUpdate()
    {
        //QuizScore = Quiz.totalPoints * QuizScoreModifier;

        //Score += (float)(Quiz.correctAnswers * (Quiz.questionsInARow * 0.1));
        //Debug.Log("CorrectAnswers | Total Points | Score " + Quiz.correctAnswersList.Count + " | " + Quiz.totalPoints + " | " + Score);
        //Score += (float)(Quiz.correctAnswersList.Count + (Quiz.questionsInARow * 0.1));
        //Debug.Log("CorrectAnswers | Total Points | Score " + Quiz.correctAnswersList.Count + " | " + Quiz.totalPoints + " | " + Score);

        //Debug.Log("Quiz total points | TempQuizPoints " + Quiz.totalPoints + " | " + TempQuizPoints);

        if (quiz.totalPoints != tempQuizPoints) //(Quiz.totalPoints > TempQuizPoints || Quiz.totalPoints < TempQuizPoints)
        {
            //Score += 1;
            //Debug.Log("Score updated: " + Score + "," + Quiz.totalPoints + "," + TempQuizPoints);
            score += (quiz.totalPoints - tempQuizPoints);
            score += pointsPerQuestion;
            tempQuizPoints = quiz.totalPoints;
            //Debug.Log("Score updated: " + Score + "," + Quiz.totalPoints + "," + TempQuizPoints);
        }
        //if (Quiz.totalPoints < TempQuizPoints)
        //{
        //    //Score -= 1;
        //    Score -= (Quiz.totalPoints - TempQuizPoints);
        //    TempQuizPoints = Quiz.totalPoints;
        //}
    }

    void CompletedQuiz()
    {
        //Debug.Log("CorrectAnswers | Total Points | Score " + Quiz.correctAnswersList.Count + " | " + Quiz.totalPoints + " | " + Score);
        //Score += Quiz.totalPoints * QuizScoreModifier;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("ScoreSystem Awake");
        Gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //mC = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        quiz = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        levelTimer = GameObject.FindGameObjectWithTag("LevelTimer").GetComponent<LevelTimer>();
        askedUpdate = quiz.cloudAskedList.Count + quiz.aiAskedList.Count + quiz.dataAskedList.Count + quiz.quantumAskedList.Count + quiz.securityAskedList.Count;
        minigameCont = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        

        
        
        DontDestroyOnLoad(this);
        if (scoreInstance == null)
        {
            scoreInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        restarted = false;
        tempQuizPoints = quiz.totalPoints; //Move to Awake()
        //Debug.Log("push test");
        minigamePoints = staticPointsXMazeYDiscZTile;
        //Gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //LevelTimer = GameObject.FindGameObjectWithTag("LevelTimer").GetComponent<LevelTimer>();
        //CompletedMinigame(1);
        //QuizScore = Quiz.rightAnswers * BonusThreshold; //Quiz timer * quiz bonus * modifier
        //LevelTimer.currentTime
        // Quiz.rangeOfQuestionsMax * 2;
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this.gameObject);
        }
        //Debug.Log("ScoreSystem Awake");
        //Gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        ////mC = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        //Quiz = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
        //ScoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        //LevelTimer = GameObject.FindGameObjectWithTag("LevelTimer").GetComponent<LevelTimer>();
        //AskedUpdate = Quiz.askedList.Count;
        if (Gc == null)
        {
            Gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
        //mC = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        if (quiz == null)
        {
            quiz = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
        }
        if (scoreText == null)
        {
            scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        }
        if (levelTimer == null)
        {
            levelTimer = GameObject.FindGameObjectWithTag("LevelTimer").GetComponent<LevelTimer>();
        }
/*        if(restarted)
        {
            scorePool = Score;
            Debug.Log("Score " +scorePool);
        }*/

        if (Gc != null)
        {
            //Debug.Log("Score: " + Score);
            //if (!Gc.inMinigame)
            //{
            scoreText.text = score.ToString("Score:" + "0");
            //countdownText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
            //Debug.Log("Score: " + Score);

            if (quiz.completedQuiz == true) { CompletedQuiz(); } //Score += Quiz.totalPoints;


            if (Gc.inQuiz || Gc.inMinigame)
            {
                scoreText.enabled = false;
            }
            else { scoreText.enabled = true; }

            if (Gc.inQuiz && askedListTotal > askedUpdate) { askedUpdate = askedListTotal; Debug.Log("QuestionUpdate"); QuizQuestionUpdate(); }

            
           /* if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("Score: " + Score);
                Debug.Log(AskedUpdate); // + " | " + //Quiz.askedList.Count);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                Score += 100;
                ScoreText.text = Score.ToString("000");
            }*/
            
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
        else if (Gc == null && SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }
}
