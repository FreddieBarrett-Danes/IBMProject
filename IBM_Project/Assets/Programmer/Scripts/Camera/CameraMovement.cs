using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    
    // Update is called once per frame
    private void Update()
    {
        var temp = new Vector3(player.transform.position.x, mainCamera.transform.position.y, player.transform.position.z);
        mainCamera.transform.position = temp;
    }
}
