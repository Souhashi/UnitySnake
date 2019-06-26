using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SnakeHandler : MonoBehaviour
{

    Snake snake;
    Food food;
    public int x, y;
    public Tile snaketile;
    public Tilemap tilemap;
    public Tilemap foodmap;
    public int velocity;
    Vector3[] bounds = new Vector3[2];
    public Camera camera;
    int score;
    

    Vector3[] GetBounds(Camera camera)
    {
        Vector3[] b = new Vector3[2];
        for (int i = 0; i <= 1; i++)
        {
            Vector3 p = camera.ViewportToWorldPoint(new Vector3(i, i, camera.nearClipPlane));
            Vector3 g = tilemap.WorldToCell(p);
            b[i] = g;
            Debug.Log(g);
        }

        return b;
    }

    bool isFoodSpawned(Tilemap map)
    {
        for (int i = (int)bounds[0].x; i < (int)bounds[1].x; i++)
        {
            for (int j = (int)bounds[0].y; j < (int)bounds[1].y; j++)
            {
                if (map.HasTile(new Vector3Int(i, j, 0))) {  return true; }
                 
            }
        }
        return false;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Score: " + score);
    }

    void CheckCollision()
    {
        if (isCollided(snake, food))
        {
            food.ConsumeFood();
            snake.AddBlock();
            score += 100;
            SpawnFood(bounds, foodmap);
        }
    }

    bool isCollided(Snake snake, Food food)
    {
        if (snake.getCurrentPos().Equals(food.GetPos())) { return true; }

        return false;
    }

    void SpawnFood(Vector3[] bounds, Tilemap map)
    {
        if (!isFoodSpawned(map))
        {
            food.SetPoint(new Point((int)Random.Range(bounds[0].x, bounds[1].x), (int)Random.Range(bounds[0].y, bounds[1].y)));
            food.DrawFood();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bounds = GetBounds(camera);
        food = new Food(new Point((int)Random.Range(bounds[0].x, bounds[1].x), (int)Random.Range(bounds[0].y, bounds[1].y)), snaketile, foodmap);
        food.DrawFood();
        snake = new Snake(x, y, snaketile, tilemap, velocity, bounds);
        snake.DrawSnake();
        snake.setDirection(Snake.Direction.STOPPED);
        score = 0;
        InvokeRepeating("UpdateSnake", 0.1f, 0.05f);
        //Debug.Log((int)bounds[0].x);
        

    }

    void UpdateSnake()
    {
        //Debug.Log("...");
        snake.Move();
        Debug.Log(snake.getCurrentPos().GetX() +", "+snake.getCurrentPos().GetY());
        
    }

    // Update is called once per frame
    void Update()
    {
        //OnGUI();
        CheckCollision();
        if (Input.GetKey("up"))
        {
            snake.setDirection(Snake.Direction.UP);
        }

        if (Input.GetKey("down"))
        {
            snake.setDirection(Snake.Direction.DOWN);
        }
        if (Input.GetKey("left"))
        {
            snake.setDirection(Snake.Direction.LEFT);
        }
        if (Input.GetKey("right"))
        {
            snake.setDirection(Snake.Direction.RIGHT);

        }
        if (Input.GetKey("space"))
        {
            score = 0;
            snake.ResetSnake();

        }
        if (snake.hasSnakeCollided()) { snake.setDirection(Snake.Direction.STOPPED); }
        
    }
}
