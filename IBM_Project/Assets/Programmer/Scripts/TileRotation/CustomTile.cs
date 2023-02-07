using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTile
{
    //public Vector2 Pos;
    public bool[] Directions;
    public bool Visited;
    //public string Type;
    public tileType type;

    //Temp variabiles for easier testing of concept
    //To replace with bool array if successful
    public bool n;
    public bool s;
    public bool e;
    public bool w;

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
}
