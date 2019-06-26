using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Snake
{
    List<Point> points;
    Tile tile;
    Tilemap Grid;
    int velocity, x, y;
    int initialSize = 3;
    public enum Direction { UP, DOWN, LEFT, RIGHT, STOPPED };
    Direction currentDirection;
    Vector3[] bounds;

    public Snake(int x, int y, Tile tile, Tilemap tilemap, int velocity, Vector3[] bounds)
    {
        points = new List<Point>();
        this.x = x;
        this.y = y;
        this.tile = tile;
        Grid = tilemap;
        this.velocity = velocity;
        currentDirection = Direction.STOPPED;
        this.bounds = bounds;
        CreateSnake(x, y);
    }

    void CreateSnake(int x, int y)
    {
        Debug.Log("I have run!");
        for (int i = 0; i < initialSize; i++)
        {
            points.Add(new Point(x - i, y));
        }
        Debug.Log(points.Count);
    }

    public void DrawSnake()
    {
        Debug.Log("Niaaah");
        for (int i = 0; i < points.Count; i++)
        {
            Grid.SetTile(new Vector3Int(points[i].GetX(), points[i].GetY(), 0), tile);
        }
    }

    public Direction GetDirection()
    {
        return currentDirection;
    }

    public void setDirection(Direction dir)
    {
        currentDirection = dir;
    }

    public void ResetSnake()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Grid.SetTile(new Vector3Int(points[i].GetX(), points[i].GetY(), 0), null);
        }
        points.Clear();
        CreateSnake(x, y);
        currentDirection = Direction.STOPPED;
        DrawSnake();
    }

    public Point getCurrentPos()
    {
        return points[0];
    }

    bool isPointOnSnake()
    {
        for (int i = 1; i < points.Count; i++)
        {
            if (points[i].Equals(points[0])) return true;
        }
        return false;
    }

    public bool hasSnakeCollided()
    {
        if (points[0].GetX() < bounds[0].x || points[0].GetY() > bounds[1].y || points[0].GetX() > bounds[1].x || points[0].GetY() < bounds[0].y)
        {
            return true;
        }
        if (isPointOnSnake()) { return true; }
        return false;
    }

    public void AddBlock()
    {
        points.Add(new Point(points[points.Count - 1].GetX() + 1, points[points.Count - 1].GetY()));
    }

    public void Move()
    {
        int lastx, lasty, newx, newy;
        switch (currentDirection)
        {
            case Direction.UP:
                lastx = points[points.Count - 1].GetX();
                lasty = points[points.Count - 1].GetY();
                Grid.SetTile(new Vector3Int(lastx, lasty, 0), null);
                points.RemoveAt(points.Count - 1);
                newx = points[0].GetX();
                newy = points[0].GetY() + velocity;
                Grid.SetTile(new Vector3Int(newx, newy , 0), tile);
                points.Insert(0, new Point(newx, newy));
                break;
            case Direction.DOWN:
                lastx = points[points.Count - 1].GetX();
                lasty = points[points.Count - 1].GetY();
                Grid.SetTile(new Vector3Int(lastx, lasty, 0), null);
                points.RemoveAt(points.Count - 1);
                newx = points[0].GetX();
                newy = points[0].GetY() - velocity;
                Grid.SetTile(new Vector3Int(newx, newy, 0), tile);
                points.Insert(0, new Point(newx, newy));
                break;
            case Direction.LEFT:
                lastx = points[points.Count - 1].GetX();
                lasty = points[points.Count - 1].GetY();
                Grid.SetTile(new Vector3Int(lastx, lasty, 0), null);
                points.RemoveAt(points.Count - 1);
                newx = points[0].GetX() - velocity;
                newy = points[0].GetY();
                Grid.SetTile(new Vector3Int(newx, newy, 0), tile);
                points.Insert(0, new Point(newx, newy));
                break;
            case Direction.RIGHT:
                lastx = points[points.Count - 1].GetX();
                lasty = points[points.Count - 1].GetY();
                Grid.SetTile(new Vector3Int(lastx, lasty, 0), null);
                points.RemoveAt(points.Count - 1);
                newx = points[0].GetX() + velocity;
                newy = points[0].GetY();
                Grid.SetTile(new Vector3Int(newx, newy, 0), tile);
                points.Insert(0, new Point(newx, newy));
                break;

        }
    }

    
}
