using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
//sing UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public GameObject visuals;
    public GameObject body;

    private Camera mainCamera;

    public bool isBehindEnemy;

    private Rigidbody rBody;
    public Vector3 velocity;

    public int threatLevel;

    //variables for shooting, only placed in here to test how dynamic functions are.
    private Shooting shooting;
/*    private Melee melee;
    private LayerMask enemyLayer;*/

    public Transform attackPoint;

    public float modifyBulletSpeed = 0;
    //public GameObject bulletPrefab;

    public GameObject enemyControlled;

    private bool canShoot = false;


    private void Start()
    {
        //controller set up
        mainCamera = Camera.main;
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        PlayerShooting();
        //Melee();
        TakeControl();
    }
    void FixedUpdate()
    {
        rBody.MovePosition(rBody.position + velocity * Time.deltaTime);
    }

    private void Movement()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed;
        mainCamera.transform.position = new Vector3(transform.position.x, 5, transform.position.z);

    }
    private void PlayerShooting()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            shooting.Execute();
        }
    }

    private void TakeControl()
    {
        if (!isBehindEnemy) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            this.gameObject.transform.position = new Vector3(enemyControlled.transform.position.x, 0, enemyControlled.transform.position.z);
            threatLevel = enemyControlled.GetComponent<BotInfo>().threatLevel;
            body.GetComponent<Renderer>().material.color = enemyControlled.transform.Find("Capsule").GetComponent<Renderer>().material.color;
            visuals.GetComponent<Renderer>().material.color = enemyControlled.transform.Find("Forward").GetComponent<Renderer>().material.color;
            
            //goes through abilities of each robot and adds them to character usiung switch statement. this needs to b eadded to another list so they can be removed after timer is up on controlling robots
            foreach(Component t in enemyControlled.GetComponent<BotInfo>().abilitiesList)
            {
                switch(t.GetType().ToString())
                {
                    case "Shooting":
                        shooting = this.gameObject.AddComponent<Shooting>();
                        shooting.SetHost(visuals);
                        shooting.bulletSpeed = modifyBulletSpeed;
                        canShoot = true;
                        break;

                }
            }

            Destroy(enemyControlled);
            isBehindEnemy = false;
        }
    }
}
