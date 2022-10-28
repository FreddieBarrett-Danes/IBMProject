using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallDestory : MonoBehaviour
{
    

    //int Num;
    //int Repeat;
    //bool touch;
    //GameObject currentPos;
    //GameObject targetPos;
    private void OnCollisionEnter(Collision collision)
    {
        
        //Debug.Log("Collision Detected");
        if (collision.gameObject.tag == "mazeWall")
        {
            //Debug.Log("Hit wall");
            Object.Destroy(collision.gameObject);
            //Num = 0;
            //Repeat = 0;
            
        }

        if (collision.gameObject.tag == "goalLocation")
        {

        }

        if (collision != null)
        {
            //touch = true;
        }
        else
        {
            //touch = false;
        }
        //Object.Destroy(collision.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //currentPos = GameObject.FindGameObjectWithTag("currentPos");
        //targetPos = GameObject.FindGameObjectWithTag("targetPos");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //    Num = Random.Range(1, 5);
        //    if (Num != Repeat)
        //    {
        //        Debug.Log(Num); //Random direction
        //        Num = Repeat;
        //    }
        //    //Vector3 dir = targetPos.transform.position - currentPos.transform.position;
        //    //currentPos.transform.position += (dir.normalized) * Time.deltaTime;
        //    //currentPos.transform.rotation = Quaternion.Euler(90, 0, 0);

        //}
    }
}
