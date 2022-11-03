using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalLocationScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mazePlayer")
        {
            Debug.Log("Maze Win");
            Debug.Log("Refer to goalLocationScript for Maze output");
            

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
