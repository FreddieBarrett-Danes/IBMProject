using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Init : MonoBehaviour
{
    public GameObject[] UI;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject ui in UI)
        {
            ui.SetActive(true);
        }
    }
}
