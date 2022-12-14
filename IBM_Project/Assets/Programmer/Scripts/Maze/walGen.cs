using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class walGen : MonoBehaviour
{
    public GameObject prefab;
    public int Maze_Width;
    public int Maze_Height;
    Vector2 Maze_Size; //Maze_Size.x, Maze_Size.y

    //Positions represented in Unity World Space
    public GameObject currentPos;
    public GameObject targetPos;

    GameObject mazePlayer;
    GameObject goalLocation;

    public TextMeshProUGUI pregameText;
    public TextMeshProUGUI ingameText;
    public TextMeshProUGUI Timer;
    public bool showIngameText;

    //Positions of Gameobjects represented in bool grid space
    private int currentGridPos;
    private int targetGridPos;

    public bool[,] Visitedtest; //to set max grid width/height

    public int frameRate;

    //bool pressedPlay;
    bool mazeReady;


    Vector2 abV2;




    void GenCell(Vector3 Origin, Vector2 ab) //Generate Maze cell by instantiating 4 walls
    {
        //Debug.Log(Origin.x + "," + Origin.y + "," + Origin.z);
        Instantiate(prefab, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z + 2.0f), Quaternion.identity);
        Instantiate(prefab, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z - 2.0f), Quaternion.identity);
        Instantiate(prefab, new Vector3(Origin.x, Origin.y, Origin.z), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        Instantiate(prefab, new Vector3(Origin.x + 4.0f, Origin.y, Origin.z), Quaternion.Euler(0.0f, 90.0f, 0.0f));


        if (ab.x == Origin.x && ab.y == Origin.z)
        {
            //Instantiate(currentPos, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z), Quaternion.identity);


            //targetPos.transform.position = new Vector3(Origin.x + 2.0f, Origin.y, Origin.z);

            currentPos.transform.position = new Vector3(Origin.x + 2.0f, Origin.y, Origin.z);

        }
        //Instantiate(currentPos, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z), Quaternion.identity);
        //Instantiate(prefab, new Vector3(Origin.x - 2.0f, Origin.y, Origin.z - 2.0f), Quaternion.identity);
        //Instantiate(prefab, new Vector3(Origin.x, 0, Origin.y), Quaternion.identity);
    }



    //'Direction' of the maze generation can be influenced using moveGen
    void moveGen(char dir, int amount)
    {
        Vector3 dist = targetPos.transform.position - currentPos.transform.position;
        switch (dir)
        {


            case 'n':
                targetPos.transform.position += new Vector3(0.0f, 0.0f, 4.0f * amount);
                //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 's':
                targetPos.transform.position -= new Vector3(0.0f, 0.0f, 4.0f * amount);
                //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 'e':
                targetPos.transform.position += new Vector3(4.0f * amount, 0.0f, 0.0f);
                //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 'w':
                targetPos.transform.position -= new Vector3(4.0f * amount, 0.0f, 0.0f);
                //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }

    }

    void moveGen2(char dir)
    {
        Vector3 dist = targetPos.transform.position - currentPos.transform.position;

        switch (dir)
        {
            case 'n':
                targetPos.transform.position += new Vector3(0.0f, 0.0f, 4.0f);
                break;
            case 's':
                targetPos.transform.position -= new Vector3(0.0f, 0.0f, 4.0f);

                //targetPos.transform.position -= new Vector3(0.0f, 0.0f, 4.0f * amount);
                //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 'e':
                targetPos.transform.position += new Vector3(4.0f, 0.0f, 0.0f);

                //targetPos.transform.position += new Vector3(4.0f * amount, 0.0f, 0.0f);
                //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 'w':
                targetPos.transform.position -= new Vector3(4.0f, 0.0f, 0.0f);

                //targetPos.transform.position -= new Vector3(4.0f * amount, 0.0f, 0.0f);
                //currentPos.transform.position += (dist.normalized * 50) * Time.deltaTime;
                //currentPos.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }


    }




    // Start is called before the first frame update
    void Start()
    {
        mazeReady = false;
        Application.targetFrameRate = frameRate;
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(24.8f, 28.3f, 12.1f);
        GameObject.FindGameObjectWithTag("preGame").GetComponent<Renderer>().material.color = Color.black;
        pregameText.GetComponent<TextMeshProUGUI>().enabled = true;
        ingameText.GetComponent<TextMeshProUGUI>().enabled = false;
        Timer.GetComponent<TextMeshProUGUI>().enabled = false;

        //camera game position: (24.8, 28.3, 12.1) x = 24.8, y = 28.3 , z = 12.1 | rotation x = 90 y and z are 0 | scale is all 1
        Maze_Size.x = Maze_Width;
        Maze_Size.y = Maze_Height;
        Maze_Width = 5;
        Maze_Height = 5;
        Maze_Size.x = 5;
        Maze_Size.y = 5;
        bool[,] Visited = new bool[(int)Maze_Size.x, (int)Maze_Size.y];
        //Instantiate(prefab, new Vector2(prefab.transform.position.x + 3.0f, prefab.transform.position.y), Quaternion.identity);
        //Instantiate(prefab, new Vector3(prefab.transform.position.x + 50.0f, prefab.transform.position.y, prefab.transform.position.z), Quaternion.identity);

        int a = Random.Range(1, (int)Maze_Size.x) * 4;
        int b = Random.Range(1, (int)Maze_Size.y) * 4;
        //Debug.Log(a + "," + b);

        for (int i = 0; i < ((int)Maze_Size.x * 4); i += 4)
        {
            for (int j = 0; j < ((int)Maze_Size.y * 4); j += 4)
            {
                //Debug.Log("(" + i + "," + j + "," + ")");
                GenCell(new Vector3(i, 0, j), new Vector2(a, b));
            }
        }

        Visited[a / 4, b / 4] = true;
        //visitedGridPos(a / 4, b / 4, false);
        //visitedGridPos(a, b, true);
        //Debug.Log((currentPos.transform.position.x - 2) / 4 + "," + (currentPos.transform.position.y) / 4);
        //Debug.Log(a / 4 + "," + b / 4);

        abV2.x = a / 4;
        abV2.y = b / 4;
        //targetPos.transform.position = new Vector3(2, 0, 4);
        currentPos.transform.position = new Vector3(2, 0, -16);
        mazePlayer = GameObject.FindGameObjectWithTag("mazePlayer");
        goalLocation = GameObject.FindGameObjectWithTag("goalLocation");
        targetPos.transform.position = new Vector3(2, 0, 0);
        //GameObject.FindGameObjectWithTag("preGame").GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(setGen());
        //moveGen2('n', 1);

        //---------------------------------------------------
        ////mazePlayer.transform.position = new Vector3(2, 0, 0);
        //////goalLocation.transform.position = targetPos.transform.position;
        ////currentPos.SetActive(false);
        //////targetPos.SetActive(false);
        ////GameObject.FindGameObjectWithTag("preGame").GetComponent<MeshRenderer>().enabled = false;
        ////pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
        ////Timer.GetComponent<TextMeshProUGUI>().enabled = true;
        ////if (showIngameText == true)
        ////{
        ////    ingameText.GetComponent<TextMeshProUGUI>().enabled = true;
        ////}
        ////Debug.Log("Maze Generated");
        //---------------------------------------------------





        //GenCell(new Vector3(0, 0, 10));
        //GenCell(new Vector3(0, 0, 14));
        //GenCell(new Vector3(4, 0, 10));
        //GenCell(new Vector3(4, 0, 14));
        //int test1 = (int)Maze_Size.x;
        //int test2 = ((int)Maze_Size.y) * test1;
        //bool[] Visited = new bool[test2];
        //for (int k = 0; k < test2; k++)
        //{
        //    Debug.Log(Visited[k]);
        //}
        //currentPos.transform.position = new Vector3((Random.Range(1,(int)Maze_Size.x) * 4) + 2.0f ,0,Random.Range(1, (int)Maze_Size.y * 4) + 1.0f);


        //moveGen('n', 3);

        //Debug.Log("is maze complete yet?");

    }

    //Generates sample maze for proof of concept within 5 seconds.
    IEnumerator setGen()
    {
        float delay = 0.035f;
        Debug.Log("Generating Maze...");
        yield return new WaitForSeconds(delay);
        targetPos.transform.position = new Vector3(2, 0, 2);
        yield return new WaitForSeconds(delay);
        moveGen2('n');
        yield return new WaitForSeconds(delay);
        moveGen2('n');
        yield return new WaitForSeconds(delay);
        moveGen2('n');
        yield return new WaitForSeconds(delay);
        targetPos.transform.position += new Vector3(2, 0, 2);
        yield return new WaitForSeconds(delay);
        //---------------
        moveGen2('e');
        yield return new WaitForSeconds(delay);
        moveGen2('e');
        yield return new WaitForSeconds(delay);
        moveGen2('e');
        yield return new WaitForSeconds(delay);
        targetPos.transform.position += new Vector3(2, 0, -2);
        yield return new WaitForSeconds(delay);
        //-------------------------------
        moveGen2('s');
        yield return new WaitForSeconds(delay);
        moveGen2('s');
        yield return new WaitForSeconds(delay);
        moveGen2('s');
        yield return new WaitForSeconds(delay);
        targetPos.transform.position += new Vector3(-2, 0, -2);
        yield return new WaitForSeconds(delay);
        //------------------------------
        moveGen2('w');
        yield return new WaitForSeconds(delay);
        moveGen2('w');
        yield return new WaitForSeconds(delay);
        targetPos.transform.position += new Vector3(-2, 0, 2);
        yield return new WaitForSeconds(delay);
        //-----------------------------


        moveGen2('n');
        yield return new WaitForSeconds(delay);
        moveGen2('n');
        yield return new WaitForSeconds(delay);
        targetPos.transform.position += new Vector3(2, 0, 2);
        yield return new WaitForSeconds(delay);
        //---------------------------
        moveGen2('e');
        yield return new WaitForSeconds(delay);
        targetPos.transform.position += new Vector3(2, 0, -2);
        yield return new WaitForSeconds(delay);
        //----------------------------
        moveGen2('s');
        yield return new WaitForSeconds(delay);
        targetPos.transform.position += new Vector3(-2, 0, -2);
        yield return new WaitForSeconds(delay);
        //----------------------------
        //moveGen2('w');
        //yield return new WaitForSeconds(delay);
        targetPos.transform.position += new Vector3(-2, 0, 2);
        yield return new WaitForSeconds(delay);
        //moveGen2('n');
        //yield return new WaitForSeconds(delay);
        mazePlayer.transform.position = new Vector3(2, 0, 0);
        goalLocation.transform.position = (targetPos.transform.position + new Vector3(0, 0, 2));
        currentPos.SetActive(false);
        targetPos.SetActive(false);
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
        Vector3 dir = targetPos.transform.position - currentPos.transform.position;
        //currentPos.transform.position += (dir.normalized * 50) * Time.deltaTime;
        //currentPos.transform.rotation = Quaternion.Euler(90, 0, 0);

        if (Input.GetKeyDown("r"))
        {
            targetPos.transform.position = new Vector3(2, 0, 4);
            currentPos.transform.position = new Vector3(2, 0, -16);
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


        //Attempts at maze functionality such as grid position (and goalLocation placement?) not used for prototype
        //if (GameObject.FindGameObjectWithTag("goalLocation").activeInHierarchy == true)
        //{
        //    Debug.Log("Goal Location is found active");
        //}
        //if (GameObject.FindGameObjectWithTag("goalLocation").activeInHierarchy == false)
        //{
        //    Debug.Log("Goal Location is found to be inactive");
        //}
        //if (GameObject.FindGameObjectWithTag("goalLocation").transform.position == new Vector3(-100,100,-100))
        //{
        //    Debug.Log("goalLocation should now be moved");
        //}

        //Maze_Size.x = Maze_Width;
        //Maze_Size.y = Maze_Height;

        //bool[] test = new bool[3]; //sets every value in the array to false by default
        //test[Random.Range(1,3)] = true;




        //Debug.Log(test[0] + "," + test[1] + "," + test[2] + ",");

        //Debug.Log(Visited[2, 1]);

        //for (int i = 0; i > (int)Maze_Size.x; i++)
        //{
        //    Debug.Log(Visited[i, 0]);
        //}


        /*
        ///Debug.Log(abV2.x + "," + abV2.y); //CurrentPos Position in Gridspace
        //Debug.Log(dir.magnitude);
        //Debug.Log(dir.normalized);

        


        if (Input.GetKeyDown("d"))
        {
            //targetPos.transform.position.x += 4.0f;
            targetPos.transform.position += new Vector3(4.0f, 0.0f, 0.0f);
            abV2.x += 1;
        }
        if (Input.GetKeyDown("w"))
        {
            //targetPos.transform.position.x += 4.0f;
            targetPos.transform.position += new Vector3(0.0f, 0.0f, 4.0f);
            abV2.y += 1;
        }
        if (Input.GetKeyDown("a"))
        {
            //targetPos.transform.position.x += 4.0f;
            targetPos.transform.position -= new Vector3(4.0f, 0.0f, 0.0f);
            abV2.x -= 1;
        }
        if (Input.GetKeyDown("s"))
        {
            //targetPos.transform.position.x += 4.0f;
            targetPos.transform.position -= new Vector3(0.0f, 0.0f, 4.0f);
            abV2.y -= 1;
        }

        */


        //---------------------------------------------------------------------


        /*

        //Convert co-ordinates to grid-space
        int x2 = (int)(targetPos.transform.position.x / 4);
        int y2 = (int)(targetPos.transform.position.y / 4);

        bool[,] Visited = new bool[(int)Maze_Size.x, (int)Maze_Size.y];

        //marking current cell as visited (setting current gridcell to true)
        Visited[x2, y2] = true;

        //Check adjacent cells up, down, left and right, true if visited
        //Debug.Log(Visited[x2 + 1, y2]); // check right
        //Debug.Log(Visited[x2, y2 + 1]); // check up
        //Debug.Log(Visited[x2 - 1, y2]); // check left
        //Debug.Log(Visited[x2, y2 - 1]); // check down

        //int nx = x2; //new x
        //int ny = y2; //new y


        //bool valid = false;
        //while(valid == false)
        //{
        int Direction = Random.Range(1, 5); //Random direction between 1-4
                                            //Randomly assign one of these values: x + 1, x - 1, y + 1, y - 1 
        bool inGrid = true;
        ///Re-roll if:
        ///Direction is of visited cell
        ///If a dead-end is reached (e.g. end of the board)

        //right, up, left, down
        //  1,   2,   3,    4
        switch (Direction)
        {
            case 1:
                x2 += 1;
                //move right
                break;
            case 2:
                y2 += 1;
                //move up
                break;
            case 3:
                x2 -= 1;
                //move left
                break;
            case 4:
                y2 -= 1;
                //move down
                break;
        }

        if (x2 + 1 > Maze_Width || x2 - 1 < 0 || y2 + 1 > Maze_Width || y2 - 1 < Maze_Width) //if new position is outside maze paramiters
        {
            inGrid = false;
        }
        else
        {
            inGrid = true;
        }
        //dead-end reached, direction unavailable


        if (Visited[x2, y2] == false && inGrid == true)
        //((Visited[nx, y2] == false && inGrid == true) || (Visited[x2, ny] == false && inGrid == true)) //If the selected direction is marked as true (before swich case, both should be set to true)
        {
            //valid = true
            //Vector3 dir = targetPos.transform.position - currentPos.transform.position;
            targetPos.transform.position = new Vector3(x2 * 4, y2 * 4);
            currentPos.transform.position += (dir.normalized * 50) * Time.deltaTime; //move currentPos to targetPos
        }
        x2 = x2 / 4;
        y2 = y2 / 4;
        //else, return back up to while loop
        //}
        */



    }


    //unused function:
    /*    void processCell(int x, int y) //Mark current cell as visited and check if adjacent cells are visited
        {
            //Convert co-ordinates to grid-space
            int x2 = x / 4;
            int y2 = y / 4;

            bool[,] Visited = new bool[(int)Maze_Size.x, (int)Maze_Size.y];

            //marking current cell as visited (setting current gridcell to true)
            Visited[x2, y2] = true;

            //Check adjacent cells up, down, left and right, true if visited
            Debug.Log(Visited[x + 1, y]); // check right
            Debug.Log(Visited[x, y + 1]); // check up
            Debug.Log(Visited[x - 1, y]); // check left
            Debug.Log(Visited[x, y - 1]); // check down

            //int nx = x2; //new x
            //int ny = y2; //new y


            //bool valid = false;
            //while(valid == false)
            //{
            int Direction = Random.Range(1, 5); //Random direction between 1-4
                                                //Randomly assign one of these values: x + 1, x - 1, y + 1, y - 1 
            bool inGrid = true;
            ///Re-roll if:
            ///Direction is of visited cell
            ///If a dead-end is reached (e.g. end of the board)

            //right, up, left, down
            //  1,   2,   3,    4
            switch (Direction)
            {
                case 1:
                    x2 += 1;
                    //move right
                    break;
                case 2:
                    y2 += 1;
                    //move up
                    break;
                case 3:
                    x2 -= 1;
                    //move left
                    break;
                case 4:
                    y2 -= 1;
                    //move down
                    break;
            }

            if (x2 + 1 > Maze_Width || x2 - 1 < 0 || y2 + 1 > Maze_Width || y2 - 1 < Maze_Width) //if new position is outside maze paramiters
            {
                inGrid = false;
            }
            else
            {
                inGrid = true;
            }
            //dead-end reached, direction unavailable


            if (Visited[x2, y2] == false && inGrid == true)
            //((Visited[nx, y2] == false && inGrid == true) || (Visited[x2, ny] == false && inGrid == true)) //If the selected direction is marked as true (before swich case, both should be set to true)
            {
                //valid = true
                Vector3 dir = targetPos.transform.position - currentPos.transform.position;
                targetPos.transform.position = new Vector3(x2 * 4, y2 * 4);
                currentPos.transform.position += (dir.normalized * 50) * Time.deltaTime; //move currentPos to targetPos
            }
            x2 = x / 4;
            y2 = y / 4;
            //else, return back up to while loop
            //}



            //if (Direction == 1 && Visited[x + 1, y] == false)
            //{
            //    //move right
            //}
            //if (Direction == 2 && Visited[x , y + 1] == false) //up is visited, try again
            //{
            //    //move up
            //}
            //if (Direction == 3 && Visited[x - 1, y] == false) //left is visited, try again
            //{
            //    //move left
            //}
            //if (Direction == 4 && Visited[x, y - 1] == false) //down is visited, try again
            //{
            //    //move down
            //}
            //else
            //{
            //    //loop back
            //}



        } */
}
