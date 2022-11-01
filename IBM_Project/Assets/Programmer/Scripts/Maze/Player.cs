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
            //Debug.Log("Trigger Wall hit");
            transform.position = new Vector3(2, 0, 0);
            //touchWall = true;
        }
        //else if (other.gameObject == null)
        //{
        //    Debug.Log("Wall exit");
        //    touchWall = false;
        //}
        if (other.gameObject.tag == "goalLocation")
        {
            Debug.Log("Maze win");

            //Setting gameobjects invisible
            GameObject.FindGameObjectWithTag("goalLocation").GetComponent<MeshRenderer>().enabled = false;
            GameObject.FindGameObjectWithTag("mazePlayer").GetComponent<MeshRenderer>().enabled = false;

            //Method transforming gameobjects to different location
            //GameObject.FindGameObjectWithTag("goalLocation").transform.position = new Vector3(-100,100,-100);
            //GameObject.FindGameObjectWithTag("mazePlayer").transform.position = new Vector3(-100, 80, -100);



            //Method deactivating gameobjects
            //GameObject.FindWithTag("goalLocation").SetActive(false);
            //GameObject.FindWithTag("mazePlayer").SetActive(false);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "mazeWall")
        {
            //Debug.Log("Trigger Wall exit");
            transform.position = new Vector3(2, 0, 0);
            //touchWall = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white; //Setting colour of Player gameobject
        GameObject goalLocation = GameObject.FindGameObjectWithTag("goalLocation");
        goalLocation.GetComponent<Renderer>().material.color = Color.green;
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
