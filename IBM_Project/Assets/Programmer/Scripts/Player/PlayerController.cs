using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;
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

    public float controlTimer = 0;
    private bool isControlling = false;
    private bool canShoot = false;

    //visual player representation will be changed when using sprites
    private Color playerColor;


    private List<Ability> abilities;

    private void Start()
    {
        //controller set up
        mainCamera = Camera.main;
        rBody = GetComponent<Rigidbody>();
        playerColor = body.GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        Movement();
        PlayerShooting();
        //Melee();
        TakeControl();
        ControllingTimer();
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
    private void ControllingTimer()
    {
        if(isControlling)
        {
            controlTimer -= Time.deltaTime;
            if(controlTimer <= 0)
            {
                body.GetComponent<Renderer>().material.color = playerColor;
                visuals.GetComponent<Renderer>().material.color = playerColor;
                controlTimer = 0;
                gameObject.GetComponent<Shooting>().enabled = false;
                canShoot = false;
                //abilities.Clear();
            }
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
            switch(threatLevel)
            {
                //change these values when designers pull their finger out
                
                case 0:
                    controlTimer = 0.0f;
                    break;
                case 1:
                    controlTimer = 0.0f;
                    break;
                case 2:
                    controlTimer = 0.0f;
                    break;
                case 3:
                    controlTimer = 5.0f;
                    break;
                
                
            }

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
                        //abilities.Add(shooting);
                        break;

                }
            }
            
            Destroy(enemyControlled);
            isBehindEnemy = false;
            isControlling = true;
        }
    }
}
