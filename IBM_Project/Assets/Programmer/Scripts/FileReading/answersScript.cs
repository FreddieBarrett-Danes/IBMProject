using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class answersScript : MonoBehaviour
{
    private Button btn;
    public bool selected;
    private bool wasSelected;
    public Color panelColour;
    public bool isCorrect; 

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(buttonClicked);
        //panelColour = this.GetComponent<Color>();
        panelColour = Color.white;
    }

    void Update()
    {
        //set colour that matches selected status
        if (selected != wasSelected && selected)
        {
            wasSelected = selected;
            //this.GetComponent<Image>().color = Color.yellow;
            panelColour = Color.yellow;
        }
        else if (selected != wasSelected && !selected)
        {
            wasSelected = selected;
            //this.GetComponent<Image>().color = Color.white;
            panelColour = Color.white;
        }
        
        this.GetComponent<Image>().color = panelColour;

        /*if (!selected)
        {
            this.GetComponent<Image>().color = Color.white;
        }*/
    }

    private void buttonClicked()
    {
        selected = !selected;

        //seingle select functionality
        if (selected)
        {
            GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>().singleSelected = this.gameObject;
        }
    }
}
