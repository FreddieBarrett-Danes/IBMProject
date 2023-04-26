using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCondition : MonoBehaviour
{
    private GameObject computer;
    // Start is called before the first frame update
    void Start()
    {
        computer = GameObject.FindGameObjectWithTag("Computer");
    }

    // Update is called once per frame
    void Update()
    {
        if(computer.GetComponent<ComputerInteraction>().enemiesArray == null)
        {
            Destroy(this.gameObject);
        }
    }
}
