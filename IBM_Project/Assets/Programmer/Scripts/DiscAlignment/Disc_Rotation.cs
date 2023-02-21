using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



//Left for Alignment minigame:
//Implement into and push git project
//Implement Lewis' Timer (via inheritance?)
//*Modify code to allow easier expansion (more or less discs can be easily implemented, preferably by setting public variable(s)
//Model Cynder objects with highlighted paths (e.g. Blender, 3DS Max etc) and implement them into Unity Project (as .obj files)
    //*Modify rotation check to allow any 'location' for the paths to be aligned at (alternatively, could create a center disc which dosen't rotate, thus requiring a specific rotation)
//*Changes to how discs affect eachother? (e.g. moving center disc will rotate outline disc in opposite direction? - Discs alternate in rotation when a disc down their hirarchy is being rotated)
                //- might be difficult to implement while allowing for expansion, considering as stetch goal
//Anything else I think of or other members of team (e.g. Designers) believe is worth implementing

//* = might take more time to implement


public class Disc_Rotation : MonoBehaviour
{
    public short ID; //1 for red (outline) 2 for green (middle) 3 for white (center)
    public float rotationSpeed;
    private short currentSelect;
    private bool Selected;
    private bool[] numAligned;
    private bool debugWin;

    GameObject Disc1; //Red (outline)
    GameObject Disc2; //green (middle)
    GameObject Disc3; //White (center)

    //Gameobjects used to check alignment. Moves position when a combonation of discs are aligned.
    GameObject R1;
    GameObject R2;
    GameObject R3;

    Quaternion Disc1startRotation;
    Quaternion Disc2startRotation;
    Quaternion Disc3startRotation;

    Vector3 R1startPosition;
    Vector3 R1startRotation;

    public GameObject pregameText;
    public bool showTutorial;

    // Start is called before the first frame update

    Vector3 RotationToDegrees(Vector3 v3)
    {
        Vector3 rv = new Vector3(1, 1, 1);
        rv.x = v3.x * Mathf.Rad2Deg;
        rv.y = v3.y * Mathf.Rad2Deg;
        rv.z = v3.z * Mathf.Rad2Deg;
        return rv;
    }

    void Start()
    {
        

        currentSelect = 1;
        numAligned = new bool[3];
        Disc1 = GameObject.Find("Disc1n");
        Disc2 = GameObject.Find("Disc2n");
        Disc3 = GameObject.Find("Disc3n");


        numAligned[0] = true;
        numAligned[1] = true;
        numAligned[2] = true;

        R1 = GameObject.Find("R1");
        R2 = GameObject.Find("R2");
        R3 = GameObject.Find("R3");

        R1.transform.position = new Vector3(-5, 15, 0);
        R2.transform.position = new Vector3(0, 15, 0);
        R3.transform.position = new Vector3(5, 15, 0);

        Disc1startRotation = Disc1.transform.rotation;
        Disc2startRotation = Disc2.transform.rotation;
        Disc3startRotation = Disc3.transform.rotation;

        GameObject.Find("TutorialBackground").GetComponent<MeshRenderer>().enabled = showTutorial;
        pregameText.GetComponent<TextMeshProUGUI>().enabled = showTutorial;

        debugWin = false;

        //R1startPosition = R1.transform.position;
        //R1startRotation = R1.transform.eulerAngles;
        //Debug.Log(R1startPosition + "," + R1startRotation);

    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Renderer>().material.color = Color.grey;
        if (currentSelect >= ID)
        {
            Selected = true;
            //GetComponent<Renderer>().material.color = Color.black;
            ////////Debug.Log("Disc1: " + (int)Disc1.transform.rotation.eulerAngles.x + " | Disc2: " + (int)Disc2.transform.rotation.eulerAngles.x + " | Disc3: " + (int)Disc3.transform.rotation.eulerAngles.x + " | currentSelect: " + currentSelect);

            //Only for debugging purposes before implementing custom game objects
            //My aim is to make this system easily expandable if more/less discs were to be added
            switch (currentSelect)
            {
                case 1:
                    Disc2.GetComponent<Renderer>().material.color = new Color(0.5f, 1.0f, 0.5f);
                    Disc3.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
                    Disc1.GetComponent<Renderer>().material.color = Color.black;
                    //Debug.Log(currentSelect + " 1~ " + transform.rotation.eulerAngles.x + " 2~ " + transform.rotation.eulerAngles.x + " 3~ " + transform.rotation.eulerAngles.x);
                    break;
                case 2:
                    Disc1.GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0.5f);
                    Disc3.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
                    Disc2.GetComponent<Renderer>().material.color = Color.black;
                    //GetComponent<Renderer>().material.color = new Color(0.5f, 1.0f, 0.5f);
                    //Debug.Log(currentSelect + " 1~ " + transform.rotation.eulerAngles.x + " 2~ " + transform.rotation.eulerAngles.x + " 3~ " + transform.rotation.eulerAngles.x);
                    break;
                case 3:
                    //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
                    Disc1.GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0.5f);
                    Disc2.GetComponent<Renderer>().material.color = new Color(0.5f, 1.0f, 0.5f);
                    Disc3.GetComponent<Renderer>().material.color = Color.black;
                    //Debug.Log(currentSelect + " 1~ " + transform.rotation.eulerAngles.x + " 2~ " + transform.rotation.eulerAngles.x + " 3~ " + transform.rotation.eulerAngles.x);
                    break;
            }

            //switch (currentSelect)
            //{
            //    case 1:
            //        Disc1.GetComponent<Renderer>().material.color = Color.black;
            //        break;
            //    case 2:
            //        Disc2.GetComponent<Renderer>().material.color = Color.black;
            //        break;
            //    case 3:
            //        Disc3.GetComponent<Renderer>().material.color = Color.black;
            //        break;
            //}
            //Debug.Log(currentSelect + " | " + transform.rotation.eulerAngles + " | " + numAligned[0] + "," + numAligned[1] + "," + numAligned[2]);
            //Debug.Log(transform.rotation.eulerAngles.x + " " + transform.rotation.eulerAngles.y + " " + transform.rotation.eulerAngles.z);
        }
        else
        {
            Selected = false;
            //switch (ID)
            //{
            //    case 1:
            //        GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0.5f);
            //        //Debug.Log(currentSelect + " 1~ " + transform.rotation.eulerAngles.x + " 2~ " + transform.rotation.eulerAngles.x + " 3~ " + transform.rotation.eulerAngles.x);
            //        break;
            //    case 2:
            //        GetComponent<Renderer>().material.color = new Color(0.5f, 1.0f, 0.5f);
            //        //Debug.Log(currentSelect + " 1~ " + transform.rotation.eulerAngles.x + " 2~ " + transform.rotation.eulerAngles.x + " 3~ " + transform.rotation.eulerAngles.x);
            //        break;
            //    case 3:
            //        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
            //        //Debug.Log(currentSelect + " 1~ " + transform.rotation.eulerAngles.x + " 2~ " + transform.rotation.eulerAngles.x + " 3~ " + transform.rotation.eulerAngles.x);
            //        break;
            //}
        }

        //Changed rotation input from Q and E to A and D to keep consistency with player controls
        if (Input.GetKey(KeyCode.D) && Selected == true)
        {

            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            //if (currentSelect == 2) { ... }
        }

        if (Input.GetKey(KeyCode.A) && Selected == true)
        {

            transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKeyDown(KeyCode.S) && currentSelect != 3)
        {
            currentSelect += 1;
        }   
        if (Input.GetKeyDown(KeyCode.W) && currentSelect != 1)
        {
            currentSelect -= 1;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Debug.Log("R Pressed");
            //R1.transform.position = R1startPosition;
            //R1.transform.eulerAngles = R1startRotation;


            Debug.Log("R1.transform.rotation | startRotationR1 " + Disc1.transform.rotation + " | " + Disc1startRotation);
            //transform.rotation = Quaternion.Euler(0, -90, 90); //Default rotation: 0, -90, 90
            Disc1.transform.rotation = Disc1startRotation;
            Disc2.transform.rotation = Disc2startRotation;
            Disc3.transform.rotation = Disc3startRotation;
            Debug.Log("[Updated] R1.transform.rotation | startRotationR1 " + Disc1.transform.rotation + " | " + Disc1startRotation);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("TutorialBackground").GetComponent<MeshRenderer>().enabled = false;
            pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
        }

        //Testing example, to be set when paths align
        //1 - 85 to 105
        //2 - 80 to 110
        //3 - 70 to 120
        //if (transform.rotation.eulerAngles.x >= 85.0f && transform.rotation.eulerAngles.x <= 105.0f && ID == 1)
        //If ID1.transform.rotation == ID2.transform.rotation with 10 degree leniance (5 degrees for going over or under target rotation)
        //Disc1 and Disc2
        if ((Disc1.transform.rotation.eulerAngles.x >= Disc2.transform.rotation.eulerAngles.x - 10.0f && Disc1.transform.rotation.eulerAngles.x <= Disc2.transform.rotation.eulerAngles.x + 10.0f) || debugWin == true)
        {
            numAligned[ID - 1] = true; //numAligned[0] = true
            Debug.Log((int)Disc1.transform.rotation.eulerAngles.x + " , " + (int)Disc2.transform.rotation.eulerAngles.x + " [Disc 1 is in target position]");
            R1.transform.position = new Vector3(-5, 10, 0);
        }
        else
        {
            if (ID == 1) { numAligned[ID - 1] = false; R1.transform.position = new Vector3(-5, 15, 0); Debug.Log((int)Disc1.transform.rotation.eulerAngles.x + " , " + (int)Disc2.transform.rotation.eulerAngles.x);  }
        }

        //if (transform.rotation.eulerAngles.x >= 80.0f && transform.rotation.eulerAngles.x <= 110.0f && ID == 2)
        //Disc1 and Disc3
        if ((Disc1.transform.rotation.eulerAngles.x >= Disc3.transform.rotation.eulerAngles.x - 10.0f && Disc1.transform.rotation.eulerAngles.x <= Disc3.transform.rotation.eulerAngles.x + 10.0f) || debugWin == true)
        {
            numAligned[ID - 1] = true; //numAligned[1] = true
            Debug.Log("Disc 2 is in target position");
            R2.transform.position = new Vector3(0, 10, 0);
        }
        else
        {
            if (ID == 2) { numAligned[ID - 1] = false; R2.transform.position = new Vector3(0, 15, 0); }
        }
        //Disc2 and Disc3
        //if (transform.rotation.eulerAngles.x >= 70.0f && transform.rotation.eulerAngles.x <= 120.0f && ID == 3)
        if ((Disc2.transform.rotation.eulerAngles.x >= Disc3.transform.rotation.eulerAngles.x - 10.0f && Disc2.transform.rotation.eulerAngles.x <= Disc3.transform.rotation.eulerAngles.x + 10.0f) || debugWin == true)
        {
            numAligned[ID - 1] = true; //numAligned[2] = true
            Debug.Log("Disc 3 is in target position");
            R3.transform.position = new Vector3(5, 10, 0);
        }
        else
        {
            if (ID == 3) { numAligned[ID - 1] = false; R3.transform.position = new Vector3(5, 15, 0); }
        }
        //Debug.Log(currentSelect);

        if (R1.transform.position == new Vector3(-5,10,0) && R2.transform.position == new Vector3(0, 10, 0) && R3.transform.position == new Vector3(5, 10, 0))// || debugWin == true)
        {
            Debug.Log("You Win!");
        }

        if (Input.GetKeyDown("p"))
        {
            R1.transform.position = new Vector3(-5, 10, 0);
            R2.transform.position = new Vector3(0, 10, 0);
            R3.transform.position = new Vector3(5, 10, 0);
            //Disc1 = Disc 3; Disc2 = Disc3

            debugWin = true;
        }

        //GetComponent<Renderer>().material.color = Color.grey;
    }
}
