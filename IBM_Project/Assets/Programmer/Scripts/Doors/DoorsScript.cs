using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class DoorsScript : MonoBehaviour
{
    private Transform ADoors;
    private Transform BDoors;

    private Vector3 restingDoorPos;

    private GameObject player;
    private float distToPlayer;

    [SerializeField]
    private float activateDist;     // Distance between player and door to open
    public float openDist;          // How far doors should open

    private bool isOpen;
    private bool wasOpen;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ADoors = this.gameObject.transform.Find("ADoor");
        BDoors = this.gameObject.transform.Find("BDoor");

        restingDoorPos = ADoors.localPosition;
    }

    void Update()
    {
        distToPlayer = (this.transform.position - player.transform.position).magnitude;

        if(distToPlayer < activateDist) //open door
        {
            isOpen = true;
        }
        else //close door
        {
            isOpen = false;
        }

        if(isOpen != wasOpen)
        {
            wasOpen = isOpen;

            if (isOpen)
            {
                OpenDoor();
            }
            if (!isOpen)
            {
                CloseDoor();
            }
        }
    }

    private void OpenDoor()
    {
        ADoors.transform.localPosition = new Vector3(-openDist, restingDoorPos.y, restingDoorPos.z);
        BDoors.transform.localPosition = new Vector3(openDist, restingDoorPos.y, restingDoorPos.z);
    }

    private void CloseDoor()
    {
        ADoors.transform.localPosition = restingDoorPos;
        BDoors.transform.localPosition = new Vector3(-restingDoorPos.x, restingDoorPos.y, restingDoorPos.z);
    }
}
