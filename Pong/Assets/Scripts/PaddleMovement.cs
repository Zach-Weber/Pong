using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public float speed = 2.0f;
    public float yBoundry = 4.0f;
    //private Rigidbody2D RigidBody;
    private Vector3 velocity = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //RigidBody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        // Adjust speed for when the up or down key
        //var velocity = RigidBody.velocity;

        if (Input.GetKey(upKey))
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(downKey))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0;
        }

        transform.position += velocity;

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
        transform.position = position;
    }
}

