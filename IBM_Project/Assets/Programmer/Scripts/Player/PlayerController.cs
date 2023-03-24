using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private MinigameController miniController;
    private GameController gc;
    private ReadTSV readTSV;

    [SerializeField]
    private AudioSource startHack;
    [SerializeField]
    private AudioSource winHack;
    [SerializeField]
    private AudioSource loseHack;
    [SerializeField]
    private AudioSource breakBox;

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
    public Animator animator;
    public Animator animator2;
    public Animator animator3;

    private void Start()
    {
        //controller set up
        miniController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rBody = GetComponent<Rigidbody>();
        //playerColor = body.GetComponent<Renderer>().material.color;

        readTSV = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
    }

    void Update()
    {
        if (miniController.completedQuiz)
        {
            gc.Deactivate = false;
        }

        if (!gc.Deactivate)
        {
            Movement();
            PlayerShooting();
            Interact();
            ControllingTimer();
            DoorInteract();
            DetectEnemy();

            animator.SetFloat("Horizontal", velocity.x);
            animator.SetFloat("Vertical", velocity.z);
            animator.SetFloat("Speed", velocity.sqrMagnitude);
            animator2.SetFloat("Horizontal", velocity.x);
            animator2.SetFloat("Vertical", velocity.z);
            animator2.SetFloat("Speed", velocity.sqrMagnitude);
            animator3.SetFloat("Horizontal", velocity.x);
            animator3.SetFloat("Vertical", velocity.z);
            animator3.SetFloat("Speed", velocity.sqrMagnitude);
        }
        else
        {
            velocity = new Vector3(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        rBody.MovePosition(rBody.position + velocity * Time.deltaTime);
        //this.transform.position += velocity * Time.deltaTime;
        //this.transform.Translate(velocity * Time.deltaTime);
    }

    private void Movement()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
        mainCamera.transform.position.y));
        visuals.transform.LookAt(mousePos + Vector3.up * transform.position.y);
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
        if (isControlling)
        {
            miniController.completedQuiz = false;
            controlTimer -= Time.deltaTime;
            if (controlTimer <= 0)
            {
                GameObject tempBody;
                switch (threatLevel)
                {
                    case 2:
                    {
                        Debug.Log("Solider_Death");
                        tempBody = Resources.Load<GameObject>("Soldier_Death_Place");
                        Instantiate(tempBody, transform.position, transform.parent.GetChild(2).rotation);
                        break;
                    }
                    case 3:
                    {
                        Debug.Log("Scout_Death");
                        tempBody = Resources.Load<GameObject>("Scout_Death_Place");
                        Instantiate(tempBody, transform.position, transform.parent.GetChild(2).rotation);
                        break;
                    }
                }
                threatLevel = 0;
                controlTimer = 0;
                gameObject.GetComponent<Shooting>().enabled = false;
                canShoot = false;
                //abilities.Clear();
            }
        }
    }

    private void Interact()
    {
        bool playOnce = false;
        if (isBehindEnemy && Input.GetKeyDown(KeyCode.E))
        {
            miniController.StartQuiz(1);
            startHack.Play();
            gc.Deactivate = true;
        }
        else if (!isBehindEnemy && Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit objectHit;
            if (Physics.Raycast(visuals.transform.position, visuals.transform.forward, out objectHit, 5))
            {
                if (objectHit.collider.CompareTag("BreakableBox"))
                {
                    bool breakBoxOnce = false;
                    if (!breakBoxOnce)
                    {
                        breakBox.Play();
                        breakBoxOnce = true;
                    }
                    Destroy(objectHit.transform.gameObject);
                }
            }
        }

        if (miniController.completedQuiz && enemyControlled != null && readTSV.hackSuccessful == true)
        {
            bool playwinOnce = false;
            if (!playwinOnce)
            {
                winHack.Play();
                playwinOnce = true;
            }
            gameObject.transform.position = new Vector3(enemyControlled.transform.position.x,
                gameObject.transform.position.y, enemyControlled.transform.position.z);
            threatLevel = enemyControlled.GetComponent<BotInfo>().bThreatLevel;
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
            Destroy(enemyControlled.transform.parent.gameObject);
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
        else if(miniController.completedQuiz && enemyControlled != null && readTSV.hackSuccessful == false)
        {
            if (!playOnce)
            {
                //loseHack.Play();
                playOnce = true;
            }
        }
    }

    private void DoorInteract()
    {
        if (computerDoor && Input.GetKeyDown(KeyCode.E) && !miniController.completedDoor)
        {
            //activate computer door minigame
            miniController.StartDoorMinigame();
        }
        else if (miniController.completedDoor)
        {
            door.isComputer = false;
        }
    }

    private void DetectEnemy()
    {
        if (gc.bots.Length <= 0) return;
        foreach (GameObject bot in gc.bots)
        {
            if (bot)
            {
                Vector3 enemyPos = bot.transform.GetChild(0).position;
                Vector3 playerPos = transform.position;
                float range = Vector3.Distance(enemyPos, playerPos);
                Vector3 toTarget = enemyPos - playerPos;
                Vector3 dirToTarget = toTarget.normalized;

                if ((range < transform.gameObject.GetComponent<FieldOfView>().viewRadius - 0.05f &&
                     Vector3.Angle(transform.forward, dirToTarget) <
                     transform.gameObject.GetComponent<FieldOfView>().viewAngle / 2)
                    && !Physics.Raycast(transform.position, dirToTarget, toTarget.magnitude,
                        transform.gameObject.GetComponent<FieldOfView>().obstacleMask))
                {
                    bot.transform.GetChild(0).GetComponent<BotInfo>().bInPlayerView = true;
                    bot.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    bot.transform.GetChild(0).GetComponent<BotInfo>().bInPlayerView = false;
                    bot.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }
}
