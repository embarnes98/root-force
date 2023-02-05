using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    // Create the current direction variable
    public enum HeadDirection{up, down, left, right};
    public HeadDirection _headDirection;
    private float horizontalInput, verticalInput;

    // Runs on start
    private void Awake()
    {
        // InvokeRepeating("InputReadout", 0.0f, 0.25f);
        _headDirection = HeadDirection.down;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_headDirection == HeadDirection.left || _headDirection == HeadDirection.right)
        {
            verticalInput = Input.GetAxis("Vertical");
            if (verticalInput > 0.01f)
                _headDirection = HeadDirection.up;
            else if (verticalInput < -0.01f)
                _headDirection = HeadDirection.down;
        }
        else if (_headDirection == HeadDirection.up || _headDirection == HeadDirection.down)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput > 0.01f)
            {
                _headDirection = HeadDirection.right;
            }
                
            else if (horizontalInput < -0.01f)
            {
                _headDirection = HeadDirection.left;
            } 
        }
    }

    // Invoked every 1 seconds
    private void InputReadout()
    {
               if (_headDirection == HeadDirection.up)
        {
            Debug.Log("up");
        } else if (_headDirection == HeadDirection.down)
        {
            Debug.Log("down");
        } else if (_headDirection == HeadDirection.left)
        {
            Debug.Log("left");
        } else if (_headDirection == HeadDirection.right)
        {
            Debug.Log("right");
        }
    }
}
