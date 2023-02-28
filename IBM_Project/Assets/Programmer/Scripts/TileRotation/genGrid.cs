using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Code inspired by Little Arch Games
//"Generate 2D Isometric Grids By Code" (2021) https://www.youtube.com/watch?v=fmVJqt3aSdE


//3D Rotation Converter: https://www.andre-gaschler.com/rotationconverter/


public class genGrid : MonoBehaviour
{
    public int gridHeight;
    public int gridWidth;
    //public float tileSize;
    public GameObject tilePrefab;
    GameObject tilePrefab2;
    Quaternion tileRotation;
    private Camera cam;
    public GameObject pregameText;
    
    public bool showCorrectRotation;
    public bool showTutorial;

    //Following variables/objects used to be public
    CustomTile[,] gridarray;
    CustomTile[] grid2;
    bool[,] boolarraytest;
    Vector2 cPos; // Current Position (within Grid array)
    bool gridCompletion = false;
    private Color startColor;
    //int ij = 0;

    private GameController gC;

    // Start is called before the first frame update
    void Start()
    {
        //gridCompletion = false;
        //tileRotation = tilePrefab.transform.rotation;
        tilePrefab2 = GameObject.Find("Tile_UpDown");

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        grid2 = new CustomTile[3];
        gridarray = new CustomTile[gridWidth, gridHeight];
        //gridarray = new CustomTile[gridWidth, gridHeight];
        //int[] array1 = new int[] { 0, 0, 0, 0, 0 };

        CustomTile tile1 = new CustomTile(false,false,false,false, false);
        tile1.n = true;
        CustomTile tile2 = new CustomTile();

        startColor = tilePrefab2.GetComponent<Renderer>().material.color;


        CustomTile[] grid3 = new CustomTile[] { tile1, tile2 };

        boolarraytest = new bool[3, 3];

        boolarraytest[1, 1] = true;

        GameObject.Find("TutorialBackground").GetComponent<MeshRenderer>().enabled = showTutorial;
        pregameText.GetComponent<TextMeshProUGUI>().enabled = showTutorial;




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
        while (gridCompletion == false)
        {
            GridMovement();
        }

        //Replace every unvisited tile as blank
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y ++)
            {
                if (gridarray[x, y].Visited == false)
                {
                    gridarray[x, y].gameObjectBack = Instantiate(GameObject.Find("Tile_Blank"), new Vector3(x, y, 0), GameObject.Find("Tile_Blank").transform.rotation);
                    gridarray[x, y].gameObjectFront = Instantiate(GameObject.Find("Tile_Blank"), new Vector3(x, y, 0), GameObject.Find("Tile_Blank").transform.rotation);
                    //Instantiate(GameObject.Find("Tile_Blank"), new Vector3(x, y, 0), GameObject.Find("Tile_Blank").transform.rotation);
                    //Instantiate(GameObject.Find("Tile_Blank"), new Vector3(x, y, -20), GameObject.Find("Tile_Blank").transform.rotation);
                    //gridarray[x, y].gameObjectBack = GameObject.Find("Tile_Blank");
                    //gridarray[x, y].gameObjectBack.transform.position = new Vector3(x, y, -20);

                    //gridarray[x, y].gameObjectFront = GameObject.Find("Tile_Blank");
                    //gridarray[x, y].gameObjectFront.transform.position = new Vector3(x, y, 0);
                    //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack = tilePrefab2;
                    //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack.transform.position = new Vector3(cPos.x, cPos.y, -20);

                }
            }
        }
        //Debug.Log("before tileCheck");
        //tileCheck();
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
        /*
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(1) == CustomTile.tileType.UpDown) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_UpDown"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(2) == CustomTile.tileType.UpLeft) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_UpLeft"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(3) == CustomTile.tileType.UpRight) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_UpRight"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(4) == CustomTile.tileType.DownLeft) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_DownLeft"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(5) == CustomTile.tileType.DownRight) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_DownRight"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(6) == CustomTile.tileType.LeftRight) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_LeftRight"); }

        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(7) == CustomTile.tileType.StartUp) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_StartUp"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(8) == CustomTile.tileType.StartLeft) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_StartLeft"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(9) == CustomTile.tileType.StartRight) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_StartRight"); }
        if (gridarray[(int)cPos.x, (int)cPos.y].ConvertIntoTile(10) == CustomTile.tileType.StartDown) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = GameObject.Find("Tile_StartDown"); }
        */


        
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
    private void DoubleInstantiate()
    {
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack = tilePrefab2;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack.transform.position = new Vector3(cPos.x, cPos.y, -20);


        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = tilePrefab2;

        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack = tilePrefab2;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack.transform.position = new Vector3(cPos.x, cPos.y, -20);
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = tilePrefab2;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.position = new Vector3(cPos.x, cPos.y, 0);
        int rng2 = Random.Range(0, 4);
        Vector3 randRotation = new Vector3(0,0,0);
        Quaternion quatRotation = new Quaternion(0,0,0,0);
        switch (rng2)
        {
            case 1:
                randRotation = new Vector3(0, 0, 0);
                quatRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f); //tilePrefab2.transform.rotation; //Quaternion.identity;
                break;
            case 2:
                randRotation = new Vector3(0, 0, 90);
                quatRotation = new Quaternion(0.0f, 0.0f, 0.7071068f, 0.7071068f);
                break;
            case 3:
                randRotation = new Vector3(0, 0, 180);
                quatRotation = new Quaternion(0.0f, 0.0f, 1.0f, 0.0f);
                break;
            case 4:
                randRotation = new Vector3(0, 0, 270);
                quatRotation = new Quaternion(0, 0, 0.7071068f, -0.7071068f);
                break;
            default:
                randRotation = new Vector3(0, 0, 0);
                quatRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
                break;
        }
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation = quatRotation;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = tilePrefab2;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.position = new Vector3(cPos.x, cPos.y, 0);


        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation = quatRotation; //For whatever reason, using quatRotation sets the randomised direction for the back set of tiles

        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation = quatRotation;



        //Generate correct path in front of the camera:
        //----------------------------------------------
        //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);


        //Generate path in front of the camera with randomised rotations:
        //----------------------------------------------------------------
        //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), quatRotation);
        if (cPos == new Vector2(0, 0) || gridarray[(int)cPos.x, (int)cPos.y].type == CustomTile.tileType.StartUp) { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation); }
        else { gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), quatRotation); }

        //Generate correct path behind the camera:
        //----------------------------------------
        //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, -20), tilePrefab2.transform.rotation);
        gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack = Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, -20), tilePrefab2.transform.rotation);

    }

    private void OldDoubleInstantiate() //For reference and as a backup
    {
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack = tilePrefab2;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack.transform.position = new Vector3(cPos.x, cPos.y, -20);


        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = tilePrefab2;
        int rng2 = Random.Range(0, 4);
        Vector3 randRotation = new Vector3(0, 0, 0);
        Quaternion quatRotation = new Quaternion(0, 0, 0, 0);
        switch (rng2)
        {
            case 1:
                randRotation = new Vector3(0, 0, 0);
                quatRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f); //tilePrefab2.transform.rotation; //Quaternion.identity;
                break;
            case 2:
                randRotation = new Vector3(0, 0, 90);
                quatRotation = new Quaternion(0.0f, 0.0f, 0.7071068f, 0.7071068f);
                break;
            case 3:
                randRotation = new Vector3(0, 0, 180);
                quatRotation = new Quaternion(0.0f, 0.0f, 1.0f, 0.0f);
                break;
            case 4:
                randRotation = new Vector3(0, 0, 270);
                quatRotation = new Quaternion(0, 0, 0.7071068f, -0.7071068f);
                break;
            default:
                randRotation = new Vector3(0, 0, 0);
                quatRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
                break;
        }
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront = tilePrefab2;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.position = new Vector3(cPos.x, cPos.y, 0);


        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation = quatRotation; //For whatever reason, using quatRotation sets the randomised direction for the back set of tiles

        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation = quatRotation;



        //Generate correct path in front of the camera:
        //----------------------------------------------
        //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);


        //Generate path in front of the camera with randomised rotations:
        //----------------------------------------------------------------
        //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.Rotate(randRotation, Space.World));

        Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), quatRotation);
        //Instantiate(gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront, new Vector3(cPos.x, cPos.y - 20), quatRotation);
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation = quatRotation;
        //Instantiate(gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront, new Vector3(cPos.x, cPos.y, 0), gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation);

        //Generate correct path behind the camera:
        //----------------------------------------
        Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, -20), tilePrefab2.transform.rotation);
        //Instantiate(gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack, new Vector3(cPos.x, cPos.y, -20), gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack.transform.rotation);

        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation = tilePrefab2.transform.rotation;
        //gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation = quatRotation;

        //gridarray[(int)cPos.x, (int)cPos.y].FrontRotation = quatRotation;
        //gridarray[(int)cPos.x, (int)cPos.y].BackRotation = tilePrefab2.transform.rotation;

        //gridarray[(int)cPos.x, (int)cPos.y].BackRotationEuler = tilePrefab2.transform.rotation.eulerAngles;
        //gridarray[(int)cPos.x, (int)cPos.y].FrontRotationEuler = quatRotation.eulerAngles;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(gridWidth / 2, gridHeight / 2, 0), new Vector3(gridWidth, gridHeight, 2));
        Gizmos.DrawWireCube(new Vector3(gridWidth / 2, gridHeight / 2, -20), new Vector3(gridWidth, gridHeight, 2));
    }

    private void tileCheck()
    {
        bool[,] correctRotation = new bool[100,100];
        short counter1 = 0;
        bool finishLock = false;
        //Debug.Log(gridarray[1, 1].FrontRotation + "," + gridarray[1, 1].BackRotation);
        //Debug.Log(gridarray[1, 1].FrontRotationEuler + "," + gridarray[1, 1].gameObjectBack.transform.rotation.eulerAngles);
        for (int x = 0; x < (gridWidth); x++)
        {
            
            for (int y = 0; y <= (gridHeight - 1); y++)
            {
                //GameObject.Destroy(gridarray[x, y].gameObjectFront);
                //Debug.Log(gridarray[x, y].gameObjectFront.transform.rotation + "," + gridarray[x, y].gameObjectBack.transform.rotation);
                //if (gridarray[x, y].gameObjectFront.transform.rotation == gridarray[x, y].gameObjectBack.transform.rotation)


                //if (gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles)


                //If GameObject at front is equal to GameObject at back OR (If GameObject at back is UpDown AND GameObject rotation is inverted (180 difference in z rotation))                                 //gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles ||
                //This is to account for the UpDown tile featuring two outputs but four rotations
                if (gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles || (gridarray[x, y].gameObjectBack.name == "Tile_UpDown(Clone)" || gridarray[x,y].gameObjectBack.name == "Tile_LeftRight(Clone)" && (gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == (gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles + new Vector3(0,0,180)) || (gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles) == (gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles - new Vector3(0, 0, 180)) || gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles)))
                //if (gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles || (gridarray[x, y].gameObjectBack.name == "Tile_UpDown" && (gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == (gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles + new Vector3(0, 0, 180)) || (gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles) == (gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles - new Vector3(0, 0, 180)) || gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles)))
                {
                    gridarray[x, y].gameObjectFront.transform.position = new Vector3(gridarray[x, y].gameObjectFront.transform.position.x, gridarray[x, y].gameObjectFront.transform.position.y, 1);
                    if (showCorrectRotation == true) { gridarray[x, y].gameObjectFront.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f); }
                    
                    //if (gridarray[x,y].gameObjectBack.name == "Tile_UpDown")
                    //{
                    //    if (gridarray[x, y].gameObjectBack.name == "Tile_UpDown" && (gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles || gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles == -gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles))
                    //    {

                    //    }
                    //}
                    correctRotation[x, y] = true;
                    counter1++;
                    Debug.Log(x + "," + y + "|" + counter1);
                }
                else
                {
                    Debug.Log(gridarray[x,y].gameObjectFront.name + "," + gridarray[x,y].gameObjectBack.name + " | " + gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles + " - " + (gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles + new Vector3(0, 0, 180)) + " - " + (gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles - new Vector3(0, 0, 180)));
                    Debug.Log("INCORRECT ROTATION AT: " + x + "," + y + " | " + gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles + "," + gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles);
                    finishLock = true;
                }
                
                //if (gridarray[x, y].gameObjectFront.transform.rotation != gridarray[x, y].gameObjectBack.transform.rotation)
                //if (gridarray[x, y].gameObjectFront.transform.rotation.eulerAngles != gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles)
                //{
                //    Debug.Log("INCORRECT ROTATION AT: " + x + "," + y + " | " + gridarray[x,y].gameObjectFront.transform.rotation.eulerAngles + "," + gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles);
                //    finishLock = true;
                //}

                //Debug.Log("(" + x + "," + y + ")" + gridarray[x, y].FrontRotationEuler + "," + gridarray[x, y].gameObjectBack.transform.rotation.eulerAngles);
                //Debug.Log("(" + x + "," + y + ") " + gridarray[x, y].gameObjectFront.transform.rotation + "," + gridarray[x, y].gameObjectBack.transform.rotation);
            }
        }
        if (finishLock == false)
        {
            Debug.Log("Grid has been rotated correctly. Tile Rotation minigame complete, refer to tileCheck() for output");
            gC.mC.completedDoor = true;
            gC.inMinigame = false;
        }
        else
        {
            Debug.Log(" Incorrect rotation for at least one tile, try again. finishLock: " + finishLock);
        }

                //Debug.Log("(" + cPos.x + "," + cPos.y + ") " + gridarray[(int)cPos.x, (int)cPos.y].gameObjectFront.transform.rotation + "," + gridarray[(int)cPos.x, (int)cPos.y].gameObjectBack.transform.rotation);
        /*
        Debug.Log("tilecheck function");
        Collider[] testCollide1 = Physics.OverlapBox(new Vector3(gridWidth / 2, gridHeight / 2, 0), new Vector3(gridWidth / 2, gridHeight / 2, 1.0f / 4));//, Quaternion.identity);
        Collider[] testCollide2 = Physics.OverlapBox(new Vector3(gridWidth / 2, gridHeight / 2, -20), new Vector3(gridWidth / 2, gridHeight / 2, 1.0f / 4));//, Quaternion.identity);
        bool[] correctRotation = new bool[1000];

        //int i = 0;
        //int j = 0;
        //float angle = 0.0f;
        
        if (ij == gridWidth * gridHeight) { ij = 0; }
        
        if (testCollide1[ij].transform.rotation == testCollide2[ij].transform.rotation) { correctRotation[ij] = true; }
        else { correctRotation[ij] = false; }
        testCollide1[ij].transform.position += new Vector3(0, 0, 0.5f);
        testCollide2[ij].transform.position += new Vector3(0, 0, 0.5f);
        Debug.Log(ij + " | " + testCollide1[ij].transform.position + "," + testCollide1[ij].transform.rotation + " , " + testCollide2[ij].transform.position + "," + testCollide2[ij].transform.rotation + ": " + correctRotation[ij]);
        ij++;
        */
       

        /*
        while (i < testCollide1.Length)
        {
            //Debug.Log(testCollide1[i].transform.position);
            //Debug.Log(testCollide2[i].transform.position);
            if (testCollide1[i].transform.rotation == testCollide2[i].transform.rotation) { correctRotation[i] = true; }
            else { correctRotation[i] = false; }
            Debug.Log(i + " | " + testCollide1[i].transform.position + "," + testCollide2[i].transform.position + ": " + correctRotation[i]);
            i++;
        }
        */

        //while (j < testCollide2.Length)
        //{
        //    Debug.Log(testCollide2[j].transform.position);
        //    j++;
        //}
        //angle = Vector3.SignedAngle(testCollide1[i].transform.position, testCollide2[j].transform)

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                
                //Physics.OverlapBox
            }
        }
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
                DoubleInstantiate();
                //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
                cPos.y += 1;
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].s = true;
                break;
            case 2:
                //move south
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].s = true;
                SetPrefab();
                DoubleInstantiate();
                //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
                cPos.y -= 1;
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].n = true;
                break;
            case 3:
                //move east
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].e = true;
                SetPrefab();
                DoubleInstantiate();
                //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
                cPos.x += 1;
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].w = true;
                break;
            case 4:
                //move west
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].w = true;
                SetPrefab();
                DoubleInstantiate();
                //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
                cPos.x -= 1;
                gridarray[(int)cPos.x, (int)cPos.y].Visited = true;
                gridarray[(int)cPos.x, (int)cPos.y].e = true;
                break;
            case 5:
                //Mark grid path as complete
                gridCompletion = true;
                SetPrefab();
                DoubleInstantiate();
                //Instantiate(tilePrefab2, new Vector3(cPos.x, cPos.y, 0), tilePrefab2.transform.rotation);
                break;
        }
    }

    private void updateRotation()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {

            }
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
            //Quaternion objRotation = 0;
            if (Physics.Raycast(ray, out obj))
            {
                //Debug.Log(gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectFront.transform.rotation);
                //Debug.Log(gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectFront.transform.rotation.eulerAngles);
                obj.transform.Rotate(0, 0, -90);
                Debug.Log(obj.transform.position.x + "," + obj.transform.position.y);
                //Debug.Log(gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectFront.transform.rotation.eulerAngles + " | " + gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectBack.transform.rotation.eulerAngles);
                //Disc1.GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0.5f);
                if (gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectFront.transform.rotation.eulerAngles == gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectBack.transform.rotation.eulerAngles)
                {
                    if (showCorrectRotation == true) { gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectFront.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f); }
                }
                else
                {
                    gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectFront.GetComponent<Renderer>().material.color = startColor;
                }
                

                //Debug.Log(gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectFront.transform.rotation.eulerAngles);
                //Debug.Log(gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectFront.transform.rotation);
                //gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].gameObjectFront.transform.rotation = obj.transform.rotation;
                //gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].FrontRotationEuler = obj.transform.rotation.eulerAngles;
                //objRotation = obj.transform.rotation;
                //gridarray[(int)obj.transform.position.x, (int)obj.transform.position.y].FrontRotation += Quaternion.Euler(0,0,0);

            }
        }

        //For debugging, completes minigame instantly.
        if (Input.GetKey("p"))
        {
            for (int x = 0; x < (gridWidth); x++)
            {

                for (int y = 0; y <= (gridHeight - 1); y++)
                {
                    gridarray[x, y].gameObjectFront.transform.rotation = gridarray[x, y].gameObjectBack.transform.rotation;
                    GameObject.Find("TutorialBackground").GetComponent<MeshRenderer>().enabled = false;
                    pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
                    tileCheck();

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("TutorialBackground").GetComponent<MeshRenderer>().enabled = false;
            pregameText.GetComponent<TextMeshProUGUI>().enabled = false;
            tileCheck();
            //GridMovement();
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
