using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTile
{
    //public Vector2 Pos;
    //////public bool[] Directions;
    public bool visited = false;
    //public string Type;
    public TileType type = TileType.UpDown;

    public GameObject gameObjectFront; //The interactable tiles in front of the camera
    public GameObject gameObjectBack; //A set of tiles out of the camera's view which references the correct solution

    public Quaternion frontRotation;
    public Vector3 frontRotationEuler;
    public Quaternion backRotation;
    public Vector3 backRotationEuler;

    //Temp variabiles for easier testing of concept
    //To replace with bool array if successful
    public bool n = false;
    public bool s = false;
    public bool e = false;
    public bool w = false;

    public bool correctRotation = false;

    public enum TileType
    {
        UpDown,
        LeftRight,
        //--
        DownRight,
        DownLeft,
        UpLeft,
        UpRight,
        //---
        //---
        StartUp,
        StartDown,
        StartLeft,
        StartRight,
        //---
        Blank
    }

    //public CustomTile(bool[] Directions, bool Visited)
    //public CustomTile(tileType type, bool Visited)
    //{
    //    //this.Pos = Pos;
    //    this.type = type;
    //    //this.Directions = Directions;
    //    this.Visited = Visited;
    //    type = tileType.UpDown;
    //}

    public CustomTile(bool n, bool s, bool e, bool w, bool visited)
    {
        this.n = n;
        this.s = s;
        this.e = e;
        this.w = w;
        this.visited = visited;
    }

    public CustomTile()
    {
        this.n = false;
        this.s = false;
        this.e = false;
        this.w = false;
        this.visited = false;
        this.correctRotation = false;
        this.gameObjectFront = GameObject.Find("Tile_Blank");
    }

    public TileType ConvertIntoTile(short num1)
    {
        TileType rv = TileType.UpDown;
        //Debug.Log("[" + num1 + "]" + "Before switch: " + n + "," + s + "," + e + "," + w);
                                                        //up       down     right     left
        
        switch (n, s, e, w)
        {
            //Double direction tiles:
            case (true, true, false, false):
                rv = TileType.UpDown;
                break;
            case (true, false, true, false):
                rv = TileType.UpRight;
                break;
            case (true, false, false, true):
                rv = TileType.UpLeft;
                break;
            //---
            case (false, true, true, false):
                rv = TileType.DownRight;
                break;
            case (false, true, false, true):
                rv = TileType.DownLeft;
                break;
            //---
            case (false, false, true, true):
                rv = TileType.LeftRight;
                break;
            //---
            //---
            //Single direction tiles:
            case (true, false, false, false):
                rv = TileType.StartUp;
                break;
            case (false, true, false, false):
                rv = TileType.StartDown;
                break;
            case (false, false, true, false):
                rv = TileType.StartRight;
                break;
            case (false, false, false, true):
                rv = TileType.StartLeft;
                break;

            default:
                //Debug.Log("[" + num1 + "]" + "Error! This tile dosen't have exactly two directions! " + n + "," + s + "," + e + "," + w);
                break;
        }
        //if (n == true && s == true)
        //{
        //    rv = tileType.UpDown;
        //}
        
        return rv;
    }
}
