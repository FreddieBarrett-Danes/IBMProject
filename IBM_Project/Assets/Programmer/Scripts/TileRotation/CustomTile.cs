using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTile
{
    //public Vector2 Pos;
    //////public bool[] Directions;
    public bool Visited = false;
    //public string Type;
    public tileType type = tileType.UpDown;

    //Temp variabiles for easier testing of concept
    //To replace with bool array if successful
    public bool n = false;
    public bool s = false;
    public bool e = false;
    public bool w = false;

    public enum tileType
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
        StartRight
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

    public CustomTile(bool n, bool s, bool e, bool w, bool Visited)
    {
        this.n = n;
        this.s = s;
        this.e = e;
        this.w = w;
        this.Visited = Visited;
    }

    public tileType ConvertIntoTile()
    {
        tileType rv = tileType.UpDown;
        
        switch (n, s, e, w)
        {
            case (true, true, false, false):
                rv = tileType.UpDown;
                break;
            case (true, false, true, false):
                rv = tileType.UpRight;
                break;
            case (true, false, false, false):
                rv = tileType.UpLeft;
                break;
            //---
            case (false, true, true, false):
                rv = tileType.DownRight;
                break;
            case (false, true, false, true):
                rv = tileType.DownLeft;
                break;
            //---
            case (false, false, true, true):
                rv = tileType.LeftRight;
                break;
            default:
                Debug.Log("Error! This tile dosen't have exactly two directions!");
                break;
        }
        if (n == true && s == true)
        {
            rv = tileType.UpDown;
        }
        
        return rv;
    }
}
