using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadCSV : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextAsset CSVFiles;

    public int row;
    public int col;

    public int Length;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dataset = CSVFiles;

        var splitDataset = dataset.text.Split(new char[] { '\n' });

        for (int i = 0; i < row; i++)
        {
            /*var data = splitDataset[i].Split(',');
            for (int j = 0; j < col; j++)
            {
                if(row > splitDataset.Length) row = splitDataset.Length;
                if(row <= 0) row = 0;
                if(col > data.Length - 1) col = data.Length - 1;
                if(col <= 0) col = 0;
                Debug.Log(splitDataset.Length);
                text.text = data[j];
            }*/
        }
    }

    /*void ReadCSV()
    {
        Length = CSVFiles.text.Length;
    }*/
}
