using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadCSV : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextAsset CSVFile;

    [Range(1, 5)]
    public int row;
    [Range(1, 5)]
    public int column;

    [SerializeField]
    private bool find; //Use this to generate the row,column that you've selected using Row and Column

    void Update()
    {
        if (find)
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
        }
        
    }
}
