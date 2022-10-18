using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class ReadCSV : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextAsset CSVFile;
    private GameObject canvas;

    public GameObject panelTest;
    public Vector2 panelSize;

    public float tempX;
    public float tempY;

    [Range(1, 5)]
    public int row;
    [Range(1, 5)]
    public int column;

    [SerializeField]
    private bool find; //Use this to generate the row,column that you've selected using Row and Column

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas"); // may be ambiguous if theres several
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

                    Debug.Log(splitDataset.Length);
                    text.text = data[j];
                }
            }
            
            panelSize = new Vector2((canvas.GetComponent<RectTransform>().sizeDelta.x * 0.9f) / 2, canvas.GetComponent<RectTransform>().sizeDelta.y * 0.25f);

            for (int i = 0; i < 2; i++)
            {
                GameObject tempPanel = Instantiate(panelTest);
                tempPanel.transform.SetParent(canvas.transform);
                //tempPanel.transform.parent = canvas.transform;
                Debug.Log(i);

                for (int j = 0; j < 2; j++)
                {

                }
            }
            panelTest.GetComponent<RectTransform>().sizeDelta = panelSize;

            panelTest.GetComponent<RectTransform>().position = new Vector2((canvas.GetComponent<RectTransform>().sizeDelta.x * tempX), canvas.GetComponent<RectTransform>().sizeDelta.y * tempY);
        }

        /*panelSize = new Vector2((canvas.GetComponent<RectTransform>().sizeDelta.x * 0.9f) / 2, canvas.GetComponent<RectTransform>().sizeDelta.y * 0.25f);

        for (int i = 0; i < 2; i++)
        {
            GameObject tempPanel = Instantiate(panelTest);
            tempPanel.transform.parent = canvas.transform;

            for (int j = 0; j < 2; j++)
            {

            }
        }
        panelTest.GetComponent<RectTransform>().sizeDelta = panelSize;

        panelTest.GetComponent<RectTransform>().position = new Vector2((canvas.GetComponent<RectTransform>().sizeDelta.x * tempX), canvas.GetComponent<RectTransform>().sizeDelta.y * tempY);*/

        //Generate panel size


    }
}
