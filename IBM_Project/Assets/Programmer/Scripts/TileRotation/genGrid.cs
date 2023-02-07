using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Code inspired by Little Arch Games
//"Generate 2D Isometric Grids By Code" (2021) https://www.youtube.com/watch?v=fmVJqt3aSdE


public class genGrid : MonoBehaviour
{
    public int gridHeight;
    public int gridWidth;
    //public float tileSize;
    public GameObject tilePrefab;
    GameObject tilePrefab2;
    Quaternion tileRotation;
    public Camera cam;
    public CustomTile[,] gridarray;
    public CustomTile[] grid2;
    public bool[,] boolarraytest;
    public Vector2 cPos; // Current Position (within Grid array)
    public bool gridCompletion = false;


    // Start is called before the first frame update
    void Start()
    {
        //gridCompletion = false;
        //tileRotation = tilePrefab.transform.rotation;
        tilePrefab2 = GameObject.Find("Tile_UpDown");

        grid2 = new CustomTile[3];
        gridarray = new CustomTile[gridWidth, gridHeight];
        //gridarray = new CustomTile[gridWidth, gridHeight];
        //int[] array1 = new int[] { 0, 0, 0, 0, 0 };

        CustomTile tile1 = new CustomTile(false,false,false,false, false);
        tile1.n = true;
        CustomTile tile2 = new CustomTile();


        CustomTile[] grid3 = new CustomTile[] { tile1, tile2 };

        boolarraytest = new bool[3, 3];

        boolarraytest[1, 1] = true;

        


        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                //Debug.Log(x + "," + y);
                gridarray[x, y] = new CustomTile();
                //gridarray[x, y].n = false;
                //gridarray[x, y].s = false;
                //gridarray[x, y].e = false;
                //gridarray[x, y].w = false;
                //gridarray[x, y].Visited = false;
                //gridarray[x, y].type = CustomTile.tileType.UpDown;
            }
        }

        cPos = new Vector2(0, 0);
        Debug.Log("start Cpos " + cPos.x + "," + cPos.y);
        //gridarray[0, 0].w = true;
        gridarray[0, 0].Visited = true;
        gridarray[0, 0].s = true;
        GridMovement();
        //vvv
        //SetPrefab();
        //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
        //^^^
        
        //GridMovement();
        //SetPrefab();
        //Debug.Log("Prefab: " + tilePrefab2);
        //Instantiate(tilePrefab2, new Vector3(0, 0, 0), tilePrefab2.transform.rotation);//Quaternion.identity);
        //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);//Quaternion.identity);

        //gridarray[1, 1].type = CustomTile.tileType.StartLeft;

        //GridGeneration();
    }

    private void GridGeneration()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                
                //Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity); //Instantiate needs Monobehaviour to function.
                                                                                    //Instantiate(tilePrefab, new Vector3(x / 2.0f, 0, y / 4.0f), Quaternion.identity);
                                                                                    //InstantiatedGameObject.name = "Tile (" + x + "," + y + ")";

                //Instantiate(UpDownPrefab, new Vector3(cPos.x, cPos.y, 0), Quaternion.identity);
                
            }
        }

        //Grid[(int)cPos.x, (int)cPos.y].e = true;
        //gridarray[0, 0].s = false;
        //Instantiate(GameObject.Find("Tile_DownLeft"), new Vector3(cPos.x, cPos.y, 0), Quaternion.identity);
        Debug.Log("test2");

        short counter = 0;

        while (gridCompletion == false) //(counter >= 9)
        {


            GridMovement();
            if (gridCompletion == false)
            {
                SetPrefab();
                Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation); //Quaternion.identity);
                counter++;
                Debug.Log("Counter: " + counter);
            }
            //if (gridarray[(int)cPos.x - 1, (int)cPos.y].w == true)
            //{ gridarray[(int)cPos.x, (int)cPos.y].e = true; }
            //if (gridarray[(int)cPos.x + 1, (int)cPos.y].e == true)
            //{ gridarray[(int)cPos.x, (int)cPos.y].w = true; }
            //if (gridarray[(int)cPos.x, (int)cPos.y + 1].s == true)
            //{ gridarray[(int)cPos.x, (int)cPos.y].n = true; }
            //if (gridarray[(int)cPos.x, (int)cPos.y - 1].n == true)
            //{ gridarray[(int)cPos.x, (int)cPos.y].s = true; }

        }

    }

    private void SetPrefab()
    {
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(1) == CustomTile.tileType.UpDown) { tilePrefab2 = GameObject.Find("Tile_UpDown"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(2) == CustomTile.tileType.UpLeft) { tilePrefab2 = GameObject.Find("Tile_UpLeft"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(3) == CustomTile.tileType.UpRight) { tilePrefab2 = GameObject.Find("Tile_UpRight"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(4) == CustomTile.tileType.DownLeft) { tilePrefab2 = GameObject.Find("Tile_DownLeft"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(5) == CustomTile.tileType.DownRight) { tilePrefab2 = GameObject.Find("Tile_DownRight"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(6) == CustomTile.tileType.LeftRight) { tilePrefab2 = GameObject.Find("Tile_LeftRight"); }

        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(7) == CustomTile.tileType.StartUp) { tilePrefab2 = GameObject.Find("Tile_StartUp"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(8) == CustomTile.tileType.StartLeft) { tilePrefab2 = GameObject.Find("Tile_StartLeft"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(9) == CustomTile.tileType.StartRight) { tilePrefab2 = GameObject.Find("Tile_StartRight"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(10) == CustomTile.tileType.StartDown) { tilePrefab2 = GameObject.Find("Tile_StartDown"); }
    }

    private void GridMovement()
    {
        //n
      //w //e
        //s


        //Check for adjacent unvisited cells

        int[] arrayvar = new int[] { 0, 0, 0, 0 };
        //n, s, e, w

        Debug.Log(cPos.x + "," + cPos.y);   

        //check north
        if (cPos.y != (gridHeight - 1))
        {
            if (gridarray[(int)cPos.x, (int)cPos.y + 1].Visited == false)
            { arrayvar[0] = 1; }
        }

        //check south
        if (cPos.y != 0)
        {
            if (gridarray[(int)cPos.x, (int)cPos.y - 1].Visited == false)
            { arrayvar[1] = 2; }
        }

        //check east
        if (cPos.x != (gridWidth - 1))
        {
            if (gridarray[(int)cPos.x + 1, (int)cPos.y].Visited == false)
            { arrayvar[2] = 3; }
        }

        //check west
        if (cPos.x != 0)
        {
            if (gridarray[(int)cPos.x - 1, (int)cPos.y].Visited == false)
            { arrayvar[3] = 4; }
        }

        int rng = 0;
        if (arrayvar[0] == 0 && arrayvar[1] == 0 && arrayvar[2] == 0 && arrayvar[3] == 0)
        {
            rng = 5;
        }

        if (rng == 0)
        {
            while (rng == 0)
            {
                rng = arrayvar[Random.Range(0, 4)];

            }
        }

        switch (rng)
        {
            case 1:
                //move north
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].n = true;
                SetPrefab();
                Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
                cPos.y += 1;
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].s = true;
                break;
            case 2:
                //move south
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].s = true;
                SetPrefab();
                Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
                cPos.y -= 1;
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].n = true;
                break;
            case 3:
                //move east
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].e = true;
                SetPrefab();
                Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
                cPos.x += 1;
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].w = true;
                break;
            case 4:
                //move west
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].w = true;
                SetPrefab();
                Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
                cPos.x -= 1;
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].e = true;
                break;
            case 5:
                //Mark grid path as complete
                gridCompletion = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //short counter;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit obj;
            if (Physics.Raycast(ray, out obj))
            {
                obj.transform.Rotate(0, 0, -90);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GridMovement();
            //if (gridCompletion == false)
            //{
            //    SetPrefab();
            //    Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation); // Quaternion.identity);
            //    //counter++;
            //    //Debug.Log("Counter: " + counter);
            //}
        }

        //Space input for debug, to be removed once generation is refined.
    }

    //How do you make an object respond to a click in Unity C#
    //https://answers.unity.com/questions/332085/how-do-you-make-an-object-respond-to-a-click-in-c.html
    //Use physics raycast, otherwise required to attach to each tile object

    //private void OnMouseDown()
    //{
    //    Destroy(transform.gameObject);
    //}
}
