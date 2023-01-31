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
    Quaternion tileRotation;
    public Camera cam;
    CustomTile[,] Grid;


    // Start is called before the first frame update
    void Start()
    {
        tileRotation = tilePrefab.transform.rotation;
        GridGeneration();
        Grid = new CustomTile[gridWidth, gridHeight];
        Grid[0, 0].type = CustomTile.tileType.StartLeft;
    }

    private void GridGeneration()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity); //Instantiate needs Monobehaviour to function.
                                                                                    //Instantiate(tilePrefab, new Vector3(x / 2.0f, 0, y / 4.0f), Quaternion.identity);
                                                                                    //InstantiatedGameObject.name = "Tile (" + x + "," + y + ")";


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit obj;
            if (Physics.Raycast(ray, out obj))
            {
                obj.transform.Rotate(0, 0, -90);
            }
        }
    }

    //How do you make an object respond to a click in Unity C#
    //https://answers.unity.com/questions/332085/how-do-you-make-an-object-respond-to-a-click-in-c.html
    //Use physics raycast, otherwise required to attach to each tile object

    //private void OnMouseDown()
    //{
    //    Destroy(transform.gameObject);
    //}
}
