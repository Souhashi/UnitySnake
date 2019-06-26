using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point 
{
    int x;
    int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public bool Equals(Point p)
    {
        if (p.GetX() == x && p.GetY() == y) return true;
        return false;
    }
}
