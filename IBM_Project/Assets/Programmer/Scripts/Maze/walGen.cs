using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class walGen : MonoBehaviour
{
    public int frameRate;


    public int Maze_Width; //Designer input to set maze Width (up to 100?)
    public int Maze_Height; //Designer input to set maze Height (up to 100?)
    Vector2 Maze_Size; //Maze_Size.x, Maze_Size.y, uses Maze_Width and Maze_Height
    bool[,] mazeGrid; //
    bool mazeReady; //Indicates when maze has finished generating

    //Positions represented in Unity World Space
    //public GameObject currentPos;



    public GameObject wallDestroyer; //Wall destroyer



    GameObject mazePlayer; //Gameobject of player avatar for maze
    GameObject goalLocation; //Gameobject of goal location
    public GameObject wallPrefab; //Reference gameobject for walls of maze

    public GameObject targetPosWorld;
    public GameObject currentPosWorld;
    Vector2 targetPosGrid;
    Vector2 currentPosGrid;

    //UI
    public TextMeshProUGUI pregameText;
    public TextMeshProUGUI ingameText;
    public TextMeshProUGUI Timer;
    public bool showIngameText;

    //Positions of Gameobjects represented in bool grid space
    //private int currentGridPos;
    //private int targetGridPos;

    //Converting between GridSpace and WorldSpace
    //WorldSpace -> GridSpace: (WorldSpace - 2x) / 4
    //GridSpace -> WorldSpace: (GridSpace * 4) + 2x




    void genGrid(Vector3 Origin, Vector2 ab) //Generate Maze cell by instantiating 4 walls
    {
        //Debug.Log(Origin.x + "," + Origin.y + "," + Origin.z);
        Instantiate(wallPrefab, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z + 2.0f), Quaternion.identity);
        Instantiate(wallPrefab, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z - 2.0f), Quaternion.identity);
        Instantiate(wallPrefab, new Vector3(Origin.x, Origin.y, Origin.z), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        Instantiate(wallPrefab, new Vector3(Origin.x + 4.0f, Origin.y, Origin.z), Quaternion.Euler(0.0f, 90.0f, 0.0f));
    }

    Vector2 convertToGrid(Vector3 v3) //WorldSpace -> GridSpace
    {
        v3 -= new Vector3(-2, 0, 0);
        v3 *= 4;

        return new Vector2(v3.x, v3.z);
    }

    Vector3 convertToWorld(Vector2 v2) //GridSpace -> WorldSpace
    {
        Vector3 rv = new Vector3(v2.x, 0, v2.y);
        rv *= 4;
        rv += new Vector3(2, 0, 0);
        Debug.Log(v2 + "," + rv);
        return rv;
    }
    Vector2 gridMovement(Vector2 cgp) //Moves currentposGrid to an adjacent unvistied cell if viable (backtracks using stack if isn't)
                                      //cgp = current grip pos
    {
        int[] array1 = new int[] { 0, 0, 0, 0, 0 }; //Array is used to hold which cells are unvisited to use for rng.
        //north, south, east, west, none


        //This checks what adjacent cells are unvisited.
        //Area of refinement: convert to switch case?
        if (mazeGrid[(int)cgp.x, (int)cgp.y + 1] == false)
        {
            //north is viable
            array1[0] = 1;

        }
        if (mazeGrid[(int)cgp.x, (int)cgp.y - 1] == false)
        {
            //south is viable
            array1[1] = 2;
        }
        if (mazeGrid[(int)cgp.x + 1, (int)cgp.y] == false)
        {
            //east is viable
            array1[2] = 3;
        }
        if (mazeGrid[(int)cgp.x - 1, (int)cgp.y] == false)
        {
            //west is viable
            array1[3] = 4;
        }

        //Area of refinement: check if all values of array = 0
        if (array1[0] == 0 && array1[1] == 0 && array1[2] == 0 && array1[3] == 0)
        {
            array1[4] = 5;
        }

        int rng = 0;

        while (rng == 0)
        {
            Debug.Log("Finding value amoung array. rng = " + array1);
            rng = array1[Random.Range(1, 5)];
            if (array1[4] == 5)
            {
                rng = 5;
            }
            //should keep looping until a value different from 0 is found
        }
        switch (rng)
        {
            case 1:
                cgp.x += 1;
                //add position to the stack
                break;
            case 2:
                cgp.x -= 1;
                //add position to the stack
                break;
            case 3:
                cgp.y += 1;
                //add position to the stack
                break;
            case 4:
                cgp.y -= 1;
                //add position to the stack
                break;
            case 5:
                //backtrack using the stack
                break;
        }
        return cgp;
    }



    //'Direction' of the maze generation can be influenced using moveGen

    void moveGen3(short dir)
    {
        switch (dir)
        {
            case 1:
                wallDestroyer.transform.position += new Vector3(0, 0, 4);
                break;
            case 2:
                wallDestroyer.transform.position += new Vector3(2, 0, 2);
                break;
            case 3:
                wallDestroyer.transform.position += new Vector3(4, 0, 0);
                break;
            case 4:
                wallDestroyer.transform.position += new Vector3(2, 0, -2);
                break;

            case 5:
                wallDestroyer.transform.position += new Vector3(0, 0, -4);
                break;
            case 6:
                wallDestroyer.transform.position += new Vector3(-2, 0, -2);
                break;
            case 7:
                wallDestroyer.transform.position += new Vector3(-4, 0, 0);
                break;
            case 8:
                wallDestroyer.transform.position += new Vector3(-2, 0, 2);
                break;
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        Stack<Vector2> visitedStack = new Stack<Vector2>();
        mazeReady = false;
        Application.targetFrameRate = frameRate;
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(24.8f, 28.3f, 12.1f);
        GameObject.FindGameObjectWithTag("preGame").GetComponent<Renderer>().material.color = Color.black;
        pregameText.GetComponent<TextMeshProUGUI>().enabled = true;
        ingameText.GetComponent<TextMeshProUGUI>().enabled = false;
        Timer.GetComponent<TextMeshProUGUI>().enabled = false;

        mazeGrid = new bool[100, 100];


        //Assuming starting position is 0, 0
        for (int i = 0; i < 100; i++)
        {

            for (int j = 0; j < 100; j++)
            {
                mazeGrid[i, j] = false;
            }
        }
        mazeGrid[0, 0] = true;
        //currentGridPos = new Vector2(0, 0);


        //camera game position: (24.8, 28.3, 12.1) x = 24.8, y = 28.3 , z = 12.1 | rotation x = 90 y and z are 0 | scale is all 1
        Maze_Size.x = Maze_Width;
        Maze_Size.y = Maze_Height;
        Maze_Width = 5;
        Maze_Height = 5;
        Maze_Size.x = 5;
        Maze_Size.y = 5;
        //bool[,] Visited = new bool[(int)Maze_Size.x, (int)Maze_Size.y];
        //Instantiate(wallPrefab, new Vector2(wallPrefab.transform.position.x + 3.0f, wallPrefab.transform.position.y), Quaternion.identity);
        //Instantiate(wallPrefab, new Vector3(wallPrefab.transform.position.x + 50.0f, wallPrefab.transform.position.y, wallPrefab.transform.position.z), Quaternion.identity);

        int a = Random.Range(1, (int)Maze_Size.x) * 4;
        int b = Random.Range(1, (int)Maze_Size.y) * 4;
        //Debug.Log(a + "," + b);

        for (int i = 0; i < ((int)Maze_Size.x * 4); i += 4)
        {
            for (int j = 0; j < ((int)Maze_Size.y * 4); j += 4)
            {
                //Debug.Log("(" + i + "," + j + "," + ")");
                genGrid(new Vector3(i, 0, j), new Vector2(a, b));
            }
        }

        mazePlayer = GameObject.FindGameObjectWithTag("mazePlayer");
        goalLocation = GameObject.FindGameObjectWithTag("goalLocation");
        //wallDestroyer.transform.position = new Vector3(2, 0, 0);

        targetPosGrid = new Vector2(0, 0);
        currentPosGrid = new Vector2(5, 5);
        //Vector3 test = new Vector3(convertToWorld(currentPosGrid).x, 0, convertToWorld(currentPosGrid).y);
        mazePlayer.transform.position = convertToWorld(currentPosGrid);
        Debug.Log(convertToWorld(currentPosGrid).x + "," + 0 + "," + convertToWorld(currentPosGrid).y);

        ////Test for random generation
        //targetPosGrid = gridMovement(targetPosGrid);
        //visitedStack.Push(targetPosGrid);
        //Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);
        //currentPosGrid = targetPosGrid;


        ////Generation for pre-set maze for prototype
        StartCoroutine(setGen());



    }

    //Generates sample maze for proof of concept within 5 seconds.
    IEnumerator setGen()
    {
        float delay = 0.035f; //0.035f;
        Debug.Log("Generating Maze...");
        yield return new WaitForSeconds(delay);
        wallDestroyer.transform.position = new Vector3(2, 0, 2);
        //moveGen3(1);
        yield return new WaitForSeconds(delay);
        //moveGen2('n');
        moveGen3(1);
        yield return new WaitForSeconds(delay);
        moveGen3(1);
        //moveGen2('n');
        yield return new WaitForSeconds(delay);
        moveGen3(1);
        //moveGen2('n');
        yield return new WaitForSeconds(delay);
        //targetPos.transform.position += new Vector3(2, 0, 2);
        moveGen3(2);
        yield return new WaitForSeconds(delay);
        //---------------
        //moveGen2('e');
        moveGen3(3);
        yield return new WaitForSeconds(delay);
        //moveGen2('e');
        moveGen3(3);
        yield return new WaitForSeconds(delay);
        //moveGen2('e');
        moveGen3(3);
        yield return new WaitForSeconds(delay);
        //targetPos.transform.position += new Vector3(2, 0, -2);
        moveGen3(4);
        yield return new WaitForSeconds(delay);
        //-------------------------------
        //moveGen2('s');
        moveGen3(5);
        yield return new WaitForSeconds(delay);
        //moveGen2('s');
        moveGen3(5);
        yield return new WaitForSeconds(delay);
        //('s');
        moveGen3(5);
        yield return new WaitForSeconds(delay);
        //targetPos.transform.position += new Vector3(-2, 0, -2);
        moveGen3(6);
        yield return new WaitForSeconds(delay);
        //------------------------------
        //moveGen2('w');
        moveGen3(7);
        yield return new WaitForSeconds(delay);
        moveGen3(7);
        // moveGen2('w');
        yield return new WaitForSeconds(delay);
        //targetPos.transform.position += new Vector3(-2, 0, 2);
        moveGen3(8);
        yield return new WaitForSeconds(delay);
        //-----------------------------


        //moveGen2('n');
        moveGen3(1);
        yield return new WaitForSeconds(delay);
        moveGen3(1);
        //moveGen2('n');
        yield return new WaitForSeconds(delay);
        moveGen3(2);
        //targetPos.transform.position += new Vector3(2, 0, 2);
        yield return new WaitForSeconds(delay);
        ////---------------------------
        //moveGen2('e');
        moveGen3(3);
        yield return new WaitForSeconds(delay);
        moveGen3(4);
        //targetPos.transform.position += new Vector3(-2, 0, -2);
        yield return new WaitForSeconds(delay);
        moveGen3(5);
        yield return new WaitForSeconds(delay);

        moveGen3(6);
        yield return new WaitForSeconds(delay);
        moveGen3(8);
        yield return new WaitForSeconds(delay);
        ////----------------------------
        ////moveGen2('w');
        //moveGen3(7);

        //yield return new WaitForSeconds(delay);
        //moveGen3(8);
        //targetPos.transform.position += new Vector3(-2, 0, 2);
        //yield return new WaitForSeconds(delay);
        //moveGen3(1);
        ////moveGen2('n');
        //yield return new WaitForSeconds(delay);
        mazePlayer.transform.position = new Vector3(2, 0, 0);
        goalLocation.transform.position = (wallDestroyer.transform.position + new Vector3(0, 0, 2));
        //currentPos.SetActive(false);
        wallDestroyer.SetActive(false);
        mazeReady = true;
        ////GameObject.FindGameObjectWithTag("preGame").GetComponent<MeshRenderer>().enabled = false;
        ////pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
        ////Timer.GetComponent<TextMeshProUGUI>().enabled = true;
        ////if (showIngameText == true)
        ////{
        ////    ingameText.GetComponent<TextMeshProUGUI>().enabled = true;
        ////}


        // myDialogBalloon.GetComponent<Image>().enabled = false;
        Debug.Log("Maze Generated, press space to begin");
        //transform.position = new Vector3(0, 0, 0);
        //yield return new WaitForSeconds(1);
        //transform.position = new Vector3(1, 0, 0);
    }






    // Update is called once per frame
    void Update()
    {
        //Vector3 dir = targetPos.transform.position - currentPos.transform.position;
        //currentPos.transform.position += (dir.normalized * 50) * Time.deltaTime;
        //currentPos.transform.rotation = Quaternion.Euler(90, 0, 0);

        if (Input.GetKeyDown("r"))
        {
            wallDestroyer.transform.position = new Vector3(2, 0, 4);
            //currentPos.transform.position = new Vector3(2, 0, -16);
            Debug.Log("Refreshing Maze, please wait");
            StartCoroutine(setGen());
        }
        if (Input.GetKeyDown("space") && mazeReady == true) //&& pressedPlay == false
        {
            GameObject.FindGameObjectWithTag("preGame").GetComponent<MeshRenderer>().enabled = false;
            pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
            Timer.GetComponent<TextMeshProUGUI>().enabled = true;
            if (showIngameText == true)
            {
                ingameText.GetComponent<TextMeshProUGUI>().enabled = true;
            }
            //pressedPlay = true;
        }



    }
}

