using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    bool touchWall;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mazeWall")
        {
            Debug.Log("Trigger Wall hit");
            touchWall = true;
        }
        //else if (other.gameObject == null)
        //{
        //    Debug.Log("Wall exit");
        //    touchWall = false;
        //}
        if (other.gameObject.tag == "goalLocation")
        {
            Debug.Log("Maze win");
            GameObject.FindWithTag("goalLocation").SetActive(false);
            GameObject.FindWithTag("mazePlayer").SetActive(false);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "mazeWall")
        {
            Debug.Log("Wall exit");
            touchWall = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("Collision Detected");
        if (collision.gameObject.tag == "mazeWall")
        {
            Debug.Log("Wall hit");
            touchWall = true;
        }
        else
        {
            touchWall = false;
        }
        if (collision.gameObject.tag == "goalLocation")
        {
            Debug.Log("Maze win");
        }
        //if (collision.gameObject.tag == "mazeWall")
        //{
        //    //Debug.Log("Hit wall");
        //    Object.Destroy(collision.gameObject);
        //    Num = 0;
        //    Repeat = 0;

        //}

        //if (collision.gameObject.tag == "goalLocation")
        //{

        //}

        //if (collision != null)
        //{
        //    touch = true;
        //}
        //else
        //{
        //    touch = false;
        //}
        //Object.Destroy(collision.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        GameObject test = GameObject.FindGameObjectWithTag("goalLocation");
        test.GetComponent<Renderer>().material.color = Color.green;
        //Color test2 = gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        //Debug.Log("Player");

        //convert to switch case
        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (touchWall == true)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0) * 3;
            }
        }
        if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            if (touchWall == true)
            {
                transform.position -= new Vector3(0, 0, speed * Time.deltaTime) * 3;
            }
        }
        if (Input.GetKey("a"))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (touchWall == true)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0) * 3;
            }
        }
        if (Input.GetKey("s"))
        {
            transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
            if (touchWall == true)
            {
                transform.position += new Vector3(0, 0, speed * Time.deltaTime) * 3;
            }
        }
        if (Input.GetKeyDown("space"))
        {
            transform.position = new Vector3(2, 0, 0);
        }
    }
}
