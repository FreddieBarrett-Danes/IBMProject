using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalLocationScript : MonoBehaviour
{
    private GameController gC;
    private ComputerInteraction cI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mazePlayer")
        {
            Debug.Log("Maze Win");
            Debug.Log("Refer to goalLocationScript for Maze output");
            gC.inMinigame = false;
            cI.completedMinigame = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
        cI = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
