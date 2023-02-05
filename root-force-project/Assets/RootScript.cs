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
    public int numFoodEaten;
    [SerializeField] public float newRootPeriod;
    [SerializeField] private float minNewRootPeriod;
    [SerializeField] private float maxNewRootPeriod;
    [SerializeField] private float damping;
    [SerializeField] private float boost;
    [SerializeField] private ScoreGauge scoreGauge;
    [SerializeField] private Plant plant;
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;
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
        numFoodEaten = 0;
        newRootTime = 0.0f;
        gameOver = false;
        
        // Set the starting location of the root as a boundary
        //_World.grid = new World.Cell[_World.width, _World.height];
        _World.grid[currentHeadPos_x, currentHeadPos_y] = World.Cell.boundary;

        // Spawn a root sprite on the starting location
        SpawnRoot(currentHeadPos_x, currentHeadPos_y);

        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (gameOver)
            return;
        
        if (newRootPeriod > maxNewRootPeriod)
            GameOver();
        
        // Update time
        newRootTime += Time.deltaTime;

        scoreGauge.SetAngle(newRootPeriod, 0, maxNewRootPeriod);
        // Check if over threshold
        if (newRootTime >= newRootPeriod)
        {
            // Find new x and y coordinates

            switch(_RootController._headDirection)
            {
                case RootController.HeadDirection.up:
                    currentHeadPos_y++;
                    break;
                case RootController.HeadDirection.down:
                    currentHeadPos_y--;
                    break;
                case RootController.HeadDirection.left:
                    currentHeadPos_x--;
                    break;
                case RootController.HeadDirection.right:
                    currentHeadPos_x++;
                    break;
            }
            
            SpawnRoot(currentHeadPos_x, currentHeadPos_y);
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
            newRootPeriod *= 1 + damping;
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
    }

    private void EatFood(int x, int y)
    {
        _World.currentNumFood -= 1;
        GameObject[] activeFoodSprites = GameObject.FindGameObjectsWithTag("Food");
        for (int i = 0; i < activeFoodSprites.Length; i++)
        {
            if (activeFoodSprites[i].name.EndsWith($"{x}_{y}"))
            {
                if (activeFoodSprites[i].name.StartsWith("nutrient"))
                {
                    audioSource.clip = audioClips[Random.Range(1, audioClips.Length)];
                }
                else if (activeFoodSprites[i].name.StartsWith("water"))
                {
                    audioSource.clip = audioClips[0];
                }
                audioSource.Play();
                Destroy(activeFoodSprites[i]);
                break;
            }
        }
        newRootPeriod = Mathf.Max(newRootPeriod / (1 + boost), minNewRootPeriod);
        if (++numFoodEaten % 4 == 0)
            plant.Grow();
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
        SceneManager.LoadScene(0);
    }
}
