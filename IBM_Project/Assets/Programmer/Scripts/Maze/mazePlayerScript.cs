using TMPro;
using UnityEngine;

public class MazePlayerScript : MonoBehaviour
{
    public float speed;
    public bool returnToStartUponCollision;
    bool touchWall;
    public TextMeshProUGUI timer;
    [SerializeField]
    private AudioSource wallHitSound;
    bool mazeReadyPlayer;
    private GameController gameController;
    public int timesHit;
    private int prevTH; //Previous timesHit


    private float debugfloat;

    private void OnEnable()
    {
        //walGen.OnMazeReady += applyReady;
        //gameController.loseSoundPlayed = false;
    }

    private void OnDisable()
    {
        //walGen.OnMazeReady -= applyReady;
    }

    void applyReady(bool mazeReady)
    {
        //mazeReadyPlayer = mazeReady;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mazeWall")
        {
            //Debug.Log("Trigger Wall hit");
            if (returnToStartUponCollision == true) transform.position = new Vector3(84, 0, 82);//(2, 0, 0);
            touchWall = true;
            //Debug.Log("debugfloat: " + debugfloat);
            touchWall = false;
            wallHitSound.Play();
            //timesHit++;
            timesHit = prevTH + 1;
            //StartCoroutine(HitDelay());

        }
        else if (other.gameObject.tag == "goalLocation")
        {
            //Debug.Log("Maze win");

            //Setting gameobjects invisible:
            GameObject.FindGameObjectWithTag("goalLocation").GetComponent<MeshRenderer>().enabled = false;
            GameObject.FindGameObjectWithTag("mazePlayer").GetComponent<MeshRenderer>().enabled = false;
            //Timer.GetComponent<TextMeshProUGUI>().enabled = false;
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            //gameController.winSoundPlayed = false;
            //gameController.loseSoundPlayed = false;

            //Method transforming gameobjects to different location:

            //GameObject.FindGameObjectWithTag("goalLocation").transform.position = new Vector3(-100,100,-100);
            //GameObject.FindGameObjectWithTag("mazePlayer").transform.position = new Vector3(-100, 80, -100);



            //Method deactivating gameobjects:

            //GameObject.FindWithTag("goalLocation").SetActive(false);
            //GameObject.FindWithTag("mazePlayer").SetActive(false);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "mazeWall")
        {
            //Debug.Log("Trigger Wall exit");
            //transform.position = new Vector3(2, 0, 0);
        }
    }

    //IEnumerator HitDelay()
    //{
    //    //timesHit++;
    //    yield return new WaitForSeconds(0.035f);
    //    if (timesHit > (prevTH + 1))
    //    {
    //        timesHit = prevTH + 1;
    //    }
    //    prevTH++;
    //}

    // Start is called before the first frame update
    void Start()
    {
        timesHit = 0;
        prevTH = 0;
        mazeReadyPlayer = false;
        gameObject.GetComponent<Renderer>().material.color = Color.white; //Setting colour of Player gameobject
        GameObject goalLocation = GameObject.FindGameObjectWithTag("goalLocation");
        goalLocation.GetComponent<Renderer>().material.color = Color.green;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //gameController.failMinigame = false;
        debugfloat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("MazePlayer timesHit: " + timesHit);




        debugfloat += Time.deltaTime;
        //debugfloat = (int)debugfloat;

        if (debugfloat > 0.035f)
        {
            debugfloat = 0;
            prevTH = timesHit;
            
        }

        //if (timesHit > (prevTH + 1))
        //{
        //    timesHit = prevTH + 1;
        //}


        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (mazeReadyPlayer)
        {
            if (Input.GetKey("d"))
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                if (touchWall == true)
                {
                    //transform.position -= new Vector3(speed * Time.deltaTime, 0, 0) * 3;
                }
            }
            if (Input.GetKey("w"))
            {
                transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                if (touchWall == true)
                {
                    //transform.position -= new Vector3(0, 0, speed * Time.deltaTime) * 3;
                }
            }
            if (Input.GetKey("a"))
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                if (touchWall == true)
                {
                    //transform.position += new Vector3(speed * Time.deltaTime, 0, 0) * 3;
                }
            }
            if (Input.GetKey("s"))
            {
                transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
                if (touchWall == true)
                {
                    //transform.position += new Vector3(0, 0, speed * Time.deltaTime) * 3;
                }
            }

           /* if (Input.GetKeyDown("p"))
            {
                transform.position = GameObject.FindGameObjectWithTag("goalLocation").transform.position;
            }*/
        }
        if (Input.GetKeyDown("space"))// && mazeReadyPlayer == true)
        {
            mazeReadyPlayer = true;
            //transform.position = new Vector3(62, 0, 60);
            transform.position = new Vector3(84, 0, 82);
            //returnToStart(true);
        }

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    GameObject.FindGameObjectWithTag("goalLocation").GetComponent<MeshRenderer>().enabled = false;
        //    GameObject.FindGameObjectWithTag("mazePlayer").GetComponent<MeshRenderer>().enabled = false;
        //    //Timer.GetComponent<TextMeshProUGUI>().enabled = false;
        //    gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        //}


        //Attempted to convert input to a switch case, didn't turnout as smooth
        //var mazeInput = Input.inputString;
        //switch (mazeInput)
        //{
        //    case "d":
        //        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        //        if (touchWall == true)
        //        {
        //            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0) * 3;
        //        }
        //        break;
        //    case "w":
        //        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        //        if (touchWall == true)
        //        {
        //            transform.position -= new Vector3(0, 0, speed * Time.deltaTime) * 3;
        //        }
        //        break;
        //    case "a":
        //        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        //        if (touchWall == true)
        //        {
        //            transform.position += new Vector3(speed * Time.deltaTime, 0, 0) * 3;
        //        }
        //        break;
        //    case "s":
        //        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        //        if (touchWall == true)
        //        {
        //            transform.position += new Vector3(0, 0, speed * Time.deltaTime) * 3;
        //        }
        //        break;
        //    case "space":
        //        transform.position = new Vector3(2, 0, 0);
        //        break;
        //}


    }
}
