//using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ReadCSV : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextAsset CSVFile;
    private GameObject canvas;
    private RectTransform canvasRectTransform;

    public GameObject panelTest;
    public List<GameObject> list;
    //public int[] numbers;
    public Vector2 panelSize;

    private float startX;
    private float startY;

    [Range(1, 5)]
    public int row;
    [Range(1, 5)]
    public int column;

    [SerializeField]
    private bool find; //Use this to generate the row,column that you've selected using Row and Column

    //public System.Random rng = new System.Random();

    List<int> Shuffle(int length)
    {
        List<int> orderedList = new List<int>(4);
        
        for(int i = 1; i < length + 1; i++) //create list of numbers 1-4
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

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas"); // may be ambiguous if theres several
        canvasRectTransform = canvas.GetComponent<RectTransform>();
    }
    void Update()
    {
        if (find) //File Reading / generate
        {
            find = false;
            var dataset = CSVFile;

            var splitDataset = dataset.text.Split(new char[] { '\n' });

            if (row <= 1) row = 1;
            if (column <= 1) column = 1;

            for (int i = 0; i < row; i++)
            {
                var data = splitDataset[i].Split(',');
                for (int j = 0; j < column; j++)
                {
                    if (row > splitDataset.Length) row = splitDataset.Length;
                    if (column > data.Length) column = data.Length;

                    text.text = data[j];
                }
            }

            //Generate question answers

            if (list.Count != 0) //Reset list
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Destroy(list[i]);
                }
                list.Clear();
            }

            startX = 0.25f; //Set start values for coords.
            startY = 0.25f;

            panelSize = new Vector2((canvasRectTransform.sizeDelta.x * 0.9f) / 2, canvasRectTransform.sizeDelta.y * 0.25f); //Find panel size

            for (int i = 0; i < 2; i++)
            {
                GameObject tempPanelY = Instantiate(panelTest);
                tempPanelY.transform.SetParent(canvas.transform);
                tempPanelY.GetComponent<RectTransform>().sizeDelta = panelSize;

                tempPanelY.GetComponent<RectTransform>().position = new Vector2(canvasRectTransform.sizeDelta.x * startX, canvasRectTransform.sizeDelta.y * startY);

                list.Add(tempPanelY);

                startX += 0.5f;

                for (int j = 0; j < 1; j++)
                {
                    GameObject tempPanelZ = Instantiate(panelTest);

                    tempPanelZ.transform.SetParent(canvas.transform);
                    tempPanelZ.GetComponent<RectTransform>().sizeDelta = panelSize;

                    tempPanelZ.GetComponent<RectTransform>().position = new Vector2((canvasRectTransform.sizeDelta.x * startX), canvasRectTransform.sizeDelta.y * startY);

                    list.Add(tempPanelZ);

                    startX -= 0.5f;
                    startY += 0.33f;
                }
            }

            panelTest.GetComponent<RectTransform>().sizeDelta = panelSize;

            panelTest.GetComponent<RectTransform>().position = new Vector2((canvasRectTransform.sizeDelta.x * startX), canvasRectTransform.sizeDelta.y * startY);
            
            List<int> tempList = new List<int>(FisherYatesShuffle(Shuffle(4)));

            Debug.Log(tempList[0]);
            Debug.Log(tempList[1]);
            Debug.Log(tempList[2]);
            Debug.Log(tempList[3]);
        }
    }
}