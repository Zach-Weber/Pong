using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public float speed = 2.0f;
    public float yBoundry = 4.0f;
    private Rigidbody2D RigidBody;


    // Start is called before the first frame update
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        // Adjust speed for when the up or down key
        var velocity = RigidBody.velocity;

        if (Input.GetKeyDown(upKey))
        {
            velocity.y = speed;
        }
        else if (Input.GetKeyDown(downKey))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0;
        }


        // Check for if the paddle is above or below set Y Boundry
        var position = transform.position;
        if (position.y > yBoundry)
        {
            position.y = yBoundry;
        }
        else if (position.y < -yBoundry)
        {
            position.y = yBoundry;
        }
    }
}

