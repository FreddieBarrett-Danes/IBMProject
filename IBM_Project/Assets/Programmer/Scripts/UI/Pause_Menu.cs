using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause_Menu : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    bool pauseToggle;
    bool uiUpdate;
    private bool mouseHover;
    public Sprite[] sprite = new Sprite[2];

    //Sprite array guide:
    //Sprite 1 [0]- Button when not hovered or clicked
    //Sprite 2 [1]- Button when hovered by mouse pointer
    //Sprite 3 [2]- Button when click is held down (but not released)

    //public delegate void DelType1(bool mazeReady); //Delegate type
    //public static event DelType1 OnMazeReady; //Event variable

    public delegate void Event1();
    public static event Event1 onUIUpdate2;

    private void OnEnable()
    {
        //walGen.OnMazeReady += timerReady;
        Pause_Menu.onUIUpdate2 += newUpdate;
    }
    private void OnDisable()
    {
        Pause_Menu.onUIUpdate2 -= newUpdate;
    }

    

    //Sets the sprite array list to 3 if it isn't
    void OnValidate()
    {
        if (sprite.Length != 3)
        {
            Debug.Log("Each UI element needs 3 sprites exactly! (errors will occur otherwise) " + name);
            Array.Resize(ref sprite, 3);
            //Inspired by:
            //https://answers.unity.com/questions/38943/public-fixed-size-array-in-inspector.html
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = sprite[1];
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = sprite[0];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = sprite[2];
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("UI element clicked");
        switch (name)
        {
            case "Paused":
                Debug.Log("Paused button pressed");
                //PauseToggle = true;
                //UIupdate = true;
                //OnUIupdate2();
                break;

            case "Resume":
                Debug.Log("Resume button pressed");
                onUIUpdate2();
                break;
            case "Settings":
                Debug.Log("Settings button pressed");
                break;
            case "Main_Menu":
                Debug.Log("Main Menu button pressed");
                break;
            case "Quit":
                Debug.Log("Quit button pressed");
                //Application.Quit();
                break;

            case null:
                Debug.Log("Unknown button pressed");
                break;
        }
    }

    void newUpdate()
    {
        if (pauseToggle == false) { pauseToggle = true; }
        else { pauseToggle = false; }
        Debug.Log(pauseToggle);
        this.GetComponent<Image>().enabled = pauseToggle;
        uiUpdate = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        //sprite = this.GetComponent<Image>().sprite;
        mouseHover = false;
        uiUpdate = false;
        pauseToggle = false;
        this.GetComponent<Image>().enabled = pauseToggle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (uiUpdate == true) )
        {
            newUpdate();
        }

        if (mouseHover == true)
        {
            //this.GetComponent<Image>().sprite = sprite;
        }
    }


}
