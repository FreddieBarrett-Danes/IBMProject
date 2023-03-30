using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Runtime.Serialization.Formatters;

public class MenuController : MonoBehaviour
{
    
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private RectTransform canvasRectTransform;
    [SerializeField]
    private float canvasHeight, canvasWidth;

    [SerializeField]
    private GameObject buttonPrefab, sliderPrefab, tickboxPrefab, dropdownPrefab, skillsText, htp0Text, htp1Text, htp2Text, htp3Text, htp1Image, htp2Image, htp3Image, htp4Image, cornerButton;

    [SerializeField]
    private List<GameObject> MainButtonList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> PauseButtonList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> SettingsButtonList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> SkillsButtonList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> CreditsButtonList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> PlayButtonList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> HTP0List = new List<GameObject>();
    [SerializeField]
    private List<GameObject> HTP1List = new List<GameObject>();
    [SerializeField]
    private List<GameObject> HTP2List = new List<GameObject>();
    [SerializeField]
    private List<GameObject> HTP3List = new List<GameObject>();

    [SerializeField]
    private List<GameObject> HTP4List = new List<GameObject>();
    [SerializeField]
    private List<GameObject> cornerList = new List<GameObject>();

    public MenuState menuState;
    private MenuState lastFrameMenuState;

    private MenuController menuInstance;

    [SerializeField]
    float buttonWidth, buttonHeight, buttonSpacing, startHeightFromStart, creditsHeightFrom, creditsSpacing, playHeightFrom, textboxScalar,
        htpTitleScalar, shipsButtonHeight, shipsButtonWidth, shipsSpacing, htpSpacing, htp0Scalar, htp1Scalar, htp2Scalar, htp3Scalar, htp4Scalar, cornerScalar;
    //public int amountOfButtons;

    const int _start = 0,
                _settings = 1,
                _credits = 2,
                _quit = 3;

    //[SerializeField]
    string menuButton1 = "Play",
                menuButton2 = "Settings",
                menuButton3 = "Credits",
                menuButton4 = "Exit",
                menuButton5 = "Exit";

    [SerializeField]
    private Color buttonColour;
    [SerializeField]
    private Color textColour;

    [SerializeField]
    private float tickboxScaler, sliderScaler, resScaler, creditsScaler;

    [SerializeField]
    bool fullscreen, lastframeFullscreen;

    [SerializeField]
    Resolution thisFrameResolution, lastframeResolution;

    [SerializeField]
    private List<Vector2> resOptions;

    [SerializeField]
    private string hyperlink = "https://google.com/";

    public string SkillsText;

    public bool inGame;
    public bool escCanBePressed;
    public bool Quiz;

    [SerializeField]
    private GameObject lTick;

    AudioSource music;

    AudioLowPassFilter lpf;

    
    //public float debug;

    public enum MenuState
    {
        Main,
        Settings,
        SkillsBuild,
        Credits,
        Levels,
        Running,
        Paused,
        HTP0,
        HTP1,
        HTP2,
        HTP3,
        HTP4
    }

    public enum Resolution
    {
        Default,
        FHD,
        LHD
    }
    private void Awake()
    {

    }
    void Start()
    {
        /*        GameObject[] menuArray = GameObject.FindGameObjectsWithTag("MenuController");
                if (menuArray.Length > 1)
                {
                    for(int i = 1; i < menuArray.Length; i++)
                    {
                        Destroy(menuArray[i]);
                    }
                }*/
        /*        GameObject existingObject = GameObject.Find(gameObject.name);
                if(existingObject != null)
                {
                    Destroy(gameObject);
                }
                else
                {

                }*/
        
        DontDestroyOnLoad(this.gameObject);
        /*if (menuInstance == null)
        {
            menuInstance = this;
            Debug.Log(menuInstance);
            Debug.Log("ahhhhhhhhh");
        }
        else
        {
            Debug.Log("Hello");
            Destroy(gameObject);
        }*/
        
        GameObject[] menuControllers = GameObject.FindGameObjectsWithTag("MenuController");
        if (menuControllers.Length > 1)
        {
            Destroy(menuControllers[0]);
        }

        //MainButtonList.Clear();
        music = GetComponent<AudioSource>();
        lpf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioLowPassFilter>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        buttonPrefab = Resources.Load<GameObject>("Menu/PanelPrefab");
        sliderPrefab = Resources.Load<GameObject>("Menu/SliderPrefab");
        tickboxPrefab = Resources.Load<GameObject>("Menu/TickboxPrefab");
        dropdownPrefab = Resources.Load<GameObject>("Menu/DropdownPrefab");
        skillsText = Resources.Load<GameObject>("Menu/SkillsText");

        getCanvasSize();
        spawnButtons();
        setButtonPosition();
        setButtonSize();

        menuState = MenuState.Main;
        thisFrameResolution = Resolution.Default;

        StateChanged();
        FullscreenState();
        ResolutionState();

        /*for (int i = 0; i < HTP0List.Count; i++)
        {
            ShowHideHTP0Components(true);
        }*/

        if (GameObject.FindGameObjectWithTag("GameController")/* != null*/)
        {
            inGame = true;
            menuState = MenuState.Running;
        }
        else
        {
            inGame = false;
            menuState = MenuState.Main;
        }
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
        /*if(GameObject.FindGameObjectWithTag("GameController") != null)
        {
            inGame = true;
        }
        else
        {
            inGame = false;
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escCanBePressed)
            {
                if (menuState == MenuState.Running)
                {
                    //pause
                    menuState = MenuState.Paused;
                    PauseGame();
                }
                else if (menuState == MenuState.Paused)
                {
                    //unpause
                    menuState = MenuState.Running;
                    UnpauseGame();
                }
                else if (menuState == MenuState.Settings || menuState == MenuState.Credits || menuState == MenuState.SkillsBuild)
                {
                    BackButtonPressed();
                }
                else if (menuState == MenuState.HTP0 || menuState == MenuState.HTP1 || menuState == MenuState.HTP2 || menuState == MenuState.HTP3 || menuState == MenuState.HTP4)
                {
                    SkillsButtonPressed();
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 26 || SceneManager.GetActiveScene().buildIndex == 27 || SceneManager.GetActiveScene().buildIndex == 28 || SceneManager.GetActiveScene().buildIndex == 29 || SceneManager.GetActiveScene().buildIndex == 30 || SceneManager.GetActiveScene().buildIndex == 31 || SceneManager.GetActiveScene().buildIndex == 32 || SceneManager.GetActiveScene().buildIndex == 33 || SceneManager.GetActiveScene().buildIndex == 34 || SceneManager.GetActiveScene().buildIndex == 35 || Quiz)
        {
            menuState = MenuState.Running;
            music.enabled = false;
            escCanBePressed = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0 || !Quiz)
        {
            //menuState = MenuState.Main;
            escCanBePressed = true;
        }


        if (lastFrameMenuState != menuState)
        {
            StateChanged();
        }
        lastFrameMenuState = menuState;

        fullscreen = SettingsButtonList[1].transform.GetChild(1).GetComponent<Toggle>().isOn;

        if (lastframeFullscreen == fullscreen)
        {
            FullscreenState();
        }

        lastframeFullscreen = fullscreen;

        //Debug.Log(SettingsButtonList[2].GetComponent<TMP_Dropdown>());

        int dropdownInt = SettingsButtonList[2].GetComponent<TMP_Dropdown>().value;
        //string temp = "Tempo";

        thisFrameResolution = (Resolution)dropdownInt;
        //Debug.Log(thisFrameResolution);

        if (lastframeResolution != thisFrameResolution)
        {
            ResolutionState();
        }

        lastframeResolution = thisFrameResolution;

        if (inGame == true)
            music.volume = 0f;
        else
            music.volume = 0.6f;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("newLevel");
        //int resolution = (int)thisFrameResolution;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {

            MainButtonList.Clear();
            PauseButtonList.Clear();
            SettingsButtonList.Clear();
            SkillsButtonList.Clear();
            CreditsButtonList.Clear();
            PlayButtonList.Clear();
            HTP0List.Clear();
            HTP1List.Clear();
            HTP2List.Clear();
            HTP3List.Clear();
            HTP4List.Clear();
            cornerList.Clear();

            music = GetComponent<AudioSource>();
            lpf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioLowPassFilter>();
            canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
            canvasRectTransform = canvas.GetComponent<RectTransform>();
            //Debug.Log("Canvas Name " + canvas.gameObject.name);
            buttonPrefab = Resources.Load<GameObject>("Menu/PanelPrefab");
            sliderPrefab = Resources.Load<GameObject>("Menu/SliderPrefab");
            tickboxPrefab = Resources.Load<GameObject>("Menu/TickboxPrefab");
            dropdownPrefab = Resources.Load<GameObject>("Menu/DropdownPrefab");
            skillsText = Resources.Load<GameObject>("Menu/SkillsText");
            getCanvasSize();
            spawnButtons();
            //SettingsButtonList[2].GetComponent<Dropdown>().value = resolution;
            setButtonPosition();
            setButtonSize();

            //menuState = MenuState.Main;
            //thisFrameResolution = Resolution.Default;

            StateChanged();
            FullscreenState();
            ResolutionState();

            if (GameObject.FindGameObjectWithTag("GameController")/* != null*/)
            {
                inGame = true;
                menuState = MenuState.Running;
            }
            else
            {
                inGame = false;
                menuState = MenuState.Main;
            }
        }
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
        //GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
        //Destroy(audio, 3f);

        if (menuState == MenuState.Main) //hide settings & credits
        {
            ShowHideMainMenuComponents(true);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(false);
        }

        else if (menuState == MenuState.Paused)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(true);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(false);
        }

        else if (menuState == MenuState.Settings)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(true);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(false);
        }

        else if (menuState == MenuState.Credits) //hide settings & credits
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(true);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(false);
        }

        else if (menuState == MenuState.SkillsBuild)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(true);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(false);
        }

        else if (menuState == MenuState.Levels)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(true);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(false);
            GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
            Destroy(audio, 3f);
        }

        else if (menuState == MenuState.Running)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(false);
        }

        else if (menuState == MenuState.HTP0)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(true);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(true);
        }

        else if (menuState == MenuState.HTP1)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(true);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(true);
        }

        else if (menuState == MenuState.HTP2)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(true);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(true);
        }

        else if (menuState == MenuState.HTP3)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(true);
            ShowHideHTP4Components(false);
            ShowHideCornerComponents(true);
        }

        else if (menuState == MenuState.HTP4)
        {
            ShowHideMainMenuComponents(false);
            ShowHidePauseMenuComponents(false);
            ShowHideSettingsMenuComponents(false);
            ShowHideCreditsMenuComponents(false);
            ShowHideSkillsMenuComponents(false);
            ShowHidePlayMenuComponents(false);
            ShowHideHTP0Components(false);
            ShowHideHTP1Components(false);
            ShowHideHTP2Components(false);
            ShowHideHTP3Components(false);
            ShowHideHTP4Components(true);
            ShowHideCornerComponents(true);
        }
    }

    void ShowHideMainMenuComponents(bool showHide)
    {
        for (int i = 0; i < MainButtonList.Count; i++)
        {
            MainButtonList[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHidePauseMenuComponents(bool showHide)
    {
        for (int i = 0; i < PauseButtonList.Count; i++)
        {
            PauseButtonList[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHideSettingsMenuComponents(bool showHide)
    {
        for (int i = 0; i < SettingsButtonList.Count; i++)
        {
            SettingsButtonList[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHideCreditsMenuComponents(bool showHide)
    {
        for (int i = 0; i < CreditsButtonList.Count; i++)
        {
            CreditsButtonList[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHideSkillsMenuComponents(bool showHide)
    {
        for (int i = 0; i < SkillsButtonList.Count; i++)
        {
            SkillsButtonList[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHidePlayMenuComponents(bool showHide)
    {
        for (int i = 0; i < PlayButtonList.Count; i++)
        {
            PlayButtonList[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHideHTP0Components(bool showHide)
    {
        for (int i = 0; i < HTP0List.Count; i++)
        {
            HTP0List[i].gameObject.SetActive(showHide);
            //Debug.Log(HTP0List[i].gameObject.name);
        }
    }

    void ShowHideHTP1Components(bool showHide)
    {
        for (int i = 0; i < HTP1List.Count; i++)
        {
            HTP1List[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHideHTP2Components(bool showHide)
    {
        for (int i = 0; i < HTP2List.Count; i++)
        {
            HTP2List[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHideHTP3Components(bool showHide)
    {
        for (int i = 0; i < HTP3List.Count; i++)
        {
            HTP3List[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHideHTP4Components(bool showHide)
    {
        for (int i = 0; i < HTP4List.Count; i++)
        {
            HTP4List[i].gameObject.SetActive(showHide);
        }
    }

    void ShowHideCornerComponents(bool showHide)
    {
        for (int i = 0; i < cornerList.Count; i++)
        {
            cornerList[i].gameObject.SetActive(showHide);
        }
    }

    void OpenHyperlink()
    {
        Application.OpenURL(hyperlink);
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
        for (int i = 0; i < 4; i++)
        {
            GameObject tempPrefab = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
            //tempPrefab.transform.SetParent(canvas.transform);
            tempPrefab.transform.SetParent(canvas.transform);
            MainButtonList.Add(tempPrefab);
            SetText(i);
            SetButtonActions(i);
            tempPrefab.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
            Destroy(tempPrefab.GetComponent<answersScript>());
            tempPrefab.GetComponent<Image>().color = buttonColour;
        }

        //
        //Spawn Pause menu varient
        //

        for (int i = 0; i < 4; i++)
        {
            GameObject tempPrefab = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
            //tempPrefab.transform.parent = canvas.transform;
            tempPrefab.transform.SetParent(canvas.transform);
            PauseButtonList.Add(tempPrefab);
            SetPauseText(i);
            SetPauseButtonActions(i);
            tempPrefab.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
            Destroy(tempPrefab.GetComponent<answersScript>());
            tempPrefab.GetComponent<Image>().color = buttonColour;
        }

        //
        //Spawn Settings Menu
        //

        //Spawn Audio Slider
        GameObject slider = Instantiate(sliderPrefab, canvas.transform.position, Quaternion.identity);
        slider.transform.SetParent(canvas.transform);
        SettingsButtonList.Add(slider);
        slider.GetComponent<Slider>().value = 5;

        //Spawn Fullscreen Tickbox
        GameObject tickbox = Instantiate(tickboxPrefab, canvas.transform.position, Quaternion.identity);
        tickbox.transform.SetParent(canvas.transform);
        SettingsButtonList.Add(tickbox);

        //Spawn Resolution Dropdown
        GameObject dropdown = Instantiate(dropdownPrefab, canvas.transform.position, Quaternion.identity);
        dropdown.transform.SetParent(canvas.transform);
        SettingsButtonList.Add(dropdown);

        //Spawn Back Button
        GameObject settingsBack = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        settingsBack.transform.SetParent(canvas.transform);
        SettingsButtonList.Add(settingsBack);
        settingsBack.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        settingsBack.GetComponent<Button>().onClick.AddListener(BackButtonPressed);
        settingsBack.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(settingsBack.GetComponent<answersScript>());
        settingsBack.GetComponent<Image>().color = buttonColour;

        //
        //Spawn Credits
        //

        GameObject creditsText = Instantiate(skillsText, canvas.transform.position, Quaternion.identity);
        creditsText.transform.SetParent(canvas.transform);
        CreditsButtonList.Add(creditsText);

        //back button
        GameObject creditsBack = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        creditsBack.transform.SetParent(canvas.transform);
        CreditsButtonList.Add(creditsBack);
        creditsBack.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        creditsBack.GetComponent<Button>().onClick.AddListener(BackButtonPressed);
        creditsBack.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(creditsBack.GetComponent<answersScript>());
        creditsBack.GetComponent<Image>().color = buttonColour;

        //
        //Spawn Skills build
        //

        //Start button
        GameObject skillsPlay = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        skillsPlay.transform.SetParent(canvas.transform);
        SkillsButtonList.Add(skillsPlay);
        skillsPlay.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        skillsPlay.GetComponent<Button>().onClick.AddListener(PlayButtonPressed);
        skillsPlay.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(skillsPlay.GetComponent<answersScript>());
        skillsPlay.GetComponent<Image>().color = buttonColour;

        //link to ibm
        GameObject skillsLink = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        skillsLink.transform.SetParent(canvas.transform);
        SkillsButtonList.Add(skillsLink);
        skillsLink.GetComponentInChildren<TextMeshProUGUI>().text = "IBM Skills Build";
        skillsLink.GetComponent<Button>().onClick.AddListener(OpenHyperlink);
        skillsLink.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(skillsLink.GetComponent<answersScript>());
        skillsLink.GetComponent<Image>().color = buttonColour;

        //text for ibm
        GameObject ibmText = Instantiate(skillsText, canvas.transform.position, Quaternion.identity);
        ibmText.transform.SetParent(canvas.transform);
        SkillsButtonList.Add(ibmText);

        //menu for controls
        GameObject controls = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        controls.transform.SetParent(canvas.transform);
        SkillsButtonList.Add(controls);
        controls.GetComponentInChildren<TextMeshProUGUI>().text = "Controls";
        controls.GetComponent<Button>().onClick.AddListener(ControlsPressed);
        controls.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(controls.GetComponent<answersScript>());
        controls.GetComponent<Image>().color = buttonColour;

        //back button
        GameObject skillsBack = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        skillsBack.transform.SetParent(canvas.transform);
        SkillsButtonList.Add(skillsBack);
        skillsBack.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        skillsBack.GetComponent<Button>().onClick.AddListener(BackButtonPressed);
        skillsBack.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(skillsBack.GetComponent<answersScript>());
        skillsBack.GetComponent<Image>().color = buttonColour;


        //
        // Spawn Play menu with 5 ships
        //

        GameObject ship1 = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        ship1.transform.SetParent(canvas.transform);
        PlayButtonList.Add(ship1);
        ship1.GetComponentInChildren<TextMeshProUGUI>().text = "Cloud";
        ship1.GetComponent<Button>().onClick.AddListener(Ship1Start);
        ship1.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(ship1.GetComponent<answersScript>());
        ship1.GetComponent<Image>().color = buttonColour;

        GameObject ship2 = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        ship2.transform.SetParent(canvas.transform);
        PlayButtonList.Add(ship2);
        ship2.GetComponentInChildren<TextMeshProUGUI>().text = "AI";
        ship2.GetComponent<Button>().onClick.AddListener(Ship2Start);
        ship2.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(ship2.GetComponent<answersScript>());
        ship2.GetComponent<Image>().color = buttonColour;

        GameObject ship3 = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        ship3.transform.SetParent(canvas.transform);
        PlayButtonList.Add(ship3);
        ship3.GetComponentInChildren<TextMeshProUGUI>().text = "Data";
        ship3.GetComponent<Button>().onClick.AddListener(Ship3Start);
        ship3.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(ship3.GetComponent<answersScript>());
        ship3.GetComponent<Image>().color = buttonColour;

        GameObject ship4 = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        ship4.transform.SetParent(canvas.transform);
        PlayButtonList.Add(ship4);
        ship4.GetComponentInChildren<TextMeshProUGUI>().text = "Quantum";
        ship4.GetComponent<Button>().onClick.AddListener(Ship4Start);
        ship4.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(ship4.GetComponent<answersScript>());
        ship4.GetComponent<Image>().color = buttonColour;

        GameObject ship5 = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        ship5.transform.SetParent(canvas.transform);
        PlayButtonList.Add(ship5);
        ship5.GetComponentInChildren<TextMeshProUGUI>().text = "Security";
        ship5.GetComponent<Button>().onClick.AddListener(Ship5Start);
        ship5.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(ship5.GetComponent<answersScript>());
        ship5.GetComponent<Image>().color = buttonColour;

        //
        //How to play 0
        //

        GameObject htp0Title = Instantiate(skillsText, canvas.transform.position, Quaternion.identity);
        htp0Title.transform.SetParent(canvas.transform);
        HTP0List.Add(htp0Title);
        htp0Title.GetComponent<TextMeshProUGUI>().text = "How To Play...";

        GameObject htp0text = Instantiate(htp0Text, canvas.transform.position, Quaternion.identity);
        htp0text.transform.SetParent(canvas.transform);
        HTP0List.Add(htp0text);
        //htp0text.GetComponent<TextMeshProUGUI>().text = "Text text text more text yes yes yes hhhmm yes very interesting";
        htp0text.GetComponent<TextMeshProUGUI>().enableAutoSizing = true;

        //back button
        GameObject htp0Back = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        htp0Back.transform.SetParent(canvas.transform);
        HTP0List.Add(htp0Back);
        htp0Back.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        htp0Back.GetComponent<Button>().onClick.AddListener(SkillsButtonPressed);
        htp0Back.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(htp0Back.GetComponent<answersScript>());
        htp0Back.GetComponent<Image>().color = buttonColour;

        //
        //How to play 1
        //

        GameObject htp1Title = Instantiate(skillsText, canvas.transform.position, Quaternion.identity);
        htp1Title.transform.SetParent(canvas.transform);
        HTP1List.Add(htp1Title);
        htp1Title.GetComponent<TextMeshProUGUI>().text = "How To Play...";

        GameObject htpo1TempImage = Instantiate(htp1Image, canvas.transform.position, Quaternion.identity);
        htpo1TempImage.transform.SetParent(canvas.transform);
        HTP1List.Add(htpo1TempImage);

        GameObject htp1Back = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        htp1Back.transform.SetParent(canvas.transform);
        HTP1List.Add(htp1Back);
        htp1Back.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        htp1Back.GetComponent<Button>().onClick.AddListener(SkillsButtonPressed);
        htp1Back.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(htp1Back.GetComponent<answersScript>());
        htp1Back.GetComponent<Image>().color = buttonColour;

        //
        //How to play 2
        //

        GameObject htp2Title = Instantiate(skillsText, canvas.transform.position, Quaternion.identity);
        htp2Title.transform.SetParent(canvas.transform);
        HTP2List.Add(htp2Title);
        htp2Title.GetComponent<TextMeshProUGUI>().text = "How To Play...";

        GameObject htpo2TempImage = Instantiate(htp2Image, canvas.transform.position, Quaternion.identity);
        htpo2TempImage.transform.SetParent(canvas.transform);
        HTP2List.Add(htpo2TempImage);

        GameObject htp2Back = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        htp2Back.transform.SetParent(canvas.transform);
        HTP2List.Add(htp2Back);
        htp2Back.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        htp2Back.GetComponent<Button>().onClick.AddListener(SkillsButtonPressed);
        htp2Back.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(htp2Back.GetComponent<answersScript>());
        htp2Back.GetComponent<Image>().color = buttonColour;

        //
        //How to play 3
        //

        GameObject htp3Title = Instantiate(skillsText, canvas.transform.position, Quaternion.identity);
        htp3Title.transform.SetParent(canvas.transform);
        HTP3List.Add(htp3Title);
        htp3Title.GetComponent<TextMeshProUGUI>().text = "How To Play...";

        GameObject htpo3TempImage = Instantiate(htp3Image, canvas.transform.position, Quaternion.identity);
        htpo3TempImage.transform.SetParent(canvas.transform);
        HTP3List.Add(htpo3TempImage);

        GameObject htp3Back = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        htp3Back.transform.SetParent(canvas.transform);
        HTP3List.Add(htp3Back);
        htp3Back.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        htp3Back.GetComponent<Button>().onClick.AddListener(SkillsButtonPressed);
        htp3Back.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(htp3Back.GetComponent<answersScript>());
        htp3Back.GetComponent<Image>().color = buttonColour;

        //
        //How to play 4
        //

        GameObject htp4Title = Instantiate(skillsText, canvas.transform.position, Quaternion.identity);
        htp4Title.transform.SetParent(canvas.transform);
        HTP4List.Add(htp4Title);
        htp4Title.GetComponent<TextMeshProUGUI>().text = "How To Play...";

        GameObject htpo4TempImage = Instantiate(htp4Image, canvas.transform.position, Quaternion.identity);
        htpo4TempImage.transform.SetParent(canvas.transform);
        HTP4List.Add(htpo4TempImage);

        GameObject htp4Back = Instantiate(buttonPrefab, canvas.transform.position, Quaternion.identity);
        htp4Back.transform.SetParent(canvas.transform);
        HTP4List.Add(htp4Back);
        htp4Back.GetComponentInChildren<TextMeshProUGUI>().text = "Back";
        htp4Back.GetComponent<Button>().onClick.AddListener(SkillsButtonPressed);
        htp4Back.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(htp4Back.GetComponent<answersScript>());
        htp4Back.GetComponent<Image>().color = buttonColour;

        //
        //Corner buttons
        //

        GameObject left = Instantiate(cornerButton, canvas.transform.position, Quaternion.identity);
        left.transform.SetParent(canvas.transform);
        cornerList.Add(left);
        left.GetComponentInChildren<TextMeshProUGUI>().text = "<-";
        left.GetComponent<Button>().onClick.AddListener(LeftHTPPressed);
        left.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(left.GetComponent<answersScript>());
        left.GetComponent<Image>().color = buttonColour;
        left.GetComponent<CornerScript>().thisIsRight = false;

        GameObject right = Instantiate(cornerButton, canvas.transform.position, Quaternion.identity);
        right.transform.SetParent(canvas.transform);
        cornerList.Add(right);
        right.GetComponentInChildren<TextMeshProUGUI>().text = "->";
        right.GetComponent<Button>().onClick.AddListener(RightHTPPressed);
        right.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        Destroy(right.GetComponent<answersScript>());
        right.GetComponent<Image>().color = buttonColour;
        right.GetComponent<CornerScript>().thisIsRight = true;

        return;
    }

    void setButtonPosition()
    {
        if (menuState == MenuState.Main)
        {
            for (int i = 0; i < MainButtonList.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((buttonSpacing * canvasHeight) * i), 0);
                MainButtonList[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.Paused)
        {
            for (int i = 0; i < PauseButtonList.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((buttonSpacing * canvasHeight) * i), 0);
                PauseButtonList[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.Settings)
        {
            for (int i = 0; i < SettingsButtonList.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((buttonSpacing * canvasHeight) * i), 0);
                SettingsButtonList[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.Credits)
        {
            for (int i = 0; i < CreditsButtonList.Count; i++)
            {
                Vector3 pos = new Vector3(0, creditsHeightFrom - ((creditsSpacing * canvasHeight) * i), 0);
                CreditsButtonList[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.SkillsBuild)
        {
            for (int i = 0; i < SkillsButtonList.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((buttonSpacing * canvasHeight) * i), 0);
                SkillsButtonList[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.Levels)
        {
            for (int i = 0; i < PlayButtonList.Count; i++)
            {
                Vector3 pos = new Vector3((shipsSpacing * canvasWidth) * i, 0, 0);
                PlayButtonList[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.HTP0)
        {
            for (int i = 0; i < HTP0List.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((htpSpacing * canvasHeight) * i), 0);
                HTP0List[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.HTP1)
        {
            for (int i = 0; i < HTP1List.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((htpSpacing * canvasHeight) * i), 0);
                HTP1List[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.HTP2)
        {
            for (int i = 0; i < HTP2List.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((htpSpacing * canvasHeight) * i), 0);
                HTP2List[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.HTP3)
        {
            for (int i = 0; i < HTP3List.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((htpSpacing * canvasHeight) * i), 0);
                HTP3List[i].transform.position = canvas.transform.position + pos;
            }
        }

        else if (menuState == MenuState.HTP4)
        {
            for (int i = 0; i < HTP4List.Count; i++)
            {
                Vector3 pos = new Vector3(0, startHeightFromStart - ((htpSpacing * canvasHeight) * i), 0);
                HTP4List[i].transform.position = canvas.transform.position + pos;
            }
        }

        /*cornerList[0].transform.position = canvas.transform.position - new Vector3((canvasWidth / 2) - (cornerList[0].GetComponent<RectTransform>().sizeDelta.x / 1.5f), 0, 0);
        cornerList[0].transform.position = new Vector3(cornerList[0].transform.position.x, HTP0List[2].transform.position.y,0);

        cornerList[1].transform.position = canvas.transform.position + new Vector3((canvasWidth / 2) - (cornerList[0].GetComponent<RectTransform>().sizeDelta.x / 1.5f), 0, 0);
        cornerList[1].transform.position = new Vector3(cornerList[1].transform.position.x, HTP0List[2].GetComponent<RectTransform>().localPosition.y, 0);*/

        //HERE Freddie
        /*
                Debug.Log(HTP0List[2].transform.position.y);
                Debug.Log(HTP0List[2].transform.localPosition.y);
                Debug.Log(HTP0List[2].GetComponent<RectTransform>().position.y);
                Debug.Log(HTP0List[2].GetComponent<RectTransform>().localPosition.y);
                Debug.Log(HTP0List[2].GetComponent<RectTransform>().anchoredPosition.y);*/
    }

    void setButtonSize()
    {
        canvasWidth = canvasRectTransform.sizeDelta.x;
        canvasHeight = canvasRectTransform.sizeDelta.y;

        if (menuState == MenuState.Main)
        {
            for (int i = 0; i < MainButtonList.Count; i++)
            {
                MainButtonList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);

                MainButtonList[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
            }
        }

        else if (menuState == MenuState.Paused)
        {
            for (int i = 0; i < PauseButtonList.Count; i++)
            {
                PauseButtonList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);

                PauseButtonList[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
            }
        }

        else if (menuState == MenuState.Settings)
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
            SettingsButtonList[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(160 * 0.8f, 30 * 0.8f);

            SettingsButtonList[3].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            SettingsButtonList[3].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);

            /*float resSize = Mathf.Min(canvasWidth, canvasHeight) * (resScaler / 100f);
            SettingsButtonList[3].transform.localScale = new Vector2(resSize, resSize);*/

        }

        else if (menuState == MenuState.Credits)
        {
            float creditsSize = Mathf.Min(canvasWidth, canvasHeight) * (creditsScaler / 100f);
            CreditsButtonList[0].transform.localScale = new Vector2(creditsSize, creditsSize);

            CreditsButtonList[1].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            CreditsButtonList[1].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
        }

        else if (menuState == MenuState.SkillsBuild)
        {
            SkillsButtonList[0].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            SkillsButtonList[0].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);

            SkillsButtonList[1].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            SkillsButtonList[1].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);

            SkillsButtonList[2].GetComponent<RectTransform>().sizeDelta = new Vector2(0.8f * canvasWidth, buttonHeight * canvasHeight);
            SkillsButtonList[2].GetComponent<TextMeshProUGUI>().text = SkillsText;
            SkillsButtonList[2].GetComponent<TextMeshProUGUI>().color = textColour;
            SkillsButtonList[2].GetComponent<TextMeshProUGUI>().enableAutoSizing = true;

            float skillsSize = Mathf.Min(canvasWidth, canvasHeight) * (resScaler / 100f);
            SettingsButtonList[2].transform.localScale = new Vector2(skillsSize, skillsSize);
            //SettingsButtonList[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>
            SettingsButtonList[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(160 * 0.8f, 30 * 0.8f);

            //SkillsButtonList[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);

            SkillsButtonList[3].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            SkillsButtonList[3].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);

            SkillsButtonList[4].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            SkillsButtonList[4].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
        }

        else if (menuState == MenuState.Levels)
        {
            PlayButtonList[0].GetComponent<RectTransform>().sizeDelta = new Vector2(shipsButtonWidth * canvasWidth, shipsButtonHeight * canvasHeight);
            PlayButtonList[0].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((shipsButtonWidth * canvasWidth) * textboxScalar, (shipsButtonHeight * canvasHeight) * textboxScalar);

            PlayButtonList[1].GetComponent<RectTransform>().sizeDelta = new Vector2(shipsButtonWidth * canvasWidth, shipsButtonHeight * canvasHeight);
            PlayButtonList[1].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((shipsButtonWidth * canvasWidth) * textboxScalar, (shipsButtonHeight * canvasHeight) * textboxScalar);

            PlayButtonList[2].GetComponent<RectTransform>().sizeDelta = new Vector2(shipsButtonWidth * canvasWidth, shipsButtonHeight * canvasHeight);
            PlayButtonList[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((shipsButtonWidth * canvasWidth) * textboxScalar, (shipsButtonHeight * canvasHeight) * textboxScalar);

            PlayButtonList[3].GetComponent<RectTransform>().sizeDelta = new Vector2(shipsButtonWidth * canvasWidth, shipsButtonHeight * canvasHeight);
            PlayButtonList[3].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((shipsButtonWidth * canvasWidth) * textboxScalar, (shipsButtonHeight * canvasHeight) * textboxScalar);

            PlayButtonList[4].GetComponent<RectTransform>().sizeDelta = new Vector2(shipsButtonWidth * canvasWidth, shipsButtonHeight * canvasHeight);
            PlayButtonList[4].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((shipsButtonWidth * canvasWidth) * textboxScalar, (shipsButtonHeight * canvasHeight) * textboxScalar);
        }

        else if (menuState == MenuState.HTP0)
        {
            float titleSize = Mathf.Min(canvasWidth, canvasHeight) * (htpTitleScalar / 100f);
            HTP0List[0].transform.localScale = new Vector2(titleSize, titleSize);

            float size = Mathf.Min(canvasWidth, canvasHeight) * (htp0Scalar / 100f);
            HTP0List[1].transform.localScale = new Vector2(size, size);

            HTP0List[2].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            HTP0List[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
        }

        else if (menuState == MenuState.HTP1)
        {
            float titleSize = Mathf.Min(canvasWidth, canvasHeight) * (htpTitleScalar / 100f);
            HTP1List[0].transform.localScale = new Vector2(titleSize, titleSize);

            float size = Mathf.Min(canvasWidth, canvasHeight) * (htp1Scalar / 100f);
            HTP1List[1].transform.localScale = new Vector2(size, size);

            HTP1List[2].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            HTP1List[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
        }

        else if (menuState == MenuState.HTP2)
        {
            float titleSize = Mathf.Min(canvasWidth, canvasHeight) * (htpTitleScalar / 100f);
            HTP2List[0].transform.localScale = new Vector2(titleSize, titleSize);

            float size = Mathf.Min(canvasWidth, canvasHeight) * (htp2Scalar / 100f);
            HTP2List[1].transform.localScale = new Vector2(size, size);

            HTP2List[2].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            HTP2List[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
        }

        else if (menuState == MenuState.HTP3)
        {
            float titleSize = Mathf.Min(canvasWidth, canvasHeight) * (htpTitleScalar / 100f);
            HTP3List[0].transform.localScale = new Vector2(titleSize, titleSize);

            float size = Mathf.Min(canvasWidth, canvasHeight) * (htp3Scalar / 100f);
            HTP3List[1].transform.localScale = new Vector2(size, size);

            HTP3List[2].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            HTP3List[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
        }

        else if (menuState == MenuState.HTP4)
        {
            float titleSize = Mathf.Min(canvasWidth, canvasHeight) * (htpTitleScalar / 100f);
            HTP4List[0].transform.localScale = new Vector2(titleSize, titleSize);

            float size = Mathf.Min(canvasWidth, canvasHeight) * (htp4Scalar / 100f);
            HTP4List[1].transform.localScale = new Vector2(size, size);

            HTP4List[2].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
            HTP4List[2].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * textboxScalar, (buttonHeight * canvasHeight) * textboxScalar);
        }

        cornerList[0].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
        cornerList[0].transform.GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * cornerScalar, (buttonHeight * canvasHeight) * textboxScalar);

        cornerList[1].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth * canvasWidth, buttonHeight * canvasHeight);
        cornerList[1].transform.GetComponent<RectTransform>().sizeDelta = new Vector2((buttonWidth * canvasWidth) * cornerScalar, (buttonHeight * canvasHeight) * textboxScalar);
    }

    void SetText(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                MainButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton1; // start
                break;
            case 1:
                MainButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton2; // settings
                break;
            case 2:
                MainButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton3; // settings
                break;
            case 3:
                MainButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton4; // credits
                break;
            case 4:
                MainButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton5; // exit
                break;
            default:
                Debug.Log("Unknown button");
                break;
        }
    }

    void SetPauseText(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                PauseButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = "Continue"; // continue
                break;
            case 1:
                PauseButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton2; // settings
                break;
            case 2:
                PauseButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton3; // Credits
                break;
            case 3:
                PauseButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton4; // Exit
                break;
            case 4:
                PauseButtonList[buttonNumber].GetComponentInChildren<TextMeshProUGUI>().text = menuButton5; // exit
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
                MainButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(SkillsButtonPressed);
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
            case 4:
                MainButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(ExitButtonPressed);
                break;
            default:
                Debug.Log("Unknown button");
                break;
        }
    }

    void SetPauseButtonActions(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                PauseButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(UnpauseGame);
                break;
            case 1:
                PauseButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(SettingsButtonPressed);
                break;
            case 2:
                PauseButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(CreditsButtonPressed);
                break;
            case 3:
                PauseButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(BackMainMenuPressed);
                break;
            case 4:
                PauseButtonList[buttonNumber].GetComponent<Button>().onClick.AddListener(BackMainMenuPressed);
                break;
            default:
                Debug.Log("Unknown button");
                break;
        }
    }

    void PositionMenu()
    {
        float averageHeight = 0;
        float averageWidth = 0;

        if (menuState == MenuState.Main)
        {
            for (int i = 0; i < MainButtonList.Count; i++)
            {
                averageHeight += MainButtonList[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= MainButtonList.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < MainButtonList.Count; i++)
            {
                MainButtonList[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if (menuState == MenuState.Paused)
        {
            for (int i = 0; i < PauseButtonList.Count; i++)
            {
                averageHeight += PauseButtonList[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= PauseButtonList.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < PauseButtonList.Count; i++)
            {
                PauseButtonList[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if (menuState == MenuState.Settings)
        {
            for (int i = 0; i < SettingsButtonList.Count; i++)
            {
                averageHeight += SettingsButtonList[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= SettingsButtonList.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < SettingsButtonList.Count; i++)
            {
                SettingsButtonList[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if (menuState == MenuState.Credits)
        {
            for (int i = 0; i < CreditsButtonList.Count; i++)
            {
                averageHeight += CreditsButtonList[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= CreditsButtonList.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < CreditsButtonList.Count; i++)
            {
                CreditsButtonList[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if (menuState == MenuState.SkillsBuild)
        {
            for (int i = 0; i < SkillsButtonList.Count; i++)
            {
                averageHeight += SkillsButtonList[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= SkillsButtonList.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < SkillsButtonList.Count; i++)
            {
                SkillsButtonList[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if (menuState == MenuState.Levels)
        {
            for (int i = 0; i < PlayButtonList.Count; i++)
            {
                averageWidth += PlayButtonList[i].GetComponent<RectTransform>().localPosition.x;
            }
            averageWidth /= PlayButtonList.Count;

            for (int i = 0; i < PlayButtonList.Count; i++)
            {
                PlayButtonList[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(averageWidth, 0, 0);
            }

        }

        else if (menuState == MenuState.HTP0)
        {
            for (int i = 0; i < HTP0List.Count; i++)
            {
                averageHeight += HTP0List[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= HTP0List.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < HTP0List.Count; i++)
            {
                HTP0List[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if (menuState == MenuState.HTP1)
        {
            for (int i = 0; i < HTP1List.Count; i++)
            {
                averageHeight += HTP1List[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= HTP1List.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < HTP1List.Count; i++)
            {
                HTP1List[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if (menuState == MenuState.HTP2)
        {
            for (int i = 0; i < HTP2List.Count; i++)
            {
                averageHeight += HTP2List[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= HTP2List.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < HTP2List.Count; i++)
            {
                HTP2List[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if (menuState == MenuState.HTP3)
        {
            for (int i = 0; i < HTP3List.Count; i++)
            {
                averageHeight += HTP3List[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= HTP3List.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < HTP3List.Count; i++)
            {
                HTP3List[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }

        else if (menuState == MenuState.HTP4)
        {
            for (int i = 0; i < HTP4List.Count; i++)
            {
                averageHeight += HTP4List[i].GetComponent<RectTransform>().localPosition.y;
            }
            averageHeight /= HTP4List.Count;
            //Debug.Log(averageHeight);

            for (int i = 0; i < HTP4List.Count; i++)
            {
                HTP4List[i].GetComponent<RectTransform>().transform.localPosition -= new Vector3(0, averageHeight, 0);
            }
        }
        cornerList[0].transform.position = canvas.transform.position - new Vector3((canvasWidth / 2) - (cornerList[0].GetComponent<RectTransform>().sizeDelta.x / 1.5f), 0, 0);
        cornerList[0].transform.position = new Vector3(cornerList[0].transform.position.x, HTP0List[2].transform.position.y, 0);

        cornerList[1].transform.position = canvas.transform.position + new Vector3((canvasWidth / 2) - (cornerList[0].GetComponent<RectTransform>().sizeDelta.x / 1.5f), 0, 0);
        cornerList[1].transform.position = new Vector3(cornerList[1].transform.position.x, HTP0List[2].transform.position.y, 0);
    }

    void PlayButtonPressed()
    {
        //Add the game start logic here
        //SceneManager.LoadScene(1);
        menuState = MenuState.Levels;
        GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
        Destroy(audio, 3f);
        return;
    }

    void Ship1Start()
    {
        //Add the game start logic here
        SceneManager.LoadScene(1);
        inGame = true;
        return;
    }

    void Ship2Start()
    {
        //Add the game start logic here
        SceneManager.LoadScene(6);
        inGame = true;
        return;
    }

    void Ship3Start()
    {
        //Add the game start logic here
        SceneManager.LoadScene(11);
        inGame = true;
        return;
    }

    void Ship4Start()
    {
        //Add the game start logic here
        SceneManager.LoadScene(16);
        inGame = true;
        return;
    }

    void Ship5Start()
    {
        //Add the game start logic here
        SceneManager.LoadScene(21);
        inGame = true;
        return;
    }

    void SettingsButtonPressed()
    {
        //Swap to settings menu
        menuState = MenuState.Settings;
        GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
        Destroy(audio, 3f);
        return;
    }

    void SkillsButtonPressed()
    {
        //swap to skills build
        menuState = MenuState.SkillsBuild;
        GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
        Destroy(audio, 3f);
        return;
    }

    void CreditsButtonPressed()
    {
        //Swap to credits menu
        menuState = MenuState.Credits;
        GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
        Destroy(audio, 3f);
        return;
    }

    void ExitButtonPressed()
    {
        //Force Quit Game..

        Application.Quit();

        return;
    }
    void BackMainMenuPressed()
    {
        //Back to Main Menu...

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        menuState = MenuState.Main;
        
        return;
    }
    void BackButtonPressed()
    {
        //Go back to main menu
        if (inGame)
            menuState = MenuState.Paused;
        else
            menuState = MenuState.Main;

        GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
        Destroy(audio, 3f);
        return;
    }

    void ControlsPressed()
    {
        menuState = MenuState.HTP0;

        GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
        Destroy(audio, 3f);
    }

    void LeftHTPPressed()
    {
        if (menuState == MenuState.HTP1)
            menuState = MenuState.HTP0;
        else if (menuState == MenuState.HTP2)
            menuState = MenuState.HTP1;
        else if (menuState == MenuState.HTP3)
            menuState = MenuState.HTP2;
        else if (menuState == MenuState.HTP4)
            menuState = MenuState.HTP3;
        GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
        Destroy(audio, 3f);
    }

    void RightHTPPressed()
    {
        if (menuState == MenuState.HTP0)
            menuState = MenuState.HTP1;
        else if (menuState == MenuState.HTP1)
            menuState = MenuState.HTP2;
        else if (menuState == MenuState.HTP2)
            menuState = MenuState.HTP3;
        else if (menuState == MenuState.HTP3)
            menuState = MenuState.HTP4;
        GameObject audio = Instantiate(lTick, transform.position, transform.rotation);
        Destroy(audio, 3f);
    }

    void PauseGame()
    {
        lpf.enabled = true;
        Time.timeScale = 0;
    }

    void UnpauseGame()
    {
        lpf.enabled = false;
        Time.timeScale = 1;
        menuState = MenuState.Running;
    }
}
