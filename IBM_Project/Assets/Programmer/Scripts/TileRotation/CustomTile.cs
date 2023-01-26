using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTile
{
    public Vector2 Pos;
    public bool[] Directions;
    public bool Visited;


    public CustomTile(Vector2 Pos, bool[] Directions, bool Visited)
    {
        this.Pos = Pos;
        this.Directions = Directions;
        this.Visited = Visited;
    }
}
