using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public GameObject visuals;

    public bool isBehindEnemy = false;

    private Vector3 move;

    void Update()
    {
        Movement();
        TakeControl();
    }

    private void Movement()
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
    }
    private void TakeControl()
    {
        if(isBehindEnemy)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player trying to control enemy");
            }
        }
    }
}