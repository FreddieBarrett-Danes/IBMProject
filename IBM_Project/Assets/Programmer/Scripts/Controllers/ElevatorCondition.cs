using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCondition : MonoBehaviour
{
    private GameObject Computer;
    // Start is called before the first frame update
    void Start()
    {
        Computer = GameObject.FindGameObjectWithTag("Computer");
    }

    // Update is called once per frame
    void Update()
    {
        if(Computer.GetComponent<ComputerInteraction>().enemiesArray == null)
        {
            Destroy(this.gameObject);
        }
    }
}
