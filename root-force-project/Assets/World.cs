using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class World : MonoBehaviour
{
    // Render settings
    // public MeshRenderer meshRenderer;
    // public Material material;
    // public Texture2D texture;
    [SerializeField] public int width;
    [SerializeField] public int height;

    // Cell settings
    [SerializeField] public Sprite[] foodSprites;
    [SerializeField] public Sprite[] rootSprites;
    public enum Cell{empty, boundary, food};
    public Cell[,] grid;

    // Food spawn
    [SerializeField] public int maxNumFood;
    public int currentNumFood;
    public float foodSpawnCounter;
    [SerializeField] public float foodSpawnProbability;
    [SerializeField] public float foodSpawnPeriod;
    [SerializeField] public Transform foodHolder;
    [SerializeField] public Transform rootHolder;

    void Awake()
    {
        grid = new Cell[width, height];

        // Set world boundaries
        for (int i = 0; i < width; i++)
        {
            // Ceiling
            grid[i,0] = Cell.boundary;
            // Floor
            grid[i,height-1] = Cell.boundary;
        }
        for (int i = 1; i < height-1; i++)
        {
            // Left wall
            grid[0,i] = Cell.boundary;
            // Right wall
            grid[width-1,i] = Cell.boundary;
        }

        // Randomly populate with food
        for (int i = 0; i < maxNumFood; i++)
        {
            addFood();
        }
    }

    void Update()
    {
        if (currentNumFood < maxNumFood)
        {
            if (foodSpawnCounter >= foodSpawnPeriod)
            {
                if (Random.Range(0.0f, 1.0f) < foodSpawnProbability)
                {
                    addFood();
                }
            }
            foodSpawnCounter += Time.deltaTime;
        }
    }

    void addFood()
    {
        int x = Random.Range(1, width-1);
        int y = Random.Range(1, height-1);
        while (grid[x,y] != Cell.empty)
        {
            x = Random.Range(1, width-1);
            y = Random.Range(1, height-1);
        }
        grid[x,y] = Cell.food;
        currentNumFood += 1;
        foodSpawnCounter = 0.0f;
        // Instantiate sprite
        GameObject foodSprite = new GameObject($"food_{x}_{y}");
        foodSprite.transform.SetParent(foodHolder);
        int foodSpriteIdx = Random.Range(0, foodSprites.Length);
        foodSprite.AddComponent<SpriteRenderer>().sprite = foodSprites[foodSpriteIdx];
        foodSprite.transform.position = new Vector3(x, y);
        // Debug.Log($"Instantiated sprite {foodSprite.name}");
        foodSprite.tag = "Food";
    }
}
