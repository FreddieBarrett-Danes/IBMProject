using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuCheck : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("MenuController") == null)
        {
            Instantiate(menu);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
