using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MinigameController miniController;
    private ReadTSV readTSV;

    public DoorsScript door;

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


    public Transform attackPoint;

    public float modifyBulletSpeed = 0;
    //public GameObject bulletPrefab;

    public GameObject enemyControlled;

    public float controlTimer = 0;
    private bool isControlling = false;
    private bool canShoot = false;

    public bool computerDoor = false;
    public bool elevatorDoor = false;

    //visual player representation will be changed when using sprites
    private Color playerColor;

    //maybe be unesscary see what comes from development
    //private List<Ability> abilities;

    private void Start()
    {
        //controller set up
        miniController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        
        mainCamera = Camera.main;
        rBody = GetComponent<Rigidbody>();
        playerColor = body.GetComponent<Renderer>().material.color;

        readTSV = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
    }
    
    void Update()
    {
        Movement();
        PlayerShooting();
        TakeControl();
        ControllingTimer();
        DoorInteract();
    }
    void FixedUpdate()
    {
        rBody.MovePosition(rBody.position + velocity * Time.deltaTime);
        //this.transform.position += velocity * Time.deltaTime;
        //this.transform.Translate(velocity * Time.deltaTime);
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
            miniController.completedQuiz = false;
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
        if (isBehindEnemy && Input.GetKeyDown(KeyCode.E))
        {
            miniController.StartQuiz(1);    
        }

        if (miniController.completedQuiz && enemyControlled != null && readTSV.hackSuccessful == true)
        {
            gameObject.transform.position = new Vector3(enemyControlled.transform.position.x, gameObject.transform.position.y, enemyControlled.transform.position.z);
            threatLevel = enemyControlled.GetComponent<BotInfo>().bThreatLevel;
            body.GetComponent<Renderer>().material.color = enemyControlled.transform.GetChild(0).Find("Capsule").GetComponent<Renderer>().material.color;
            visuals.GetComponent<Renderer>().material.color = enemyControlled.transform.GetChild(0).Find("Forward").GetComponent<Renderer>().material.color;
            if (shooting == null)
            {
                shooting = gameObject.AddComponent<Shooting>();
                shooting.SetHost(visuals);
                shooting.bulletSpeed = modifyBulletSpeed;
                canShoot = true;
            }

            controlTimer = 10.0f;
            isBehindEnemy = false;
            isControlling = true;
            Destroy(enemyControlled);
            //enemyControlled.SetActive(enemyControlled);
            /*switch (threatLevel)
            {
                //change these values when designers pull their finger out

                case 0:
                    controlTimer = 10.0f;
                    break;
                case 1:
                    controlTimer = 10.0f;
                    break;
                case 2:
                    controlTimer = 10.0f;
                    break;
                case 3:
                    controlTimer = 5.0f;
                    break;


            }*/

            /*//goes through abilities of each robot and adds them to character usiung switch statement. this needs to b eadded to another list so they can be removed after timer is up on controlling robots
            foreach (Component t in enemyControlled.GetComponent<BotInfo>().bAbilitiesList)
            {
                switch (t.GetType().ToString())
                {
                    case "Shooting":
                        

                }
            }*/
            //enemyControlled.SetActive(enemyControlled.GetComponent<Collider>());

            readTSV.hackSuccessful = false;
        }
    }
    private void DoorInteract()
    {
        if(computerDoor && !isBehindEnemy && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("computer door");
            //activate computer door minigame
            miniController.StartDoorMinigame();
            //door.isComputer = false;
        }
    }
}
