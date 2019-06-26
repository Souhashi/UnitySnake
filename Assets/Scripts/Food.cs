using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Food 
{
    Point pos;
    Tile tile;
    Tilemap grid;
    public Food(Point p, Tile t, Tilemap g)
    {
        pos = p;
        tile = t;
        grid = g;
    }

    public void DrawFood()
    {
        grid.SetTile(new Vector3Int(pos.GetX(), pos.GetY(), 0), tile);
    }

    public void SetPoint(Point p)
    {
        pos = p;
    }

    public void ConsumeFood()
    {
        grid.SetTile(new Vector3Int(pos.GetX(), pos.GetY(), 0), null);
       
    }

    public Point GetPos() { return pos; }
}
