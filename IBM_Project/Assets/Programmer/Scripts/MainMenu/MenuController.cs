using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private float canvasHeight, canvasWidth;

    [SerializeField]
    private GameObject buttonPrefab;

    private List<GameObject> buttonList = new List<GameObject>();

    [SerializeField]
    float buttonWidth, buttonHeight, buttonSpacing, startHeightFromStart, textboxScalar;
    public int amountOfButtons;

    const int   _start =        0,
                _settings =     1,
                _credits =      2,
                _quit =         3;



    // Start is called before the first frame update
    void Start()
    {
        buttonList.Clear();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        getCanvasSize();
        spawnButtons();
        setButtonPosition();
        setButtonSize();
    }

    void OnGUI()
    {
        setButtonPosition();
        setButtonSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getCanvasSize()
    {
        canvasRectTransform = canvas.GetComponent<RectTransform>();

        //This is to find the size of the canvas
        canvasWidth = canvasRectTransform.sizeDelta.x;
        canvasHeight = canvasRectTransform.sizeDelta.y;

        return;
    }

    void spawnButtons()
    {
        for (int i = 0; i < amountOfButtons; i++)
        {
            GameObject tempPrefab = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
            tempPrefab.transform.parent = canvas.transform;
            buttonList.Add(tempPrefab);
        }

        return;
    }

    void setButtonPosition()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            Vector3 pos = new Vector3(0, startHeightFromStart - (buttonSpacing * i), 0);
            buttonList[i].transform.position = canvas.transform.position + pos;
        }
    }

    void setButtonSize()
    {
        canvasWidth = canvasRectTransform.sizeDelta.x;
        canvasHeight = canvasRectTransform.sizeDelta.y;
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);

            buttonList[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
        }
    }
}
