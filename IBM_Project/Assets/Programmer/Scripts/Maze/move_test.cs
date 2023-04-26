using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_test : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Cube Collision");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Time.deltaTime, 0, 0);
        
    }
}
