using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DoorsScript : MonoBehaviour
{
    private Transform ADoors;
    private Transform BDoors;

    public GameObject openDoor;
    public GameObject closeDoor;

    [SerializeField]
    private Vector3 restingDoorPos;

    private GameObject player;
    private float distToPlayer = 100f;

    private List<GameObject> enemies = new List<GameObject>();
    private float nearestEnemy = 100f;

    [SerializeField]
    private float activateDistance;     // Distance between player and door to open
    [SerializeField]
    private float openDistance;         // How far doors should open

    public bool isOpen;
    public bool wasOpen;

    public bool isComputer;

    private GameController gameController;


    //

    private float openAmount10;
    [SerializeField]
    private float moveSpeed;

    private Vector3 AOpen, AClosed, BOpen, BClosed;


    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)).GameObject();
        ADoors = this.gameObject.transform.Find("ADoor");
        BDoors = this.gameObject.transform.Find("BDoor");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        FindEnemiesInScene();

        //restingDoorPos = ADoors.localPosition;
        restingDoorPos = new Vector3(-0.75f, 0.5f, 0f);

        openAmount10 = 1;
        wasOpen = isOpen;
    }

    void Update()
    {
        FindEnemiesInScene();

        nearestEnemy = (this.transform.position - player.transform.position).magnitude;
        nearestEnemy = Mathf.Infinity;


        for (int i = 0; i < enemies.Count; i++)
        {
            if ((this.transform.position - enemies[i].transform.position).magnitude < nearestEnemy)
            {
                nearestEnemy = (this.transform.position - enemies[i].transform.position).magnitude;
            }
        }

        distToPlayer = (this.transform.position - player.transform.position).magnitude;

        if (!isComputer)
        {
            //try here
            //player.GetComponent<PlayerController>().computerDoor = false;
            //player.GetComponent<PlayerController>().elevatorDoor = false;

            if (distToPlayer < activateDistance || nearestEnemy < activateDistance) //open door
            {
                openAmount10 -= moveSpeed * Time.deltaTime;

                isOpen = true;
            }

            else //close door
            {
                openAmount10 += moveSpeed * Time.deltaTime;
                isOpen = false;
            }

            openAmount10 = Mathf.Clamp01(openAmount10);

            AOpen = ADoors.transform.localPosition = new Vector3(-openDistance, restingDoorPos.y, restingDoorPos.z);
            AClosed = ADoors.transform.localPosition = restingDoorPos;
            BOpen = BDoors.transform.localPosition = new Vector3(openDistance, restingDoorPos.y, restingDoorPos.z);
            BClosed = BDoors.transform.localPosition = new Vector3(-restingDoorPos.x, restingDoorPos.y, restingDoorPos.z);

            ADoors.transform.localPosition = Vector3.Lerp(AOpen, AClosed, openAmount10);
            BDoors.transform.localPosition = Vector3.Lerp(BOpen, BClosed, openAmount10);
        }
        else if (isComputer)
        {
            
            if (distToPlayer < activateDistance)//close to computer door
            {
                player.GetComponent<PlayerController>().computerDoor = true;
                player.GetComponent<PlayerController>().door = gameObject.GetComponent<DoorsScript>();
                
            }

            else //not close to computer door
            {
                player.GetComponent<PlayerController>().computerDoor = false;
                player.GetComponent<PlayerController>().door = null;
                
            }
        }

        if (isOpen != wasOpen)
        {
            DoorStateChanged();
        }

        wasOpen = isOpen;
    }

    private void FindEnemiesInScene()
    {
        enemies.Clear();

        BotInfo[] botScripts = FindObjectsOfType<BotInfo>();
        enemies = botScripts.Select(t => t.transform.gameObject).ToList();
    }

    private void DoorStateChanged()
    {
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 playerPos = player.transform.position;
        float range = Vector3.Distance(enemyPos, playerPos);
        Vector3 toTarget = enemyPos - playerPos;
        Vector3 dirToTarget = toTarget.normalized;

        bool inRange = false;

        if ((range < player.gameObject.GetComponent<FieldOfView>().viewRadius - 0.05f &&
                 Vector3.Angle(transform.forward, dirToTarget) <
                 player.gameObject.GetComponent<FieldOfView>().viewAngle / 2)
                && !Physics.Raycast(transform.position, dirToTarget, toTarget.magnitude,
                    player.gameObject.GetComponent<FieldOfView>().obstacleMask))
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
        if (isOpen && inRange)
        {
                GameObject sound = Instantiate(openDoor, transform.position, Quaternion.identity);
                Destroy(sound, 2f);
                //Spawn open sound in here
        }

        else if(!isOpen && inRange)
        {
            GameObject sound = Instantiate(closeDoor, transform.position, Quaternion.identity);
            Destroy(sound, 2f);
            //Spawn close sound in here
        }
    }
}