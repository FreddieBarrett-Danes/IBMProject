using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class answersScript : MonoBehaviour
{
    private Button btn;
    public bool selected;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(buttonClicked);
    }

    void Update()
    {
        if (selected)
        {
            this.GetComponent<Image>().color = Color.red;
        }
        if (!selected)
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }

    private void buttonClicked()
    {
        selected = !selected;
        this.GetComponent<Image>().color = Color.red;
    }
}
