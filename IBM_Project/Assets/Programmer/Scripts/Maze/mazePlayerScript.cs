using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mazePlayerScript : MonoBehaviour
{
    public float speed;
    public bool returnToStartUponCollision;
    bool touchWall;
    public TextMeshProUGUI Timer;
    [SerializeField]
    private AudioSource wallHitSound;
    bool mazeReadyPlayer;

    public int timesHit;

    private void OnEnable()
    {
        //walGen.OnMazeReady += applyReady;
    }

    private void OnDisable()
    {
        //walGen.OnMazeReady -= applyReady;
    }

    void applyReady(bool mazeReady)
    {
        //mazeReadyPlayer = mazeReady;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mazeWall")
        {
            //Debug.Log("Trigger Wall hit");
            if (returnToStartUponCollision == true) transform.position = new Vector3(64, 0, 62);//(2, 0, 0);
            touchWall = true;
            wallHitSound.Play();
            timesHit++;
        }
        else if (other.gameObject.tag == "goalLocation")
        {
            //Debug.Log("Maze win");

            //Setting gameobjects invisible:
            GameObject.FindGameObjectWithTag("goalLocation").GetComponent<MeshRenderer>().enabled = false;
            GameObject.FindGameObjectWithTag("mazePlayer").GetComponent<MeshRenderer>().enabled = false;
            Timer.GetComponent<TextMeshProUGUI>().enabled = false;

            //Method transforming gameobjects to different location:

            //GameObject.FindGameObjectWithTag("goalLocation").transform.position = new Vector3(-100,100,-100);
            //GameObject.FindGameObjectWithTag("mazePlayer").transform.position = new Vector3(-100, 80, -100);



            //Method deactivating gameobjects:

            //GameObject.FindWithTag("goalLocation").SetActive(false);
            //GameObject.FindWithTag("mazePlayer").SetActive(false);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "mazeWall")
        {
            //Debug.Log("Trigger Wall exit");
            //transform.position = new Vector3(2, 0, 0);
            touchWall = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mazeReadyPlayer = false;
        gameObject.GetComponent<Renderer>().material.color = Color.white; //Setting colour of Player gameobject
        GameObject goalLocation = GameObject.FindGameObjectWithTag("goalLocation");
        goalLocation.GetComponent<Renderer>().material.color = Color.green;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (mazeReadyPlayer)
        {
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

            if (Input.GetKeyDown("p"))
            {
                transform.position = GameObject.FindGameObjectWithTag("goalLocation").transform.position;
            }
        }
        if (Input.GetKeyDown("space"))// && mazeReadyPlayer == true)
        {
            mazeReadyPlayer = true;
            //transform.position = new Vector3(62, 0, 60);
            transform.position = new Vector3(64, 0, 62);
            //returnToStart(true);
        }


        //Attempted to convert input to a switch case, didn't turnout as smooth
        //var mazeInput = Input.inputString;
        //switch (mazeInput)
        //{
        //    case "d":
        //        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        //        if (touchWall == true)
        //        {
        //            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0) * 3;
        //        }
        //        break;
        //    case "w":
        //        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        //        if (touchWall == true)
        //        {
        //            transform.position -= new Vector3(0, 0, speed * Time.deltaTime) * 3;
        //        }
        //        break;
        //    case "a":
        //        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        //        if (touchWall == true)
        //        {
        //            transform.position += new Vector3(speed * Time.deltaTime, 0, 0) * 3;
        //        }
        //        break;
        //    case "s":
        //        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        //        if (touchWall == true)
        //        {
        //            transform.position += new Vector3(0, 0, speed * Time.deltaTime) * 3;
        //        }
        //        break;
        //    case "space":
        //        transform.position = new Vector3(2, 0, 0);
        //        break;
        //}


    }
}
