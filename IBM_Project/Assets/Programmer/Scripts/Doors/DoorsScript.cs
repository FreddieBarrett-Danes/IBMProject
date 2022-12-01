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
    private float activeDist;
    public float openDist;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ADoors = this.gameObject.transform.Find("ADoor");
        BDoors = this.gameObject.transform.Find("BDoor");

        restingDoorPos = ADoors.localPosition;
        //openDoorPos = new Vector3(restingDoorPos.x + 1.5f, restingDoorPos.y, restingDoorPos.z);
        //doors[0] = gameObject.transform.Find("LeftDoor");
    }

    void Update()
    {
        distToPlayer = (this.transform.position - player.transform.position).magnitude;
        if(distToPlayer < activeDist) //open door
        {
            ADoors.transform.localPosition = new Vector3(-openDist, restingDoorPos.y, restingDoorPos.z);
            BDoors.transform.localPosition = new Vector3(openDist, restingDoorPos.y, restingDoorPos.z);
        }
        else //close door
        {
            ADoors.transform.localPosition = restingDoorPos;
            BDoors.transform.localPosition = new Vector3(-restingDoorPos.x, restingDoorPos.y, restingDoorPos.z);
        }
    }

    private void OpenDoor()
    {

    }

    private void CloseDoor()
    {

    }
}
