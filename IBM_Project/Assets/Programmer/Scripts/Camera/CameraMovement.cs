using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;
    public GameObject MainCamera;

    private Transform TMainCamera;
    private Transform TPlayer;

    // Start is called before the first frame update
    void Start()
    {
        TMainCamera = MainCamera.transform;
        TPlayer = Player.transform;
        //MainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3(Player.transform.position.x, MainCamera.transform.position.y, Player.transform.position.z);
        MainCamera.transform.position = temp;
    }
}
