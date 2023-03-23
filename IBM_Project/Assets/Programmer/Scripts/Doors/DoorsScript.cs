using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorsScript : MonoBehaviour
{
    private Transform ADoors;
    private Transform BDoors;

    public GameObject openDoor;
    public GameObject closeDoor;

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

    public bool isComputer;    

    
    //

    private float openAmount10;
    [SerializeField]
    private float moveSpeed;

    private Vector3 AOpen,AClosed,BOpen,BClosed;

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
                GameObject sound = Instantiate(openDoor, transform.position, Quaternion.identity);
                Destroy(sound, 1f);
                openAmount10 -= moveSpeed * Time.deltaTime;
            }

            else //close door
            {
                GameObject sound = Instantiate(closeDoor, transform.position, Quaternion.identity);
                Destroy(sound, 1f);
                openAmount10 += moveSpeed * Time.deltaTime;
            }

            openAmount10 = Mathf.Clamp01(openAmount10);

            AOpen = ADoors.transform.localPosition = new Vector3(-openDistance, restingDoorPos.y, restingDoorPos.z);
            AClosed = ADoors.transform.localPosition = restingDoorPos;
            BOpen = BDoors.transform.localPosition = new Vector3(openDistance, restingDoorPos.y, restingDoorPos.z);
            BClosed = BDoors.transform.localPosition = new Vector3(-restingDoorPos.x, restingDoorPos.y, restingDoorPos.z);

            ADoors.transform.localPosition = Vector3.Lerp(AOpen, AClosed, openAmount10);
            BDoors.transform.localPosition = Vector3.Lerp(BOpen, BClosed, openAmount10);
        }
        else if(isComputer)
        {
            if (distToPlayer < activateDistance || nearestEnemy < activateDistance)//close to computer door
            {
                player.GetComponent<PlayerController>().computerDoor = true;
                player.GetComponent<PlayerController>().door = gameObject.GetComponent<DoorsScript>();
                //Debug.Log("close to PC Door");
            }

            else //not close to computer door
            {
                player.GetComponent<PlayerController>().computerDoor = false;
                player.GetComponent<PlayerController>().door = null;
            }
        }
    }

    private void FindEnemiesInScene()
    {
        enemies.Clear();

        BotInfo[] botScripts = FindObjectsOfType<BotInfo>();
        enemies = botScripts.Select(t => t.transform.gameObject).ToList();
    }
}