using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.AI;
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



    private void Start()
    {
        //controller set up
        mainCamera = Camera.main;
        rBody = GetComponent<Rigidbody>();

        //jank for now but handled by 'ability listener' class that will determine what abilities and attributes are availbe to the specific actor attached to the class
        shooting = gameObject.AddComponent<Shooting>();
        shooting.SetHost(visuals);
        shooting.bulletSpeed = modifyBulletSpeed;

        

 /*       //jank melee intialisation
        melee = gameObject.AddComponent<Melee>();
        enemyLayer = LayerMask.GetMask("Enemy");
        melee.tempAtkPoint = attackPoint;
 */
    }

    void Update()
    {
        Movement();
        Shooting();
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
    private void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("trying to shoot");
            shooting.Execute();
        }
    }
/*    private void Melee()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("trying to melee");
            melee.Execute(enemyLayer);
        }
    }*/
    private void TakeControl()
    {
        if (!isBehindEnemy) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            this.gameObject.transform.position = new Vector3(enemyControlled.transform.position.x, 0, enemyControlled.transform.position.z);
            threatLevel = enemyControlled.GetComponent<BotInfo>().threatLevel;
            body.GetComponent<Renderer>().material.color = enemyControlled.transform.Find("Capsule").GetComponent<Renderer>().material.color;
            visuals.GetComponent<Renderer>().material.color = enemyControlled.transform.Find("Forward").GetComponent<Renderer>().material.color;
            //body.GetComponent<MeshRenderer>().material.SetColor("_color", Color.red);
            //visuals.GetComponent<MeshRenderer>().material.SetColor("_color", Color.red);
            //speed = enemyControlled.GetComponent<BotInfo>().speed;
            Destroy(enemyControlled);
            isBehindEnemy = false;
        }
    }
}
