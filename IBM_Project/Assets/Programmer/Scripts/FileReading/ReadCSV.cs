using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ReadCSV : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextAsset CSVFile;
    private GameObject canvas;
    private RectTransform canvasRectTransform;

    public GameObject panelTest;
    public List<GameObject> list;
    public Vector2 panelSize;

    private float startX;
    private float startY;

    [Range(1, 5)]
    public int row;
    [Range(1, 5)]
    public int column;

    [SerializeField]
    private bool find; //Use this to generate the row,column that you've selected using Row and Column

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

                    //Debug.Log(splitDataset.Length);
                    text.text = data[j];
                }
            }

            //Generate question answers

            if(list.Count != 0) //Reset list
            {
                for(int i = 0; i < list.Count; i++)
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

            int random = UnityEngine.Random.Range(0, list.Count);
            list[random].GetComponentInChildren<TextMeshProUGUI>().text = ("arrrrggghhhh");

        }
    }
}
