using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    // Create the current direction variable
    public enum headDirection{up, down, left, right}
    public headDirection _headDirection = headDirection.down;

    // Runs on start
    private void Start()
    {
        // InvokeRepeating("InputReadout", 0.0f, 0.25f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_headDirection == headDirection.left || _headDirection == headDirection.right)
            {
                _headDirection = headDirection.up;
            }
        } 
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (_headDirection == headDirection.left || _headDirection == headDirection.right)
            {
                _headDirection = headDirection.down;
            }
        } 
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_headDirection == headDirection.up || _headDirection == headDirection.down)
            {
                _headDirection = headDirection.left;
            }
        } 
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (_headDirection == headDirection.up || _headDirection == headDirection.down)
            {
                _headDirection = headDirection.right;
            }
        }
    }

    // Invoked every 1 seconds
    private void InputReadout()
    {
               if (_headDirection == headDirection.up)
        {
            Debug.Log("up");
        } else if (_headDirection == headDirection.down)
        {
            Debug.Log("down");
        } else if (_headDirection == headDirection.left)
        {
            Debug.Log("left");
        } else if (_headDirection == headDirection.right)
        {
            Debug.Log("right");
        }
    }
}
