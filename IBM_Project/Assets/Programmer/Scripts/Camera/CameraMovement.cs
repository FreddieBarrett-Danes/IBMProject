using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 temp = new Vector3(player.transform.position.x, this.gameObject.transform.position.y, player.transform.position.z);
        this.gameObject.transform.position = temp;
    }
}
