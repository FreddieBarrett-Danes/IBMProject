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

    //public CustomTile(Vector2 Pos, bool[] Directions, bool Visited)
    public CustomTile(tileType type, bool Visited)
    {
        //this.Pos = Pos;
        this.type = type;
        this.Visited = Visited;
        type = tileType.UpDown;
    }
}
