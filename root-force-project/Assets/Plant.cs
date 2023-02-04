using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private int treeGrowthIdx;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        treeGrowthIdx = 0;
    }
    
    public void Grow()
    {
        treeGrowthIdx += 1;
        Debug.Log($"Growth idx: {treeGrowthIdx}");
        if (treeGrowthIdx < sprites.Length)
        {
            spriteRenderer.sprite = sprites[treeGrowthIdx];
        }
    }
}
