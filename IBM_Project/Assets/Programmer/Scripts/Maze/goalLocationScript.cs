using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalLocationScript : MonoBehaviour
{
    private GameController gC;
    private MinigameController mC;
    private ComputerInteraction cI;
    public walGen wG;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("mazePlayer"))
        {
            Debug.Log("Maze Win");
            Debug.Log("Refer to goalLocationScript for Maze output");
            gC.inMinigame = false;
            wG.Timer.SetActive(false);
            mC.completedMinigame = true;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cI = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();
        mC = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
