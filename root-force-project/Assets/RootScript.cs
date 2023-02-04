using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootScript : MonoBehaviour
{
    // Declare variables
    private int currentHeadPos_x;
    private int currentHeadPos_y;
    public int totalRootCellNumber;
    [SerializeField] public float newRootPeriod;
    [SerializeField] public float maxNewRootPeriod;
    [SerializeField] public float damping;
    [SerializeField] public float boost;
    [SerializeField] public ScoreGauge scoreGauge;
    [SerializeField] public Plant plant;
    private float newRootTime;
    bool gameOver;
    
    // Setup external component reference
    [SerializeField] private World _World;
    private RootController _RootController;

    // On Start Up of Game
    private void Start()
    {
        // Access external component
        _RootController = GetComponent<RootController>();
        
        // Initialize variables
        currentHeadPos_x = _World.width / 2;
        currentHeadPos_y = _World.height - 1;
        // currentHeadPos_x = currentHeadPos_x;
        // currentHeadPos_y = currentHeadPos_y;
        totalRootCellNumber = 1;
        newRootTime = 0.0f;
        gameOver = false;
        
        // Set the starting location of the root as a boundary
        //_World.grid = new World.Cell[_World.width, _World.height];
        _World.grid[currentHeadPos_x, currentHeadPos_y] = World.Cell.boundary;

        // Spawn a root sprite on the starting location
        SpawnRoot(currentHeadPos_x, currentHeadPos_y);
    }

    private void FixedUpdate()
    {
        // Update time
        newRootTime += Time.deltaTime;

        if (gameOver)
        {
            return;
        }

        scoreGauge.SetAngle(newRootPeriod);
        // Check if over threshold
        if (newRootTime >= newRootPeriod)
        {
            // Find new x and y coordinates

            switch(_RootController._headDirection)
            {
                case RootController.headDirection.up:
                    currentHeadPos_y++;
                    break;
                case RootController.headDirection.down:
                    currentHeadPos_y--;
                    break;
                case RootController.headDirection.left:
                    currentHeadPos_x--;
                    break;
                case RootController.headDirection.right:
                    currentHeadPos_x++;
                    break;
            }
            
            SpawnRoot(currentHeadPos_x, currentHeadPos_y);
            if (totalRootCellNumber % 10 == 0)
            {
                plant.Grow();
            }
            switch(_World.grid[currentHeadPos_x, currentHeadPos_y])
            {
                case World.Cell.boundary:
                    GameOver();
                    break;
                case World.Cell.food:
                    EatFood(currentHeadPos_x, currentHeadPos_y);
                    break;
            }

            _World.grid[currentHeadPos_x, currentHeadPos_y] = World.Cell.boundary;
            newRootTime = 0.0f;
            newRootPeriod *= 1 + damping / 10;
            // Debug.Log($"New root period: {newRootPeriod}");
            if (newRootPeriod > maxNewRootPeriod)
            {
                GameOver();
            }
        }
    }

    private void SpawnRoot(int x, int y)
    {
        
        GameObject rootSprite = new GameObject($"root_{x}_{y}");
        rootSprite.transform.SetParent(_World.rootHolder);
        int rootSpriteIdx = Random.Range(0, _World.rootSprites.Length);
        rootSprite.AddComponent<SpriteRenderer>().sprite = _World.rootSprites[rootSpriteIdx];
        rootSprite.transform.position = new Vector3(x, y);
        // Debug.Log($"Instantiated sprite {rootSprite.name}");
        rootSprite.tag = "Root";
        totalRootCellNumber++;
    }

    private void EatFood(int x, int y)
    {
        Debug.Log("Yum yum");
        _World.currentNumFood -= 1;
        GameObject[] activeFoodSprites = GameObject.FindGameObjectsWithTag("Food");
        string foodSpriteName = $"food_{x}_{y}";
        for (int i = 0; i < activeFoodSprites.Length; i++)
        {
            if (activeFoodSprites[i].name == foodSpriteName)
            {
                Destroy(activeFoodSprites[i]);
                break;
            }
        }
        newRootPeriod /= boost + 1;
    }

    private void GameOver()
    {
        gameOver = true;
        while (newRootTime < 5)
        {
            // Insert code to create a game over screen and a restart button--
            // 
            // ---------------------------------------------------------------
            newRootTime += Time.deltaTime;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
