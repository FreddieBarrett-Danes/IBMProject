using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReadTSV : MonoBehaviour
{
    public GameObject questionText;
    public List<GameObject> answersList;
    public TextAsset CSVFile;
    private GameObject canvas;
    private RectTransform canvasRectTransform;
    //[SerializeField]
    public int rightAnswers;
    [SerializeField]
    private int correctAnswers;

    public float timer;
    public float waitTime;
    public bool waiting;
    
    public GameObject panel;
    public Vector2 panelSize;

    private float startX;
    private float startY;

    [Range(1, 5)]
    public int row;

    //[SerializeField]
    public bool find; //Use this to generate the row,column that you've selected using Row and Column
    [SerializeField]
    private bool submit;
    [SerializeField]
    private bool reloadSceneAtEnd;

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

    string Find(int findRow, int findColmn)
    {
        string rv = null;

        find = false;
        var dataset = CSVFile;

        var splitDataset = dataset.text.Split(new char[] { '\n' });

        if (findRow < 1)
        {
            findRow = 1;
            Debug.LogWarning("Desired Row given in the Find() function located on: " + this.gameObject.name + " was out of bounds. It was automatically brought back into range. - ask Istvan");
        }

        if (findColmn < 1)
        {
            findColmn = 1;
            Debug.LogWarning("Desired Column given in the Find() function located on: " + this.gameObject.name + " was out of bounds. It was automatically brought back into range. - ask Istvan");
        }

        for (int i = 0; i < findRow; i++)
        {
            char tabSpace = '\u0009'; //takes the TAB ascii code as the splitter character. this value used to be a , when using CSV but now we are using TSV.
            var data = splitDataset[i].Split(tabSpace.ToString()); //
            //var data = splitDataset[i].Split(',');
            for (int j = 0; j < findColmn; j++)
            {
                if (findRow > splitDataset.Length) findRow = splitDataset.Length;
                if (findColmn > data.Length) findColmn = data.Length;

                //questionText.text = data[j];

                rv = data[j];
            }
        }
        //Debug.Log(rv);
        return rv;
    }

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas"); // may be ambiguous if theres several
        canvasRectTransform = canvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (find) //File Reading / generate
        {
            if (answersList.Count != 0) //Reset list
            {
                for (int i = 0; i < answersList.Count; i++)
                {
                    Destroy(answersList[i]);
                }
                Destroy(questionText);
                answersList.Clear();
            }

            startX = 0.25f; //Set start values for coords.
            startY = 0.25f;

            panelSize = new Vector2((canvasRectTransform.sizeDelta.x * 0.9f) / 2, canvasRectTransform.sizeDelta.y * 0.25f); //Find panel size

            rightAnswers = 0; //reset the counter for how many answers player got right
            
            //Instantiating question box

            questionText = Instantiate(panel);
            questionText.transform.SetParent(canvas.transform);
            questionText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRectTransform.sizeDelta.x * 0.95f, panelSize.y);
            questionText.GetComponent<RectTransform>().position = new Vector2(canvasRectTransform.sizeDelta.x / 2, canvasRectTransform.sizeDelta.y * 0.83f);

            Destroy(questionText.GetComponent<Button>());
            Destroy(questionText.GetComponent<answersScript>());

            if(questionText.GetComponentInChildren<TextMeshProUGUI>().isTextOverflowing == true)
            {
                Debug.Log("Text overflow");
            }

            questionText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRectTransform.sizeDelta.x * 0.95f, panelSize.y); //set siz of textbox

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

            panel.GetComponent<RectTransform>().sizeDelta = panelSize;

            panel.GetComponent<RectTransform>().position = new Vector2((canvasRectTransform.sizeDelta.x * startX), canvasRectTransform.sizeDelta.y * startY);

            int.TryParse(Find(row, 6), out correctAnswers);

            if(correctAnswers < 1)
            {
                correctAnswers = 1;
                Debug.LogWarning(this.name + " was unable to determine how many correct answers there are. It was automatically set to 1. - ask Istvan");
            }
            
            /*for(int j = 0; j < correctAnswers; j++) //This determines which answers are the correct ones
            {
                Debug.Log(j);
            }*/

            

            List<int> orderList = new List<int>(FisherYatesShuffle(Shuffle(4)));

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
        }

        if (reloadSceneAtEnd) // okay maybe dont need this now then. cool.
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            reloadSceneAtEnd = false;
        }

        if (submit)
        {
            for(int i = 0; i < answersList.Count; i++) //check to make sure correct answers were ticked and set colours
            {
                Destroy(answersList[i].GetComponent<Button>());

                if (answersList[i].GetComponent<answersScript>().isCorrect && answersList[i].GetComponent<answersScript>().selected) // selected is correct
                {
                    rightAnswers++;
                    answersList[i].GetComponent<answersScript>().panelColour = Color.green; // set colour to green
                    //answersList[i].GetComponent<Image>().color);
                }
                else if (!answersList[i].GetComponent<answersScript>().isCorrect && answersList[i].GetComponent<answersScript>().selected) // selected is incorrect
                {
                    answersList[i].GetComponent<answersScript>().panelColour = Color.red; // set colour to red
                }
                else if (answersList[i].GetComponent<answersScript>().isCorrect && !answersList[i].GetComponent<answersScript>().selected) // not seleced correct answer
                {
                    answersList[i].GetComponent<answersScript>().panelColour = new Color(1,0.5f,0,1); // set colour to orange
                }
            }
            waiting = true;

            //start timer here
            timer = waitTime;
            submit = false;
            
        }
        if(waiting)
        {
            timer -= Time.deltaTime;
        }

        if(timer < 0 && waiting)
        {
            find = true;
            waiting = false;
            
        }
    }
}