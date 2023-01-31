using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngineInternal;

public class DoorsScript : MonoBehaviour
{
    private Transform ADoors;
    private Transform BDoors;

    private Vector3 restingDoorPos;

    private GameObject player;
    private float distToPlayer;

    private List<GameObject> enemies = new List<GameObject>();
    private float nearestEnemy;

    [SerializeField]
    private float activateDistance;     // Distance between player and door to open
    [SerializeField]
    private float openDistance;         // How far doors should open

    private bool isOpen;
    private bool wasOpen;

    //

    public bool isLerping;

    public float lerpMultiplier;
    public float lerpTime;

    private Vector3 start;
    private Vector3 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ADoors = this.gameObject.transform.Find("ADoor");
        BDoors = this.gameObject.transform.Find("BDoor");

        FindEnemiesInScene();

        restingDoorPos = ADoors.localPosition;
    }

    void Update()
    {
        nearestEnemy = (this.transform.position - player.transform.position).magnitude;
        nearestEnemy = Mathf.Infinity;

        for(int i = 0; i < enemies.Count; i++)
        {
            if ((this.transform.position - enemies[i].transform.position).magnitude < nearestEnemy)
            {
                nearestEnemy = (this.transform.position - enemies[i].transform.position).magnitude;
            }
        }

        distToPlayer = (this.transform.position - player.transform.position).magnitude;

        if(distToPlayer < activateDistance || nearestEnemy < activateDistance) //open door
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
                start = new Vector3(-openDistance, restingDoorPos.y, restingDoorPos.z);
                target = restingDoorPos;
            }
            if (!isOpen)
            {
                CloseDoor();
                target = new Vector3(-openDistance, restingDoorPos.y, restingDoorPos.z);
                start = restingDoorPos;
            }
        }

        if (isLerping)
        {
            Vector3 LerpLocation;

            lerpTime = (lerpTime + Time.deltaTime) * lerpMultiplier;

            LerpLocation = Vector3.Lerp(start, restingDoorPos, lerpTime);

            if(lerpTime > 1f)
            {
                isLerping = false;
                lerpTime = 0f;
                //lerp is done
            }
        }
    }

    private void OpenDoor()
    {
        ADoors.transform.localPosition = new Vector3(-openDistance, restingDoorPos.y, restingDoorPos.z);
        BDoors.transform.localPosition = new Vector3(openDistance, restingDoorPos.y, restingDoorPos.z);
    }

    private void CloseDoor()
    {
        ADoors.transform.localPosition = restingDoorPos;
        BDoors.transform.localPosition = new Vector3(-restingDoorPos.x, restingDoorPos.y, restingDoorPos.z);
    }

    private void FindEnemiesInScene()
    {
        enemies.Clear();

        BotInfo[] botScripts = FindObjectsOfType<BotInfo>();
        enemies = botScripts.Select(t => t.transform.gameObject).ToList();
    }

    /*public Vector3 LerpBetween(Vector3 startPos, Vector3 endPos, float lerpTime)
    {
        Vector3 LerpLocation;

        LerpLocation = Vector3.Lerp(startPos,endPos))

        return LerpLocation;
    }*/
}