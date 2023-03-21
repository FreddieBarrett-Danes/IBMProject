using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System;

public class MenuController : MonoBehaviour
{
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private float canvasHeight, canvasWidth;

    [SerializeField]
    private GameObject buttonPrefab, sliderPrefab, tickboxPrefab, dropdownPrefab;

    private List<GameObject> MainButtonList = new List<GameObject>();
    private List<GameObject> SettingsButtonList = new List<GameObject>();

    [SerializeField]
    private MenuState menuState;
    private MenuState lastFrameMenuState;

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
    string      settingButton1 =    "", 
                settingButton2 =    "";

    [SerializeField]
    private Color buttonColour;

    [SerializeField]
    private float tickboxScaler, sliderScaler, resScaler;

    [SerializeField]
    bool fullscreen, lastframeFullscreen;

    [SerializeField]
    Resolution thisFrameResolution, lastframeResolution;

    [SerializeField]
    private List<Vector2> resOptions;

    public float debug;

    public enum MenuState
    {
        Main,
        Settings,
        Credits
    }

    public enum Resolution
    {
        Default,
        FHD,
        LHD
    }

    void Start()
    {
        MainButtonList.Clear();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        getCanvasSize();
        spawnButtons();
        setButtonPosition();
        setButtonSize();

        menuState = MenuState.Main;
        thisFrameResolution = Resolution.Default;

        StateChanged();
        FullscreenState();
        ResolutionState();

    }

    void OnGUI()
    {
        setButtonPosition();
        setButtonSize();
        PositionMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(lastFrameMenuState != menuState)
        {
            StateChanged();
        }
        lastFrameMenuState = menuState;

        fullscreen = SettingsButtonList[1].transform.GetChild(1).GetComponent<Toggle>().isOn;

        if(lastframeFullscreen == fullscreen)
        {
            FullscreenState();
        }

        lastframeFullscreen = fullscreen;

        int dropdownInt = dropdownPrefab.GetComponent<Dropdown>().value;
        string temp = "Tempo";

        thisFrameResolution = (Resolution)Enum.Parse(typeof(Resolution),temp);

        if(lastframeResolution != thisFrameResolution)
        {
            ResolutionState();
        }

        lastframeResolution = thisFrameResolution;
    }

    void FullscreenState()
    {
        Screen.fullScreen = fullscreen;

        return;
    }

    void ResolutionState()
    {
        Screen.SetResolution((int)resOptions[thisFrameResolution.GetHashCode()].x, (int)resOptions[thisFrameResolution.GetHashCode()].y, true);

        return;
    }

    void StateChanged()
    {
        if(menuState == MenuState.Main) //hide settings & credits
        {
            ShowHideMainMenuComponents      (true);
            ShowHideSettingsMenuComponents  (false);
        }

        if(menuState == MenuState.Settings)
        {
            ShowHideMainMenuComponents      (false);
            ShowHideSettingsMenuComponents  (true);
            
        }

        if(menuState == MenuState.Credits) //hide settings & credits
        {
            ShowHideMainMenuComponents      (false);
            ShowHideSettingsMenuComponents  (false);

        }
    }

    void ShowHideMainMenuComponents(bool showHide)
    {
        for (int i = 0; i < MainButtonList.Count; i++)
        {
            MainButtonList[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHideSettingsMenuComponents(bool showHide)
    {
        for (int i = 0; i < SettingsButtonList.Count; i++)
        {
            SettingsButtonList[i].gameObject.SetActive(showHide);
        }
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
        //
        //Spawning Main Menu
        //
        for (int i = 0; i < amountOfButtons; i++)
        {
            GameObject tempPrefab = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
            tempPrefab.transform.parent = canvas.transform;
            MainButtonList.Add(tempPrefab);
            SetText(i);
            SetButtonActions(i);
            tempPrefab.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
            Destroy(tempPrefab.GetComponent<answersScript>());
            tempPrefab.GetComponent<Image>().color = buttonColour;
        }

        //
        //Spawn Settings Menu
        //

        //Spawn Audio Slider
        GameObject slider = Instantiate(sliderPrefab, canvas.transform.position, Quaternion.identity);
        slider.transform.parent = canvas.transform;
        SettingsButtonList.Add(slider);

        //Spawn Fullscreen Tickbox
        GameObject tickbox = Instantiate(tickboxPrefab, canvas.transform.position, Quaternion.identity);
        tickbox.transform.parent = canvas.transform;
        SettingsButtonList.Add(tickbox);

        //Spawn Resolution Dropdown
        GameObject dropdown = Instantiate(dropdownPrefab, canvas.transform.position, Quaternion.identity);
        dropdown.transform.parent = canvas.transform;
        SettingsButtonList.Add(dropdown);

        //Spawn Back Button
        GameObject settingsBack = Instantiate(buttonPrefab,canvas.transform.position, Quaternion.identity);
        settingsBack.transform.parent = canvas.transform;
        SettingsButtonList.Add(settingsBack);
        settingsBack.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        settingsBack.GetComponent<Button>().onClick.AddListener(BackButtonPressed);
        settingsBack.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(settingsBack.GetComponent<answersScript>());
        settingsBack.GetComponent<Image>().color = buttonColour;

        //
        //Spawn Credits
        //

        return;
    }

    void setButtonPosition()
    {
        if(menuState == MenuState.Main)
        {
            for (int i = 0; i < MainButtonList.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((buttonSpacing * canvasHeight) * i), 0);
                MainButtonList[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if(menuState == MenuState.Settings)
        {
            for (int i = 0; i < SettingsButtonList.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((buttonSpacing * canvasHeight) * i), 0);
                SettingsButtonList[i].transform.position = canvas.transform.position + pos;
            }
        }
    }

    void setButtonSize()
    {
        canvasWidth = canvasRectTransform.sizeDelta.x;
        canvasHeight = canvasRectTransform.sizeDelta.y;

        if(menuState == MenuState.Main)
        {
            for (int i = 0; i < MainButtonList.Count; i++)
            {
                MainButtonList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);

                MainButtonList[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
            }
        }

        else if(menuState == MenuState.Settings)
        {
            //SettingsButtonList[0].GetComponent<RectTransform>().sizeDelta = canvasWidth * debug/*new Vector2(buttonWidth * canvasWidth, /*buttonHeight * canvasHeight * debug)*/;
            //SettingsButtonList[0].GetComponent<RectTransform>().sizeDelta *= new Vector2(debug, debug);
            
            float sliderSize = Mathf.Min(canvasWidth, canvasHeight) * (sliderScaler / 100f);
            //SettingsButtonList[0].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, ((buttonWidth / 500) * 30) * canvasHeight);
            SettingsButtonList[0].transform.localScale = new Vector2(sliderSize, sliderSize);

            float checkboxSize = Mathf.Min(canvasWidth, canvasHeight) * (tickboxScaler / 100f);
            SettingsButtonList[1].transform.localScale = new Vector2(checkboxSize, checkboxSize);

            /*SettingsButtonList[2].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            SettingsButtonList[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(((buttonWidth * canvasWidth) * textboxScalar) * 0.5f, ((buttonHeight * canvasHeight) * textboxScalar) * 0.5f);*/ //This is scaling funny and idk why

            float resSize = Mathf.Min(canvasWidth, canvasHeight) * (resScaler / 100f);
            SettingsButtonList[2].transform.localScale = new Vector2(resSize, resSize);
            //SettingsButtonList[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>
            SettingsButtonList[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);

            SettingsButtonList[3].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            SettingsButtonList[3].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);

            /*float resSize = Mathf.Min(canvasWidth, canvasHeight) * (resScaler / 100f);
            SettingsButtonList[3].transform.localScale = new Vector2(resSize, resSize);*/

        }
    }

    void SetText(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                MainButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton1;
                break;
            case 1:
                MainButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton2;
                break;
            case 2:
                MainButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton3;
                break;
            case 3:
                MainButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton4;
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
                MainButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(PlayButtonPressed);
                break;
            case 1:
                MainButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(SettingsButtonPressed);
                break;
            case 2:
                MainButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(CreditsButtonPressed);
                break;
            case 3:
                MainButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(ExitButtonPressed);
                break;
            default:
                Debug.Log("Unknown button");
                break;
        }
    }

    void PositionMenu()
    {
        float averageHeight = 0;

        if(menuState == MenuState.Main)
        {
            for (int i = 0; i < MainButtonList.Count; i++)
            {
                averageHeight += MainButtonList[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= MainButtonList.Count;
            Debug.Log(averageHeight);

            for (int i = 0; i < MainButtonList.Count; i++)
            {
                MainButtonList[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if(menuState == MenuState.Settings)
        {
            for (int i = 0; i < SettingsButtonList.Count; i++)
            {
                averageHeight += SettingsButtonList[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= SettingsButtonList.Count;
            Debug.Log(averageHeight);

            for (int i = 0; i < SettingsButtonList.Count; i++)
            {
                SettingsButtonList[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }
    }

    void PlayButtonPressed()
    {
        //Add the game start logic here
        SceneManager.LoadScene(1);
        return;
    }
    
    void SettingsButtonPressed()
    {
        //Swap to settings menu
        menuState = MenuState.Settings;
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

    void BackButtonPressed()
    {
        //Go back to main menu
        menuState = MenuState.Main;

        return;
    }
}
