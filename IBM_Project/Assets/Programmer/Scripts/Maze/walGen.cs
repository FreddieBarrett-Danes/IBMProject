  using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Camera position is handled as cameraMaze(bool inMaze)
//-----------------------------------------------------


//Generation

//Prep. Set start position, move targetPosWorld and currentPosWorld to start position

//1. Move targetPosWorld to adjacent unvisited cell at random
//Use gridMovement (targetPosGrid = gridMovement(currentPosGrid);

//IF there’s at least one viable cell adjacent:

//2.Break walls between current and target position
//WallDestroyer.transform.position = Vector3.Lerp(ConvertToWorld(currentPosGrid),ConvertToWorld(targetPosGrid), 0.5f)
//(Convert CurrentPos and TargetPos from grid to world, move wall destroyer between them using Lerp)

//3.Move CurrentPos to TargetPos

//4. Add selected cell to stack
//visitedStack.push(targetPosGrid.x, targetPosGrid.y);
//Repeat back up from step 1

//————————

//IF there’s no viable adjacent cells:

//2.Move CurrentPos and TargetPos to position of top stack item
//CurrentPosGrid = visitedStack.pop()
//TargetPosGrid = CurrentPosGrid


//    IF there’s at least one element on the stack:
//If(visitedCells.Contains(startposition) == true);

//Repeat back up from step 1

//	IF there’s no elements in the stack:

//Mark maze as complete

public class walGen : MonoBehaviour
{
    //public int frameRate;


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


    //used for debugging
    public GameObject targetPosWorld;
    public GameObject currentPosWorld;


    Vector2 targetPosGrid;
    Vector2 currentPosGrid;

    //UI
    public GameObject pregameText;
    public GameObject pressStartText;
    public GameObject Timer;
    public bool showIngameText;
    public bool showPregameTutorial;

    //public Camera camera;


    Stack<Vector2> visitedStack;


    //public Vector3 cameraPosition;
    public Vector3 preGoalLocation;
    int goalLocationRng;


    //public delegate void DelType1(bool mazeReady); //Delegate type
    //public static event DelType1 OnMazeReady; //Event variable



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
        v3 -= new Vector3(62, 0, 60);
        v3 -= new Vector3(2, 0, 0);
        v3 /= 4;

        return new Vector2(v3.x, v3.z);
    }


    /* Original convertToWorld
     Vector3 convertToWorld(Vector2 v2) //GridSpace -> WorldSpace
    {
        Vector3 rv = new Vector3(v2.x, 0, v2.y);
        rv *= 4;
        rv += new Vector3(2, 0, 0);
        //Debug.Log("Grid , World" + " " + v2 + "," + rv);
        return rv;
    }
    */


    Vector3 convertToWorld(Vector2 v2) //GridSpace -> WorldSpace
    {
        Vector3 rv = new Vector3(v2.x, 0, v2.y);
        rv *= 4;
        //rv += new Vector3(2, 0, 0);
        rv += new Vector3(64, 0, 62); //+new Vector3(24, 0, 22) for updated/moved maze position | + new Vector3(2,0,2) for world maze calibration
        //rv _- new Vector3(2, 0, 2);
        //rv += new Vector3(2, 0, 0);
        //rv += new Vector3(64, 0, 60)
        //Debug.Log("Grid , World" + " " + v2 + "," + rv);
        return rv;
    }
    Vector2 gridMovement(Vector2 cgp) //Moves currentposGrid to an adjacent unvistied cell if viable (backtracks using stack if isn't)
                                      //cgp = current grip pos
    {
        int[] array1 = new int[] { 0, 0, 0, 0, 0 }; //Array is used to hold which cells are unvisited to use for rng.
                                                    //north, south, east, west, none
        bool backtracking = false;

        Debug.Log("cpg.x = " + cgp.x + " | cpg.y = " + cgp.y);
        //This checks what adjacent cells are unvisited.
        //Area of refinement: convert to switch case?
        if (cgp.y != (Maze_Size.y - 1))
        {
            if (mazeGrid[(int)cgp.x, (int)cgp.y + 1] == false)
            {
                //north is viable
                array1[0] = 1;

            }
        }
        if (cgp.y != 0)
        {
            if (mazeGrid[(int)cgp.x, (int)cgp.y - 1] == false)
            {
                //south is viable
                array1[1] = 2;
            }
        }
        if (cgp.x != (Maze_Size.x - 1))
        {
            if (mazeGrid[(int)cgp.x + 1, (int)cgp.y] == false)
            {
                //east is viable
                array1[2] = 3;
            }
        }
        if (cgp.x != 0) //Ignore if can't progress further from - x
        {
            if (mazeGrid[(int)cgp.x - 1, (int)cgp.y] == false)
            {
                //west is viable
                array1[3] = 4;
            }
        }
        Debug.Log("Array values: " + array1[0] + "," + array1[1] + "," + array1[2] + "," + array1[3]);

        //Area of refinement: check if all values of array = 0
        if (array1[0] == 0 && array1[1] == 0 && array1[2] == 0 && array1[3] == 0)
        {
            Debug.Log("Dead end reached");
            array1[4] = 5;
        }

        int rng = 0;

        

        while (rng == 0)
        {
            if (array1[4] == 5)
            {
                rng = 5;
            }
            Debug.Log("This should be showing");
            if (array1[4] == 0)
            {
                rng = array1[Random.Range(0, 5)];
            }
            //if (visitedStack.Count == 0)
            //{
            //    rng = 6;
            //}
            Debug.Log("This should also be showing");
            //Debug.Log("rng = " + rng + " array being referenced: " + array1[rng]);
            //should keep looping until a value different from 0 is found
        }

        Debug.Log("Directions found | N: " + array1[0] + " S: " + array1[1] + " E: " + array1[2] + " W: " + array1[3]);
        Debug.Log("Direction selected: " + rng);

        switch (rng)
        {
            case 0:
                Debug.Log("rng error, check Random.Range of rng in gridMovement");
                break;
            case 1:
                cgp.y += 1;
                //visitedStack.Push(cgp);
                //add position to the stack
                mazeGrid[(int)cgp.x, (int)cgp.y] = true;
                break;
            case 2:
                cgp.y -= 1;
                //visitedStack.Push(cgp);
                //add position to the stack
                mazeGrid[(int)cgp.x, (int)cgp.y] = true;
                break;
            case 3:
                cgp.x += 1;
                //visitedStack.Push(cgp);
                //add position to the stack
                mazeGrid[(int)cgp.x, (int)cgp.y] = true;
                break;
            case 4:
                cgp.x -= 1;
                //visitedStack.Push(cgp);
                //add position to the stack
                mazeGrid[(int)cgp.x, (int)cgp.y] = true;
                break;
            case 5:
                backtracking = true;
                Debug.Log("Backtrack using stack");
                //Debug.Log(visitedStack.Peek());
                cgp = visitedStack.Pop();
                wallDestroyer.GetComponent<MeshRenderer>().enabled = false;
                //currentPosWorld.GetComponent<MeshRenderer>().enabled = false;
                //targetPosWorld.GetComponent<MeshRenderer>().enabled = false;
                //visitedStack.Pop();
                //visitedStack.Push(currentPosGrid);
                //backtrack using the stack
                break;
            case 6:
                Debug.Log("Reached end of stack");
                break;
        }
        if (backtracking == false)
        {
            visitedStack.Push(cgp);
            wallDestroyer.GetComponent<MeshRenderer>().enabled = true;
            //currentPosWorld.GetComponent<MeshRenderer>().enabled = true;
            //targetPosWorld.GetComponent<MeshRenderer>().enabled = true;

        }
        targetPosWorld.transform.position = convertToWorld(targetPosGrid);
        currentPosWorld.transform.position = convertToWorld(currentPosGrid);
        targetPosWorld.transform.position = convertToWorld(cgp);
        currentPosWorld.transform.position = convertToWorld(cgp);
        //GameObject.Find("Tracker").transform.position = convertToWorld(cgp);
        if (backtracking == false) { wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(cgp), 0.5f); }
        //wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(cgp), 0.5f);
        currentPosGrid = cgp;
        //if (backtracking == false)
        //{
        //    wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);
        //}
        //currentPosGrid = targetPosGrid;
        //Vector2 rv2 = cgp;
        //mazeGrid[(int)rv2.x, (int)rv2.y] = true;
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

    //void cameraMaze(bool inMaze)
    //{
    //    if (inMaze == true)
    //    {
    //        camera.transform.position += new Vector3(24, 0, 22);
    //        //new Vector3(24, 0, 22)
    //    }
    //    else
    //    {
    //        camera.transform.position -= new Vector3(24, 0, 22);
    //    }
    //}





    // Start is called before the first frame update
    void Start()
    {
        visitedStack = new Stack<Vector2>();
        mazeReady = false;
        //Application.targetFrameRate = frameRate;
        //GameObject.FindGameObjectWithTag("MainCamera").transform.position = cameraPosition;
        //GameObject.FindGameObjectWithTag("preGame").GetComponent<Renderer>().material.color = Color.white;
        //pregameText.GetComponent<TextMeshProUGUI>().enabled = true;
        //Debug.Log("Pregame enabled: " + pregameText.GetComponent<TextMeshProUGUI>().enabled);
        preGoalLocation = new Vector3(0, 0, 0);
        goalLocationRng = Random.Range(Maze_Width*2, (Maze_Width * Maze_Height));

        //--------------------------------------
        //UI:
        ////pregameText.SetActive(true);
        ////pressStartText.SetActive(true);
        ////Timer.SetActive(true);
        ////GameObject.Find("tutorialBackground").GetComponent<MeshRenderer>().enabled = showPregameTutorial;

        //////GameObject.FindGameObjectWithTag("preGame").GetComponent<Renderer>().enabled = showPregameTutorial;
        //////GameObject.FindGameObjectWithTag("preGame").GetComponent<Renderer>().transform.position = new Vector3(cameraPosition.x, cameraPosition.y-2, cameraPosition.z);
        ////pressStartText.GetComponent<TextMeshProUGUI>().enabled = false;
        ////Timer.GetComponent<TextMeshProUGUI>().enabled = false;

        //---------------------------------------

        mazeGrid = new bool[100, 100];


        //Set each value in bool array to false (univisted)
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
        //Maze_Width = 5;
        //Maze_Height = 5;
        //Maze_Size.x = 5;
        //Maze_Size.y = 5;
        //bool[,] Visited = new bool[(int)Maze_Size.x, (int)Maze_Size.y];
        //Instantiate(wallPrefab, new Vector2(wallPrefab.transform.position.x + 3.0f, wallPrefab.transform.position.y), Quaternion.identity);
        //Instantiate(wallPrefab, new Vector3(wallPrefab.transform.position.x + 50.0f, wallPrefab.transform.position.y, wallPrefab.transform.position.z), Quaternion.identity);

        //Sets random start location (not currently implemented)
        int a = Random.Range(1, (int)Maze_Size.x) * 4;
        int b = Random.Range(1, (int)Maze_Size.y) * 4;
        //Debug.Log(a + "," + b);

        //Generate grid in world space (instigate wall gameobject)
        
        
        for (int i = 62; i < (62 + (int)Maze_Size.x * 4); i += 4)
        {
            for (int j = 62; j < (62 + (int)Maze_Size.y * 4); j += 4)
            {
                //Debug.Log("(" + i + "," + j + "," + ")");
                genGrid(new Vector3(i, 0, j), new Vector2(1, 1));
            }
        }

        /*for (int i = 0; i < ((int)Maze_Size.x * 4); i += 4)
        {
            for (int j = 0; j < ((int)Maze_Size.y * 4); j += 4)
            {
                //Debug.Log("(" + i + "," + j + "," + ")");
                genGrid(new Vector3(i, 0, j), new Vector2(a, b));
            }
        }*/


        mazePlayer = GameObject.FindGameObjectWithTag("mazePlayer");
        goalLocation = GameObject.FindGameObjectWithTag("goalLocation");
        //wallDestroyer.transform.position = new Vector3(2, 0, 0);


        //Test of moving mazePlayer based on Gird-space

        //targetPosGrid = new Vector2(0, 0);
        //currentPosGrid = new Vector2(5, 5);
        ////Vector3 test = new Vector3(convertToWorld(currentPosGrid).x, 0, convertToWorld(currentPosGrid).y);
        //mazePlayer.transform.position = convertToWorld(currentPosGrid);
        //Debug.Log(convertToWorld(currentPosGrid).x + "," +  0 + "," + convertToWorld(currentPosGrid).y);

        //mazePlayer.transform.position = convertToWorld(targetPosGrid);


        //Setting Start Position: 1,1
        currentPosGrid = new Vector2(0, 0);
        targetPosGrid = new Vector2(0, 0);

        //CameraMaze call - Sets position of the camera
        //cameraMaze(true);

        //--debug--//
         //currentPosGrid = convertToGrid(new Vector3(24, 0, 22));//= new Vector2(24, 24);
         //targetPosGrid = convertToGrid(new Vector3(24, 0, 22));
        //--debug//
        //GameObject.Find("Tracker").transform.position = convertToWorld(currentPosGrid);
        //Debug.Log(currentPosGrid);
        //currentPosWorld.transform.position = new Vector3(2, 0, 0);
        //targetPosWorld.transform.position = new Vector3(4, 0, 0);
        //wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);
        //wallDestroyer.transform.position = Vector3.Lerp(currentPosWorld.transform.position, targetPosWorld.transform.position, 0.5f);
        //currentPosGrid = targetPosGrid;

        //targetPosGrid = gridMovement(currentPosGrid);
        //Debug.Log("currentPosGrid , targetPosGrid" + " " + currentPosGrid + "," + targetPosGrid);
        //visitedStack.Push(currentPosGrid);
        
        
        StartCoroutine(destroyWall());

        //targetPosWorld.transform.position = convertToWorld(targetPosGrid);
        //currentPosWorld.transform.position = convertToWorld(currentPosGrid);

        //wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);

        ////Repeat test

        //targetPosGrid = gridMovement(currentPosGrid);
        //Debug.Log("currentPosGrid , targetPosGrid" + " " + currentPosGrid + "," + targetPosGrid);


        //targetPosWorld.transform.position = convertToWorld(targetPosGrid);
        //currentPosWorld.transform.position = convertToWorld(currentPosGrid);

        //wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);



        ////Test for random generation
        //targetPosGrid = gridMovement(targetPosGrid);
        //visitedStack.Push(targetPosGrid);
        //Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);
        //currentPosGrid = targetPosGrid;


        ////Generation for pre-set maze for prototype
        //StartCoroutine(setGen());



    }

    IEnumerator destroyWall()
    {
        float delay = 0.035f; //0.035f; //0.0175f; //0.035f;

        //delay = (1 / frameRate) * 2;
        //Debug.Log(delay);
        //wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);
        //yield return new WaitForSeconds(delay);

        int cellCount = 0;

        yield return new WaitForSeconds(delay);

        targetPosGrid = gridMovement(currentPosGrid);
        //targetPosWorld.transform.position = convertToWorld(targetPosGrid);
        //currentPosWorld.transform.position = convertToWorld(currentPosGrid);
        //wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);
        //currentPosGrid = targetPosGrid;
        //mazeGrid[(int)currentPosGrid.x, (int)currentPosGrid.y] = true;
        //visitedStack.Push(currentPosGrid);
        currentPosWorld.transform.position = targetPosWorld.transform.position;
        Debug.Log("UPDATE " + targetPosGrid.x + "," + targetPosGrid.y);

        yield return new WaitForSeconds(delay);

        //--Debug--//
        //yield return new WaitForSeconds(120.0f);

        while (visitedStack.Count != 0) //Looping 100 times for demonstration
        {
            cellCount++;
            targetPosGrid = gridMovement(currentPosGrid);
            //targetPosWorld.transform.position = convertToWorld(targetPosGrid);
            //currentPosWorld.transform.position = convertToWorld(currentPosGrid);
            //wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);
            //currentPosGrid = targetPosGrid;
            //mazeGrid[(int)currentPosGrid.x, (int)currentPosGrid.y] = true;
            //visitedStack.Push(currentPosGrid);
            //currentPosWorld.transform.position = targetPosWorld.transform.position;
            Debug.Log("UPDATE " + targetPosGrid.x + "," + targetPosGrid.y);

            if (cellCount == ((Maze_Size.x * Maze_Size.y))) { preGoalLocation = convertToWorld(currentPosGrid); }
            //if (cellCount == goalLocationRng) { preGoalLocation = convertToWorld(currentPosGrid); }

            yield return new WaitForSeconds(delay); 
        }
        yield return new WaitForSeconds(delay);
        Debug.Log("MAZE IS COMPLETE!!! WOOOH!!!!");
        

        //mazePlayer.transform.position = new Vector3(2, 0, 0);
        //goalLocation.transform.position = new Vector3(12,0,12);
        //currentPos.SetActive(false);
        wallDestroyer.SetActive(false);
        currentPosWorld.SetActive(false);
        targetPosWorld.SetActive(false);
        mazeReady = true;
        pressStartText.GetComponent<TextMeshProUGUI>().enabled = true;

        //for (int i = 0; i < 100; i++)
        //{

        //    for (int j = 0; j < 100; j++)
        //    {
        //        Debug.Log(mazeGrid[i, j]);
        //    }
        //}

        ////Repeat test

        //Debug.Log("Repeat");

        //targetPosGrid = gridMovement(currentPosGrid);
        //Debug.Log("currentPosGrid , targetPosGrid" + " " + currentPosGrid + "," + targetPosGrid);


        //targetPosWorld.transform.position = convertToWorld(targetPosGrid);
        //currentPosWorld.transform.position = convertToWorld(currentPosGrid);

        //wallDestroyer.transform.position = Vector3.Lerp(convertToWorld(currentPosGrid), convertToWorld(targetPosGrid), 0.5f);
        //currentPosGrid = targetPosGrid;
        //currentPosWorld.transform.position = targetPosWorld.transform.position;
        //visitedStack.Push(currentPosGrid);

        //visitedStack.Peek();
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
        //mazePlayer.transform.position = new Vector3(2, 0, 0);
        //goalLocation.transform.position = (wallDestroyer.transform.position + new Vector3(0, 0, 2));
        ////currentPos.SetActive(false);
        //wallDestroyer.SetActive(false);
        //mazeReady = true;
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




        /*if (Input.GetKeyDown("r"))
        {
            wallDestroyer.transform.position = new Vector3(2, 0, 4);
            //currentPos.transform.position = new Vector3(2, 0, -16);
            Debug.Log("Refreshing Maze, please wait");
            StartCoroutine(setGen());
        } */



        if (mazeReady == true) //&& pressedPlay == false
        {
            //mazePlayer.transform.position = new Vector3(64, 0, 62); //new Vector3(2, 0, 0);
            //goalLocation.transform.position = preGoalLocation;

            //Debug.Log("Press 'P' to complete maze instantly");
            //OnMazeReady(true); //Activate maze minigame timer


            //GameObject.FindGameObjectWithTag("preGame").GetComponent<MeshRenderer>().enabled = false;
            //GameObject.Find("tutorialBackground").GetComponent<MeshRenderer>().enabled = false;
            //pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
            //Timer.GetComponent<TextMeshProUGUI>().enabled = true;
            //pressStartText.GetComponent<TextMeshProUGUI>().enabled = false;

            //pressedPlay = true;
        }


        //if (Input.GetKeyDown("space") && mazeReady == true) //&& pressedPlay == false
        //{
        //    Debug.Log("Press 'P' to complete maze instantly");
        //    OnMazeReady(true);
        //    mazePlayer.transform.position = new Vector3(64, 0, 62); //new Vector3(2, 0, 0);
        //    goalLocation.transform.position = preGoalLocation;
        //    //goalLocation.transform.position = new Vector3(18, 0, 16);
        //    //GameObject.FindGameObjectWithTag("preGame").GetComponent<MeshRenderer>().enabled = false;
        //    GameObject.Find("tutorialBackground").GetComponent<MeshRenderer>().enabled = false;
        //    pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
        //    Timer.GetComponent<TextMeshProUGUI>().enabled = true;
        //    pressStartText.GetComponent<TextMeshProUGUI>().enabled = false;

        //    //pressedPlay = true;
        //}

        if (Input.GetKeyDown(KeyCode.U))
        {
            for (int i = 62; i < (62 + (int)Maze_Size.x * 4); i += 4)
            {
                for (int j = 62; j < (62 + (int)Maze_Size.y * 4); j += 4)
                {
                    //Debug.Log("(" + i + "," + j + "," + ")");
                    genGrid(new Vector3(i, 0, j), new Vector2(1, 1));
                }
            }
        }



    }
}

