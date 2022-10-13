using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    public GameObject visuals;

    private GameObject mainCamera;

    public bool isBehindEnemy;

    private Vector3 movePlayer;
    private Vector3 moveCamera;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        Movement();
        TakeControl();
    }
    
    //old movement function, could still be used do not delete
    /*private void Movement()
    {
        //move = new Vector3(0, 0, 0);
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        this.gameObject.transform.Translate(move * speed * Time.deltaTime);

        if (move != Vector3.zero)
        {
            Vector3 NewAt = Vector3.Cross(transform.up, move);

            Quaternion toRotation = new Quaternion();
            toRotation.SetLookRotation(NewAt);
            toRotation *= Quaternion.Euler(0, -90, 0);
            visuals.transform.rotation = toRotation;
        }
    }*/

    //uses Input.KeyCode to check for button presses, can change to looking through Input Manager
    private void Movement()
    {
        //Temporary vectors to update object transforms
        Vector3 tempPlayerMove = new Vector3(0, 0, 0);
        Vector3 tempCameraMove = new Vector3(0, 0, 0);
        
        //Input Checks
        if (Input.GetKey(KeyCode.A))
        {
            tempPlayerMove += Vector3.left;
            tempCameraMove += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            tempPlayerMove += Vector3.right;
            tempCameraMove += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            tempPlayerMove += Vector3.forward;
            tempCameraMove += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            tempPlayerMove += Vector3.back;
            tempCameraMove += Vector3.down;
        }
        
        //normalise for consistent movement
        movePlayer = tempPlayerMove.normalized;
        moveCamera = tempCameraMove.normalized;

        //add new translations to player and camera
        gameObject.transform.Translate(movePlayer * (speed * Time.deltaTime));
        mainCamera.transform.Translate(moveCamera * (speed * Time.deltaTime));

        //rotates the player visual to give a visual representation for direction of movement
        if (movePlayer == Vector3.zero) return;
        Vector3 NewAt = Vector3.Cross(transform.up, movePlayer);

        Quaternion toRotation = new Quaternion();
        toRotation.SetLookRotation(NewAt);
        toRotation *= Quaternion.Euler(0, -90, 0);
        visuals.transform.rotation = toRotation;


    }
    private void TakeControl()
    {
        if (!isBehindEnemy) return;
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player trying to control enemy");
        }
    }
}
