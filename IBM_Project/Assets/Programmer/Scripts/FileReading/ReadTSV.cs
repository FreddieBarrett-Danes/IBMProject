using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReadTSV : MonoBehaviour
{
    [Header("Game Controller Intergration")]
    [Space]
    public bool completedQuiz = false;
    private MinigameController mC;
    private GameController gC;
    public bool hackSuccessful;

    [Header("Initialisation Parameters")]
    [Space]
    public float submitButtonSizeMultiplier; //Multiplier of the Submit text-box 
    public List<GameObject> answersList;

    //TSV FILES
    [Header("TSV Files")]
    [Space]

    public TextAsset TSVFile;

    public TextAsset CloudTSV;
    public int cloudRangeMax;

    public TextAsset AITSV;
    public int aiRangeMax;

    public TextAsset DataTSV;
    public int dataRangeMax;

    public TextAsset QuantumTSV;
    public int quantumRangeMax;

    public TextAsset SecurityTSV;
    public int securityRangeMax;

    public int shipNumber;

    public int questionsInARow;
    public int loopNumber;
    private GameObject questionText;
    private GameObject submitText;
    private Button submitButton;
    [Range(0.1f, 1f)]
    private GameObject canvas;
    private RectTransform canvasRectTransform;
    private float timer;
    [SerializeField]
    private float waitTime;
    private bool waiting;
    //public int rangeOfQuestionsMax = 6; //find a way to set this automatically
    //[Range(1, 6)]
    public int row;

    private const int _questionRow = 0,
                      _answer1 = 1,
                      _answer2 = 2,
                      _answer3 = 3,
                      _answer4 = 4,
                      _amountOfAnswers = 5,
                      _timeForQuestions = 6;

    [Header("Quiz Stats")]
    [Space]

    public int rightAnswers; //How many the player got right
    public int correctAnswers; //How many correct answers there are
    private int amountSelected;

    //public List<int> incorrectAnswersList; //List of questions the user answered Wrong
    public List<int> cloudIncorrectAnswersList;
    public List<int> aiIncorrectAnswersList;
    public List<int> dataIncorrectAnswersList;
    public List<int> quantumIncorrectAnswersList;
    public List<int> securityIncorrectAnswersList;

    //public List<int> correctAnswersList; //List of questions the user answered Correct
    public List<int> cloudCorrectAnswersList; 
    public List<int> aiCorrectAnswersList; 
    public List<int> dataCorrectAnswersList; 
    public List<int> quantumCorrectAnswersList; 
    public List<int> securityCorrectAnswersList; 

    //public List<int> askedList; //List of questions already asked
    public List<int> cloudAskedList; 
    public List<int> aiAskedList; 
    public List<int> dataAskedList; 
    public List<int> quantumAskedList; 
    public List<int> securityAskedList; 

    private int tempCorrect;
    private int tempWrong;

    [SerializeField]
    private float timeForQuestion;
    [SerializeField]
    private float fallbackTimeForQuestion;

    [Header("Points")]
    [Space]

    public int totalPoints;
    //public int points;

    [Header("Visuals")]
    [Space]

    public GameObject singleSelected;
    private GameObject tempSingleSelected;
    public GameObject panel;
    public Vector2 panelSize;
    public float smallestFont;
    [Range(0.1f, 1f)]
    public float fontSizeMultiplier;

    private float startX;
    private float startY;

    [Header("Generation")]
    [Space]

    public bool find; //Use this to generate the row,column that you've selected using Row and Column
    [SerializeField]
    private bool submit;

    [Header("Debug")]
    public bool debug;

    int InitialiseTSVs(int shipNumber)
    {
        int i = 0;
        string cellValue = Find(i, 0, shipNumber);
        while (cellValue != "")
        {
            //Debug.Log(cellValue);
            i++;
            cellValue = Find(i, 0, shipNumber);
        }
        return i - 1;
    }

    List<int> Shuffle(int length)
    {
        List<int> orderedList = new List<int>(4);

        for (int i = 1; i < length + 1; i++) //create list of numbers 1-4
        {
            orderedList.Add(i);
        }

        return orderedList;
    }

    public static List<int> FisherYatesShuffle(List<int> list)
    {
        System.Random sysRandom = new System.Random();

        int tempInt;

        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(sysRandom.NextDouble() * (n - i)); // NextDouble returns a random number between 0 and 1 (dont ask)
            tempInt = list[r];
            list[r] = list[i];
            list[i] = tempInt;
        }

        return list;
    }

    string Find(int findRow, int findColumn)
    {
        string rv = null;

        find = false;

        var dataset = CloudTSV;

        if (gC.Ship1)
        {
            dataset = CloudTSV;
        }
        else if (gC.Ship2)
        {
            dataset = AITSV;
        }
        else if (gC.Ship3)
        {
            dataset = DataTSV;
        }
        else if (gC.Ship4)
        {
            dataset = QuantumTSV;
        }
        else if (gC.Ship5)
        {
            dataset = SecurityTSV;
        }

        var splitDataset = dataset.text.Split(new char[] { '\n' });

        if (findRow < 1)
        {
            findRow = 1;
            //Debug.LogWarning("Desired Row given in the Find() function located on: " + this.gameObject.name + " was out of bounds. It was automatically brought back into range. - ask Istvan");
        }

        if (findColumn < 1)
        {
            findColumn = 1;
            //Debug.LogWarning("Desired Column given in the Find() function located on: " + this.gameObject.name + " was out of bounds. It was automatically brought back into range. - ask Istvan");
        }

        for (int i = 0; i < findRow; i++)
        {
            char tabSpace = '\u0009'; //takes the TAB ascii code as the splitter character. this value used to be a , when using CSV but now we are using TSV.
            var data = splitDataset[i].Split(tabSpace.ToString()); //
            //var data = splitDataset[i].Split(',');
            for (int j = 0; j < findColumn; j++)
            {
                if (findRow > splitDataset.Length) findRow = splitDataset.Length;
                if (findColumn > data.Length) findColumn = data.Length;

                //questionText.text = data[j];

                rv = data[j];
            }
        }
        //Debug.Log(rv);
        //Debug.Log(findColumn);
        //Debug.Log(findRow);
        return rv;
    }

    string Find(int findRow, int findColumn, int shipNumber)
    {
        string rv = null;

        find = false;

        var dataset = CloudTSV;

        if (shipNumber == 1)
        {
            dataset = CloudTSV;
        }
        else if (shipNumber == 2)
        {
            dataset = AITSV;
        }
        else if (shipNumber == 3)
        {
            dataset = DataTSV;
        }
        else if (shipNumber == 4)
        {
            dataset = QuantumTSV;
        }
        else if (shipNumber == 5)
        {
            dataset = SecurityTSV;
        }

        var splitDataset = dataset.text.Split(new char[] { '\n' });

        if (findRow < 1)
        {
            findRow = 1;
            //Debug.LogWarning("Desired Row given in the Find() function located on: " + this.gameObject.name + " was out of bounds. It was automatically brought back into range. - ask Istvan");
        }

        if (findColumn < 1)
        {
            findColumn = 1;
            //Debug.LogWarning("Desired Column given in the Find() function located on: " + this.gameObject.name + " was out of bounds. It was automatically brought back into range. - ask Istvan");
        }

        for (int i = 0; i < findRow; i++)
        {
            char tabSpace = '\u0009'; //takes the TAB ascii code as the splitter character. this value used to be a , when using CSV but now we are using TSV.
            var data = splitDataset[i].Split(tabSpace.ToString()); //
            //var data = splitDataset[i].Split(',');
            for (int j = 0; j < findColumn; j++)
            {
                if (findRow > splitDataset.Length) findRow = splitDataset.Length;
                if (findColumn > data.Length) findColumn = data.Length;

                //questionText.text = data[j];

                rv = data[j];
            }
        }
        //Debug.Log(rv);
        //Debug.Log(findColumn);
        //Debug.Log(findRow);
        return rv;
    }

    public void submitClicked()
    {
        //Debug.Log("submit clicked");
        submit = true;
        //DON'T WRITE ANYTHING UNDER HERE
        //UNITY UI WILL LAG YOU INTO NEXT SUNDAY
        //Caused by button hold for a couple of frames .... I think
    }

    public void nonDuplicateRow()
    {
        if (gC.Ship1)
            row = Random.Range(1, cloudRangeMax + 1);
        else if (gC.Ship2)
            row = Random.Range(1, aiRangeMax + 1);
        else if (gC.Ship3)
            row = Random.Range(1, dataRangeMax + 1);
        else if (gC.Ship4)
            row = Random.Range(1, quantumRangeMax + 1);
        else if (gC.Ship5)
            row = Random.Range(1, securityRangeMax + 1);

        if (gC.Ship1)
        {
            for (int i = 0; i < cloudIncorrectAnswersList.Count; i++)
            {
                if (row == cloudIncorrectAnswersList[i])
                {
                    //Debug.Log("alredy asked question: " + cloudIncorrectAnswersList[i]);
                    nonDuplicateRow(); //Re-runs the randomisation to find one that has not been used yet
                }
            }
            //cloudIncorrectAnswersList

        }
        else if (gC.Ship2)
        {
            for (int i = 0; i < aiIncorrectAnswersList.Count; i++)
            {
                if (row == aiIncorrectAnswersList[i])
                {
                    //Debug.Log("alredy asked question: " + aiIncorrectAnswersList[i]);
                    nonDuplicateRow(); //Re-runs the randomisation to find one that has not been used yet
                }
            }
            //aiIncorrectAnswersList =

        }
        else if (gC.Ship3)
        {
            for (int i = 0; i < dataIncorrectAnswersList.Count; i++)
            {
                if (row == dataIncorrectAnswersList[i])
                {
                    //Debug.Log("alredy asked question: " + dataIncorrectAnswersList[i]);
                    nonDuplicateRow(); //Re-runs the randomisation to find one that has not been used yet
                }
            }
            //dataIncorrectAnswersList

        }
        else if (gC.Ship4)
        {
            for (int i = 0; i < quantumIncorrectAnswersList.Count; i++)
            {
                if (row == quantumIncorrectAnswersList[i])
                {
                    //Debug.Log("alredy asked question: " + quantumIncorrectAnswersList[i]);
                    nonDuplicateRow(); //Re-runs the randomisation to find one that has not been used yet
                }
            }
            //quantumIncorrectAnswersList

        }
        else if (gC.Ship5)
        {
            for (int i = 0; i < securityIncorrectAnswersList.Count; i++)
            {
                if (row == securityIncorrectAnswersList[i])
                {
                    //Debug.Log("alredy asked question: " + securityIncorrectAnswersList[i]);
                    nonDuplicateRow(); //Re-runs the randomisation to find one that has not been used yet
                }
            }
            //securityIncorrectAnswersList

        }

        /*for (int i = 0; i < askedList.Count; i++)
        {
            if (row == askedList[i])
            {
                Debug.Log("alredy asked question: " + askedList[i]);
                nonDuplicateRow(); //Re-runs the randomisation to find one that has not been used yet
            }
        }*/
    }

    void Start()
    {
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        mC = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        canvas = GameObject.FindGameObjectWithTag("Canvas"); // may be ambiguous if theres several //why tf would there be several?? //oh coz if you combine scens //scenes cant read between eachother
        canvasRectTransform = canvas.GetComponent<RectTransform>();

        cloudRangeMax = InitialiseTSVs(1);
        aiRangeMax = InitialiseTSVs(2);
        dataRangeMax = InitialiseTSVs(3);
        quantumRangeMax = InitialiseTSVs(4);
        securityRangeMax = InitialiseTSVs(5);

        //Debug.Log((panelSize));
    }

    void Update()
    {
        if (find) //File Reading / generate
        {
            gC.inMinigame = true;
            nonDuplicateRow(); //Generates a row number that is not on askedList.

            ////
            /// This section checks if there is a declared limit time for the question being asked.
            /// If not it will be set to fallbackTimeForQuestion
            ////

            //Debug.Log(Find(row, 0));

            timeForQuestion = fallbackTimeForQuestion;

            float parseOutput = 0;

            // Debug.Log("Value is :" + Find(row, 7) + ":");

            if (Find(row, 7) != "")
            {
                timeForQuestion = fallbackTimeForQuestion;
            }

            if (float.TryParse(Find(row, 7), out parseOutput))
            {
                timeForQuestion = parseOutput;
            }
            else
            {
                timeForQuestion = fallbackTimeForQuestion;
            }

            //Debug.Log(parseOutput);

            //row = Random.Range(1, rangeOfQuestionsMax);

            if (answersList.Count != 0) //Reset list
            {
                for (int i = 0; i < answersList.Count; i++)
                {
                    Destroy(answersList[i]);
                }
                Destroy(questionText);
                Destroy(submitText);
                answersList.Clear();
            }

            startX = 0.25f; //Set start values for coords.
            startY = 0.29f; // 0.25f by default

            /*if (Find(row, 4) == null || Find(row, 4) == "") //there are 2 answers
            {
                Debug.Log("question has no 3rd answer");
                panelSize = new Vector2((canvasRectTransform.sizeDelta.x * 0.9f) / 2, canvasRectTransform.sizeDelta.y * 0.5f); //Find panel size
            }

            if (Find(row, 6) != null || Find(row, 6) != "") //there are 4 answers
            {
                Debug.Log("question has 4th answer");

            }*/

            panelSize = new Vector2((canvasRectTransform.sizeDelta.x * 0.9f) / 2, canvasRectTransform.sizeDelta.y * 0.25f); //Find panel size

            rightAnswers = 0; //reset the counter for how many answers player got right

            //Instantiating question box

            questionText = Instantiate(panel);
            questionText.transform.SetParent(canvas.transform);
            questionText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRectTransform.sizeDelta.x * 0.95f, panelSize.y);
            questionText.GetComponent<RectTransform>().position = new Vector2(canvasRectTransform.sizeDelta.x / 2, canvasRectTransform.sizeDelta.y * 0.83f);

            Destroy(questionText.GetComponent<Button>());
            Destroy(questionText.GetComponent<answersScript>());

            if (questionText.GetComponentInChildren<TextMeshProUGUI>().isTextOverflowing == true)
            {
                //Debug.Log("Text overflow");
            }

            questionText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRectTransform.sizeDelta.x * 0.95f, panelSize.y); //set size of textbox

            //Instantiating submit box

            submitText = Instantiate(panel);
            submitText.transform.SetParent(canvas.transform);
            submitText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRectTransform.sizeDelta.x * 0.45f, panelSize.y * 0.5f);
            submitText.GetComponent<RectTransform>().position = new Vector2(canvasRectTransform.sizeDelta.x / 2, canvasRectTransform.sizeDelta.y * 0.085f);


            submitText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRectTransform.sizeDelta.x * 0.95f, panelSize.y); //set size of textbox
            submitText.GetComponentInChildren<TextMeshProUGUI>().text = ("Submit Answer");
            submitText.GetComponent<Transform>().GetChild(0).GetComponent<RectTransform>().localScale *= submitButtonSizeMultiplier;

            submitButton = submitText.GetComponent<Button>();

            Destroy(submitText.GetComponentInChildren<answersScript>());

            if (Find(row, 4) == "")
            {
                Debug.Log("question has 2 answers");
                //Instantiating answer boxes
                for (int i = 0; i < 2; i++)
                {
                    GameObject tempPanelY = Instantiate(panel);
                    tempPanelY.transform.SetParent(canvas.transform);
                    tempPanelY.GetComponent<RectTransform>().sizeDelta = panelSize * new Vector2(1f, 1.5f); // Sets the size of the text-box for the answer

                    tempPanelY.GetComponent<RectTransform>().position = new Vector2(canvasRectTransform.sizeDelta.x * startX, (canvasRectTransform.sizeDelta.y * startY) * 1.5f); // Sets the position of the text-box for the answer

                    tempPanelY.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = panelSize;

                    answersList.Add(tempPanelY);

                    startX += 0.5f;

                    /*for (int j = 0; j < 1; j++)
                    {
                        GameObject tempPanelZ = Instantiate(panel);

                        tempPanelZ.transform.SetParent(canvas.transform);
                        tempPanelZ.GetComponent<RectTransform>().sizeDelta = panelSize;

                        tempPanelZ.GetComponent<RectTransform>().position = new Vector2((canvasRectTransform.sizeDelta.x * startX), canvasRectTransform.sizeDelta.y * startY);

                        tempPanelZ.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = panelSize; //set size of textbox

                        answersList.Add(tempPanelZ);

                        startX -= 0.5f;
                        startY += 0.28f; //was 0.33
                    }*/
                }
            }

            else
            {
                //Debug.Log("question has 4 answers");
                //Instantiating answer boxes
                for (int i = 0; i < 2; i++)
                {
                    GameObject tempPanelY = Instantiate(panel);
                    tempPanelY.transform.SetParent(canvas.transform);
                    tempPanelY.GetComponent<RectTransform>().sizeDelta = panelSize;

                    tempPanelY.GetComponent<RectTransform>().position = new Vector2(canvasRectTransform.sizeDelta.x * startX, canvasRectTransform.sizeDelta.y * startY);

                    tempPanelY.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = panelSize; //set size of textbox

                    answersList.Add(tempPanelY);

                    startX += 0.5f;

                    for (int j = 0; j < 1; j++)
                    {
                        GameObject tempPanelZ = Instantiate(panel);

                        tempPanelZ.transform.SetParent(canvas.transform);
                        tempPanelZ.GetComponent<RectTransform>().sizeDelta = panelSize;

                        tempPanelZ.GetComponent<RectTransform>().position = new Vector2((canvasRectTransform.sizeDelta.x * startX), canvasRectTransform.sizeDelta.y * startY);

                        tempPanelZ.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = panelSize; //set size of textbox

                        answersList.Add(tempPanelZ);

                        startX -= 0.5f;
                        startY += 0.28f; //was 0.33
                    }
                }
            }


            panel.GetComponent<RectTransform>().sizeDelta = panelSize;

            panel.GetComponent<RectTransform>().position = new Vector2((canvasRectTransform.sizeDelta.x * startX), canvasRectTransform.sizeDelta.y * startY);

            int.TryParse(Find(row, 6), out correctAnswers); // randomissation added here;

            if (correctAnswers < 1)
            {
                correctAnswers = 1;
                //Debug.LogWarning(this.name + " was unable to determine how many correct answers there are. It was automatically set to 1. - ask Istvan");
            }

            /*for(int j = 0; j < correctAnswers; j++) //This determines which answers are the correct ones
            {
                Debug.Log(j);
            }*/

            List<int> orderList;
            //List<int> orderList = new List<int>(FisherYatesShuffle(Shuffle(4)));

            if (Find(row, 4) == "")
            {
                orderList = new List<int>(FisherYatesShuffle(Shuffle(2)));
            }

            else
            {
                orderList = new List<int>(FisherYatesShuffle(Shuffle(4)));
            }


            for (int i = 0; i < answersList.Count; i++)
            {
                if (orderList[i] <= correctAnswers)
                {
                    answersList[i].GetComponent<answersScript>().isCorrect = true;
                }

                //Debug.Log(orderList[i]);

                answersList[i].GetComponentInChildren<TextMeshProUGUI>().text = Find(row, orderList[i] + 1).ToString();
            }

            questionText.GetComponentInChildren<TextMeshProUGUI>().text = Find(row, 1).ToString();

            loopNumber++;
        }

        amountSelected = 0;

        for (int i = 0; i < answersList.Count; i++) // finds how many buttons have been selected
        {
            if (answersList[i] != null)
            {
                if (answersList[i].GetComponent<answersScript>().selected)
                {
                    amountSelected++;
                }
            }
        }

        //Debug.Log(amountSelected);

        if (amountSelected != 0) //allows submit to be clicked if something is selected //THIS IS'NT WORKING AS INTENDED
        {
            submitButton.onClick.AddListener(submitClicked);
        }

        if (amountSelected >= correctAnswers)
        {
            for (int i = 0; i < answersList.Count; i++)
            {
                if (!answersList[i].GetComponent<answersScript>().selected && answersList[i].GetComponent<Button>() != null)
                {
                    answersList[i].GetComponent<Button>().interactable = false;
                }
            }
        }
        else if ((amountSelected < correctAnswers) && completedQuiz)
        {
            for (int i = 0; i < answersList.Count; i++)
            {
                if (answersList[i].GetComponent<Button>() != null)
                    answersList[i].GetComponent<Button>().interactable = true;
                else
                    break;
            }
        }

        ////
        /// This section is to make the timer tick down for the timeForQuestion
        ////

        if (timeForQuestion < 0 && answersList.Count != 0)
        {
            //Question time is up
            amountSelected = 1;
            submit = true;
        }

        timeForQuestion -= Time.deltaTime;

        ////
        ///
        ////

        if (submit == true && amountSelected != 0 && waiting == false && answersList[0] != null)
        {
            //This segment resets all of the temp counter values
            //It is used to measure how many questions the user answered right/wrong. It is used for calculations later...
            tempCorrect = 0;
            tempWrong = 0;

            for (int i = 0; i < answersList.Count; i++) //check to make sure correct answers were ticked and set colours
            {
                //if(answersList[i] != null)
                Destroy(answersList[i].GetComponent<Button>());

                if (answersList[i].GetComponent<answersScript>().isCorrect && answersList[i].GetComponent<answersScript>().selected) // selected is correct
                {
                    rightAnswers++;
                    answersList[i].GetComponent<answersScript>().panelColour = Color.green; // set colour to green
                    tempCorrect++;
                    //answersList[i].GetComponent<Image>().color);
                }
                else if (!answersList[i].GetComponent<answersScript>().isCorrect && answersList[i].GetComponent<answersScript>().selected) // selected is incorrect
                {
                    answersList[i].GetComponent<answersScript>().panelColour = Color.red; // set colour to red
                    tempWrong++;
                }
                else if (answersList[i].GetComponent<answersScript>().isCorrect && !answersList[i].GetComponent<answersScript>().selected) // not seleced correct answer
                {
                    answersList[i].GetComponent<answersScript>().panelColour = new Color(1, 0.5f, 0, 1); // set colour to orange
                    tempWrong++;
                }
            }

            //set this question as asked
            //askedList.Add(row); //double check this works

            if (gC.Ship1)
            {
                cloudAskedList.Add(row);

            }
            else if (gC.Ship2)
            {
                aiAskedList.Add(row);

            }
            else if (gC.Ship3)
            {
                dataAskedList.Add(row);

            }
            else if (gC.Ship4)
            {
                quantumAskedList.Add(row);

            }
            else if (gC.Ship5)
            {
                securityAskedList.Add(row);

            }

            //set this question right/wrong/partial depending on answer
            if (tempWrong > 0)
            {
                if (gC.Ship1)
                {
                    cloudIncorrectAnswersList.Add(row);

                }
                else if (gC.Ship2)
                {
                    aiIncorrectAnswersList.Add(row);

                }
                else if (gC.Ship3)
                {
                    dataIncorrectAnswersList.Add(row);

                }
                else if (gC.Ship4)
                {
                    quantumIncorrectAnswersList.Add(row);

                }
                else if (gC.Ship5)
                {
                    securityIncorrectAnswersList.Add(row);

                }
                
                //incorrectAnswersList.Add(row);
            }

            //Set this question as answered fully correct
            else
            {
                if (gC.Ship1)
                {  
                    cloudCorrectAnswersList.Add(row);    

                }
                else if (gC.Ship2)
                {
                    aiCorrectAnswersList.Add(row); 

                }
                else if (gC.Ship3)
                {
                    dataCorrectAnswersList.Add(row); 

                }
                else if (gC.Ship4)
                {
                    quantumCorrectAnswersList.Add(row); 
                   
                }
                else if (gC.Ship5)
                {
                    securityCorrectAnswersList.Add(row); 

                }

                
            }

            //Update the user points for each correct answer
            totalPoints += tempCorrect;

            //Check if all of the answers were correct
            if (tempCorrect == correctAnswers)
            {
                //FREDDIE
                //THIS IS WHERE YOU CAN CHECK TO MAKE SURE BOT HACK IS SUCCESSFUL
                hackSuccessful = true;
            }

            else
            {
                //LEWIS
                //Set bots to a hunting state.
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PlayerStatus = GameController.Status.HUNTED;
            }

            //Reset asked list if all questions have already been asked

            if (gC.Ship1 && cloudAskedList.Count == cloudRangeMax)
                cloudAskedList.Clear();
            else if (gC.Ship2 && aiAskedList.Count == aiRangeMax)
                aiAskedList.Clear();
            else if (gC.Ship3 && dataAskedList.Count == dataRangeMax)
                dataAskedList.Clear();
            else if (gC.Ship4 && quantumAskedList.Count == quantumRangeMax)
                quantumAskedList.Clear();
            else if (gC.Ship5 && securityAskedList.Count == securityRangeMax)
                securityAskedList.Clear();

            /*if (askedList.Count == rangeOfQuestionsMax)
            {
                //clear asked list so duplicates can be asked again
                askedList.Clear();
            }*/

            waiting = true;

            //start timer here
            timer = waitTime;
        }
        submit = false;
        amountSelected = 0;

        if (waiting)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0 && waiting && loopNumber < questionsInARow)
        {
            find = true;
            waiting = false;
        }

        else if (timer < 0 && waiting && loopNumber == questionsInARow)
        {
            if (questionsInARow > 1)
            {
                //find = true;

                gC.inMinigame = false;
                gC.completedLevel = true;
            }
            else
            {
                for (int i = 0; i < answersList.Count; i++)
                {
                    Destroy(answersList[i]);
                }
                Destroy(questionText);
                Destroy(submitText);
                mC.completedQuiz = true;
                gC.inMinigame = false;
                gC.inQuiz = false;
                submit = false;
                //completedQuiz = true;
                loopNumber = 0;
                waiting = false;
            }

            //waiting = false;

        }

        //SingleSelectfunctionality
        if (correctAnswers <= 1)//if only 1 answer is available
        {//if selected answer has changed since last frame then unselect all but current
            if (singleSelected != tempSingleSelected)
            {
                for (int i = 0; i < answersList.Count; i++)
                {
                    if (answersList[i] != singleSelected)
                    {
                        answersList[i].GetComponent<answersScript>().selected = false;
                    }
                }
                singleSelected.GetComponent<answersScript>().selected = true;
            }
        }
        tempSingleSelected = singleSelected; //setting selected "last frame" to current
    }

    void OnGUI()
    {
        smallestFont = Mathf.Infinity;

        for (int i = 0; i < answersList.Count; i++)
        {
            if (answersList[i] != null)
            {
                if (answersList[i].GetComponentInChildren<TextMeshProUGUI>().fontSize < smallestFont)
                {
                    smallestFont = answersList[i].GetComponentInChildren<TextMeshProUGUI>().fontSize;
                }
            }
        }

        smallestFont = smallestFont * fontSizeMultiplier;

        for (int i = 0; i < answersList.Count; i++)
        {
            if (answersList[i] != null)
            {
                answersList[i].GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = false;
                answersList[i].GetComponentInChildren<TextMeshProUGUI>().fontSize = smallestFont;
            }
        }
    }

    
}
