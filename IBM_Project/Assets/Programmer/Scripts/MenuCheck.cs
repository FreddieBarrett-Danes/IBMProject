using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCheck : MonoBehaviour
{
    public GameObject Menu;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("MenuController") == null)
        {
            Instantiate(Menu);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
