using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    
    // Update is called once per frame
    private void Update()
    {
        Vector3 temp = new Vector3(player.transform.position.x, this.gameObject.transform.position.y, player.transform.position.z);
        this.gameObject.transform.position = temp;
    }
}
