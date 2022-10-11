using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed;
    public GameObject player;

    private Vector3 move;
    public float rotationSpeed;
	
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(0, 0, 0);
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        this.gameObject.transform.Translate(move * Speed *Time.deltaTime);

        if(move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
