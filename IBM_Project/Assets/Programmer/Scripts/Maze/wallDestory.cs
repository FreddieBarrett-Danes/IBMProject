using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallDestory : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        //Debug.Log("Collision Detected");
        if (collision.gameObject.tag == "mazeWall")
        {
            Destroy(collision.gameObject);

            
        }

        //if (collision.gameObject.tag == "goalLocation")
        //{

        //}
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
