using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int speed;
    public int frameRate;
    public Vector2 StartPos;
    RectTransform rectTrans;
    bool touchWall;
    public GameObject uiPlayer;

    void OnCollisionEnter2D(Collision2D col)
    {
        //transform.position.an = new Vector2(StartPos.x, StartPos.y);
        rectTrans.anchoredPosition = new Vector2(StartPos.x, StartPos.y);
        //touchWall = true;
        //transform.position -= new Vector3((speed*3) * Time.deltaTime, 0, 0);
        
        //speed = speed * -1;
       //Debug.Log("Collision Detected");
       //speed = speed * -1;
        //touchWall = true;
        //Debug.Log("Collision detected" + collision.collider.name);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger Collision");
        //touchWall = true;
       // speed = speed * -1;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Exiting Trigger");
        //touchWall = false;
       // speed = speed * -1;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        touchWall = false;
        //Debug.Log("Exiting Collision");
        //speed = speed * 1;
        //touchWall = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        uiPlayer.SetActive(false);
        rectTrans = GetComponent<RectTransform>();
        //speed = 10;
        Application.targetFrameRate = frameRate;
        touchWall = false;
        rectTrans.anchoredPosition = new Vector2(StartPos.x, StartPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        //To convert to switch case or FSM, just prototyping movement
            if (Input.GetKey("w"))
            {
            rectTrans.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
            //transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            if (touchWall == true)
            {
                transform.position -= new Vector3(0, (speed*5) * Time.deltaTime, 0);
            }
            }
            if (Input.GetKey("d"))
            {
            rectTrans.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);
            //transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (touchWall == true)
            {
                transform.position -= new Vector3((speed*5) * Time.deltaTime, 0, 0);
            }
            }
            if (Input.GetKey("s"))
            {
            rectTrans.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);
            //transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            if (touchWall == true)
            {
                transform.position += new Vector3(0, (speed*5) * Time.deltaTime, 0);
            }
            }

            if (Input.GetKey("a"))
            {
            rectTrans.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
            //transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (touchWall == true)
            {
                transform.position += new Vector3((speed*5) * Time.deltaTime, 0, 0);
            }
            }
        


    }
}
