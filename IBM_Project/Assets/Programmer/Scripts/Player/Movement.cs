using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement Instance;
    public float Speed;
    public GameObject player;
//  public int Framerate; //Testing movement's consistant regardless of framerate
    private Vector3 Origin;
    Vector3 Distance;
	
	
    // Start is called before the first frame update
    void Start()
    {
        //   Application.targetFrameRate = Framerate;
        //Timer = 0;
        if (!Instance)
        {
            Instance = this;
        }

        //player = GameObject.FindGameObjectWithTag("Player");

        Origin = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Using Input.GetKey
        if (Input.GetKey("w"))
        {
            player.transform.position += new Vector3(0.0f, 0.0f, 0.1f) * (Time.deltaTime * Speed);
        }
        if (Input.GetKey("s"))
        {
            player.transform.position += new Vector3(0.0f, 0.0f, -0.1f) * (Time.deltaTime * Speed);
        }
        if (Input.GetKey("d"))
        {
            player.transform.position += new Vector3(0.1f, 0.0f, 0.0f) * (Time.deltaTime * Speed);
        }
        if (Input.GetKey("a"))
        {
            player.transform.position += new Vector3(-0.1f, 0.0f, 0.0f) * (Time.deltaTime * Speed);
        }
    }
}
