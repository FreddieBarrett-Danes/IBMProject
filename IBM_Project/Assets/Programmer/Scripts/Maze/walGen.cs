using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    //Positions of Gameobjects represented in bool grid space
    private int currentGridPos;
    private int targetGridPos;

    public bool[,] Visitedtest; //to set max grid width/height


    Vector2 abV2;




    void GenCell(Vector3 Origin, Vector2 ab) //Generate Maze cell by instantiating 4 walls
    {
        //Debug.Log(Origin.x + "," + Origin.y + "," + Origin.z);
        Instantiate(prefab, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z + 2.0f), Quaternion.identity);
        Instantiate(prefab, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z - 2.0f), Quaternion.identity);
        Instantiate(prefab, new Vector3(Origin.x, Origin.y, Origin.z), Quaternion.Euler(0.0f,90.0f,0.0f));
        Instantiate(prefab, new Vector3(Origin.x + 4.0f, Origin.y, Origin.z), Quaternion.Euler(0.0f, 90.0f, 0.0f));


        if (ab.x == Origin.x && ab.y == Origin.z)
        {
            //Instantiate(currentPos, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z), Quaternion.identity);
            targetPos.transform.position = new Vector3(Origin.x + 2.0f, Origin.y, Origin.z);
            currentPos.transform.position = new Vector3(Origin.x + 2.0f, Origin.y, Origin.z);
            
        }
        //Instantiate(currentPos, new Vector3(Origin.x + 2.0f, Origin.y, Origin.z), Quaternion.identity);
        //Instantiate(prefab, new Vector3(Origin.x - 2.0f, Origin.y, Origin.z - 2.0f), Quaternion.identity);
        //Instantiate(prefab, new Vector3(Origin.x, 0, Origin.y), Quaternion.identity);
    }

    

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

    


    // Start is called before the first frame update
    void Start()
    {
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
                GenCell(new Vector3(i, 0, j), new Vector2(a,b));
            }
        }

        Visited[a / 4, b / 4] = true;
        //visitedGridPos(a / 4, b / 4, false);
        //visitedGridPos(a, b, true);
        //Debug.Log((currentPos.transform.position.x - 2) / 4 + "," + (currentPos.transform.position.y) / 4);
        //Debug.Log(a / 4 + "," + b / 4);

        abV2.x = a / 4;
        abV2.y = b / 4;
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

        targetPos.transform.position = new Vector3(2, 0, 4);
        currentPos.transform.position = new Vector3(2, 0, -16);
        mazePlayer = GameObject.FindGameObjectWithTag("mazePlayer");
        goalLocation = GameObject.FindGameObjectWithTag("goalLocation");
        //moveGen('n', 3);


        StartCoroutine(setGen());
        //Debug.Log("is maze complete yet?");

    }

    IEnumerator setGen()
    {

        moveGen('n', 3);
        yield return new WaitForSeconds(1.0f);
        moveGen('e', 4);
        yield return new WaitForSeconds(0.5f);
        moveGen('s', 4);
        yield return new WaitForSeconds(0.5f);
        moveGen('w', 3);
        yield return new WaitForSeconds(0.5f);
        moveGen('n', 3);
        yield return new WaitForSeconds(0.5f);
        moveGen('e', 2);
        yield return new WaitForSeconds(0.5f);
        moveGen('s', 2);
        yield return new WaitForSeconds(0.5f);
        moveGen('w', 1);
        yield return new WaitForSeconds(0.5f);
        moveGen('n', 1);
        yield return new WaitForSeconds(0.5f);
        mazePlayer.transform.position = new Vector3(2, 0, 0);
        goalLocation.transform.position = targetPos.transform.position;
        currentPos.SetActive(false);
        targetPos.SetActive(false);
        Debug.Log("Maze Generated");
        //transform.position = new Vector3(0, 0, 0);
        //yield return new WaitForSeconds(1);
        //transform.position = new Vector3(1, 0, 0);
    }



    // Update is called once per frame
    void Update()
    {
        //Maze_Size.x = Maze_Width;
        //Maze_Size.y = Maze_Height;
        Vector3 dir = targetPos.transform.position - currentPos.transform.position;
        //bool[] test = new bool[3]; //sets every value in the array to false by default
        //test[Random.Range(1,3)] = true;

        
        currentPos.transform.position += (dir.normalized * 50) * Time.deltaTime;
        currentPos.transform.rotation = Quaternion.Euler(90, 0, 0);

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
