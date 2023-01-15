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

    public List<GameObject> enemies = new List<GameObject>();
    public float nearestEnemy;

    [SerializeField]
    private float activateDist;     // Distance between player and door to open
    [SerializeField]
    private float openDist;          // How far doors should open

    private bool isOpen;
    private bool wasOpen;

    //

    public bool isLerping;

    public float lerpMultiplier;
    public float lerpTime;

    private Vector3 tempStart;
    private Vector3 tempTarget;

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
        nearestEnemy = 100f;

        for(int i = 0; i < enemies.Count; i++)
        {
            if ((this.transform.position - enemies[i].transform.position).magnitude < nearestEnemy)
            {
                nearestEnemy = (this.transform.position - enemies[i].transform.position).magnitude;
            }
        }

        distToPlayer = (this.transform.position - player.transform.position).magnitude;

        if(distToPlayer < activateDist || nearestEnemy < activateDist) //open door
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
                tempStart = new Vector3(-openDist, restingDoorPos.y, restingDoorPos.z);
                tempTarget = restingDoorPos;
            }
            if (!isOpen)
            {
                CloseDoor();
                tempTarget = new Vector3(-openDist, restingDoorPos.y, restingDoorPos.z);
                tempStart = restingDoorPos;
            }
        }

        if (isLerping)
        {
            Vector3 LerpLocation;

            lerpTime = (lerpTime + Time.deltaTime) * lerpMultiplier;

            LerpLocation = Vector3.Lerp(tempStart, restingDoorPos, lerpTime);

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
        ADoors.transform.localPosition = new Vector3(-openDist, restingDoorPos.y, restingDoorPos.z);
        BDoors.transform.localPosition = new Vector3(openDist, restingDoorPos.y, restingDoorPos.z);
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
        enemies = botScripts.Select(t => t.transform.root.gameObject).ToList();
    }

    /*public Vector3 LerpBetween(Vector3 startPos, Vector3 endPos, float lerpTime)
    {
        Vector3 LerpLocation;

        LerpLocation = Vector3.Lerp(startPos,endPos))

        return LerpLocation;
    }*/
}