using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CornerScript : MonoBehaviour
{
    public bool thisIsRight;
    private void Update()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.GetComponent<Button>().enabled = true;
        this.GetComponent<Image>().enabled = true;

        if (GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>().menuState == MenuController.MenuState.HTP0 && !thisIsRight)
        {
            //hide left
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.GetComponent<Button>().enabled = false;
            this.GetComponent<Image>().enabled = false;
        }
        
        if(GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>().menuState == MenuController.MenuState.HTP4 && thisIsRight)
        {
            //hide right
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.GetComponent<Button>().enabled = false;
            this.GetComponent<Image>().enabled = false;
        }
    }
}
