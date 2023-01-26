using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class genGrid : MonoBehaviour
{
    public int gridHeight;
    public int gridWidth;
    //public float tileSize;
    public GameObject tilePrefab;
    Quaternion tileRotation;


    // Start is called before the first frame update
    void Start()
    {
        tileRotation = tilePrefab.transform.rotation;
        GridGeneration();
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
        
    }
}
