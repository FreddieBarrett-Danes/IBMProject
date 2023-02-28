using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

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

    [SerializeField]
    string      menuButton1 = "Start",
                menuButton2 = "Settings",
                menuButton3 = "Credits",
                menuButton4 = "Exit";

    [SerializeField]
    private Color buttonColour;

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
            SetText(i);
            SetButtonActions(i);
            tempPrefab.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
            Destroy(tempPrefab.GetComponent<answersScript>());
            tempPrefab.GetComponent<Image>().color = buttonColour;
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

    void SetText(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                buttonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton1;
                break;
            case 1:
                buttonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton2;
                break;
            case 2:
                buttonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton3;
                break;
            case 3:
                buttonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton4;
                break;
            default:
                Debug.Log("Unknown button");
                break;
        }
    }

    void SetButtonActions(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                buttonList[buttonNumber].GetComponent<Button>().onClick.AddListener(PlayButtonPressed);
                break;
            case 1:
                buttonList[buttonNumber].GetComponent<Button>().onClick.AddListener(SettingsButtonPressed);
                break;
            case 2:
                buttonList[buttonNumber].GetComponent<Button>().onClick.AddListener(CreditsButtonPressed);
                break;
            case 3:
                buttonList[buttonNumber].GetComponent<Button>().onClick.AddListener(ExitButtonPressed);
                break;
            default:
                Debug.Log("Unknown button");
                break;
        }
    }

    void PlayButtonPressed()
    {
        //Add the game start logic here

        return;
    }
    
    void SettingsButtonPressed()
    {
        //Swap to settings menu

        return;
    }

    void CreditsButtonPressed()
    {
        //Swap to credits menu

        return;
    }

    void ExitButtonPressed()
    {
        //Force Quit Game..

        Application.Quit();

        return;
    }
}
