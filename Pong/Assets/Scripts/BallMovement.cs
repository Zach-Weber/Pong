using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private float speed = 0.1f;
    private Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
    private float direction = 0.0f; //angle in radians
    private bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            MoveBall();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateStartVelocity();
            gameStart = true;
        }
    }

    void MoveBall()
    {
        transform.position += velocity;

        // check if ball reaches the "score" areas
        if (transform.position.x <= -8.0f)
        {
            velocity.x = velocity.y = 0.0f;
            transform.position = velocity;
            gameStart = false;
            //incrementScore(0);
        }
        else if (transform.position.x >= 8.0f)
        {
            velocity.x = velocity.y = 0.0f;
            transform.position = velocity;
            gameStart = false;
            //incrementScore(1);
        }
    }

    private void CalculateStartVelocity()
    {
        // random angle between 0 and 2pi (radians)
        direction = Random.Range(0.0f, 2.0f * Mathf.PI);

        // can't be straight up or down (no movement towards either side)
        while (direction == 0.5f * Mathf.PI || direction == 1.5f * Mathf.PI)
        {
            direction = Random.Range(0.0f, 2.0f * Mathf.PI);
        }

        //use direction to calculate velocity (speed * direction)
        velocity.x = speed * Mathf.Cos(direction);
        velocity.y = speed * Mathf.Sin(direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //paddle
        if (collision.gameObject.tag == "Paddle")
        {
            velocity.x = -velocity.x;
        }
        //top & bottom walls
        else if (collision.gameObject.tag == "Wall")
        {
            velocity.y = -velocity.y;
        }
    }
}
