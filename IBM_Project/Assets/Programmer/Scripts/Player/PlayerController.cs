using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private MinigameController miniController;
    private GameController gc;
    private ReadTSV readTSV;

    [SerializeField]
    private AudioSource winHack;
    [SerializeField]
    private AudioSource loseHack;
    [SerializeField]
    private AudioSource breakBox;
    [SerializeField]
    private AudioSource botShutdown;

    public DoorsScript door;

    public float speed;
    private float speedOrigin;
    public bool canSpeed;
    
    public GameObject visuals;
    public GameObject body;

    private ComputerInteraction PC;

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
    public bool canShoot = false;

    public bool computerDoor = false;
    public bool elevatorDoor = false;

    //visual player representation will be changed when using sprites
    private Color playerColor;

    //maybe be unesscary see what comes from development
    //private List<Ability> abilities;
    public Animator animator;
    public Animator animator2;
    public Animator animator3;

    public bool failedHack;
    private bool loseSoundPlayed = false;
    private bool shutdownPlayed = false;
    private void Start()
    {
        //controller set up
        miniController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MinigameController>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        PC = GameObject.FindGameObjectWithTag("Computer").GetComponent<ComputerInteraction>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rBody = GetComponent<Rigidbody>();
        //playerColor = body.GetComponent<Renderer>().material.color;

        readTSV = GameObject.FindGameObjectWithTag("QuizMaster").GetComponent<ReadTSV>();
        speedOrigin = speed;
        canSpeed = false;
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
                        tempBody = Resources.Load<GameObject>("Soldier_Death_Sprite_Holder");
                        Instantiate(tempBody, transform.position, transform.parent.GetChild(2).rotation);
                        if (!shutdownPlayed)
                        {
                            botShutdown.Play();
                            shutdownPlayed = true;
                        }
                        break;
                    }
                    case 3:
                    {
                        tempBody = Resources.Load<GameObject>("Scout_Death_Sprite_Holder");
                        Instantiate(tempBody, transform.position, transform.parent.GetChild(2).rotation);
                        if (!shutdownPlayed)
                        {
                            botShutdown.Play();
                            shutdownPlayed = true;
                        }
                        break;
                    }
                }
                threatLevel = 0;
                controlTimer = 0;
                if(gameObject.GetComponent<Shooting>())
                    gameObject.GetComponent<Shooting>().enabled = false;
                canShoot = false;
                canSpeed = false;
                speed = speedOrigin;
                //abilities.Clear();
            }
        }
    }

    private void Interact()
    {

        if (isBehindEnemy && Input.GetKeyDown(KeyCode.E) && gc.PlayerStatus != GameController.Status.HUNTED && Time.timeScale != 0)
        {
            loseSoundPlayed = false;
            miniController.StartQuiz(1);
            gc.Deactivate = true;
        }
        else if (!isBehindEnemy && Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0)
        {
            RaycastHit objectHit;
            if (Physics.Raycast(visuals.transform.position, visuals.transform.forward, out objectHit, 1))
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
            
            if (threatLevel != 3)
            {
                if (shooting == null)
                {
                    shooting = gameObject.AddComponent<Shooting>();
                    shooting.SetHost(visuals);
                    shooting.bulletSpeed = modifyBulletSpeed;
                }
                canShoot = true;
                controlTimer = 10.0f;
                isBehindEnemy = false;
                isControlling = true;
                
                enemyControlled.GetComponent<BotInfo>().bIsDead = true;
                enemyControlled.GetComponent<BotInfo>().bWasHacked = true;
            }
            else
            {
                canSpeed = true;
                speed += 1.0f;
                controlTimer = 10.0f;
                isBehindEnemy = false;
                isControlling = true;
                
                enemyControlled.GetComponent<BotInfo>().bIsDead = true;
                enemyControlled.GetComponent<BotInfo>().bWasHacked = true;
            }
            
            readTSV.hackSuccessful = false;
        }
        else if(miniController.completedQuiz && enemyControlled != null && readTSV.hackSuccessful == false)
        {
            if(!loseSoundPlayed)
            {
                failedHack = true;
                loseHack.Play();
                loseSoundPlayed = true;
            }
        }
    }

    private void DoorInteract()
    {
        if (computerDoor && Input.GetKeyDown(KeyCode.E) && !miniController.completedDoor && !gc.failMinigame)
        {
            //activate computer door minigame
            miniController.StartDoorMinigame();
        }
        else if (miniController.completedDoor && !gc.failMinigame)
        {
            door.isComputer = false;
        }
    }

    private void DetectEnemy()
    {
        //if (!PC.mazeDONE)
        //{
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
                        if (!bot.transform.GetChild(0).GetComponent<BotInfo>().bWasHacked)
                        {
                            bot.transform.GetChild(0).GetComponent<BotInfo>().bInPlayerView = true;
                            bot.transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        if (!bot.transform.GetChild(0).GetComponent<BotInfo>().bWasHacked)
                        {
                            bot.transform.GetChild(0).GetComponent<BotInfo>().bInPlayerView = false;
                            bot.transform.GetChild(1).gameObject.SetActive(false);
                        }
                }
                }
            }
            if (gc.deadDroids.Length <= 0) return;
            foreach (GameObject droid in gc.deadDroids)
            {
                if (droid)
                {
                    Vector3 droidPos = droid.transform.position;
                    Vector3 playerPos = transform.position;
                    float range = Vector3.Distance(droidPos, playerPos);
                    Vector3 toTarget = droidPos - playerPos;
                    Vector3 dirToTarget = toTarget.normalized;

                    if ((range < transform.gameObject.GetComponent<FieldOfView>().viewRadius - 0.05f &&
                         Vector3.Angle(transform.forward, dirToTarget) <
                         transform.gameObject.GetComponent<FieldOfView>().viewAngle / 2)
                        && !Physics.Raycast(transform.position, dirToTarget, toTarget.magnitude,
                            transform.gameObject.GetComponent<FieldOfView>().obstacleMask))
                    {
                        droid.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        droid.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        //}
    }
}
