using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private float speed = 0.1f;
    private float offset = Mathf.PI / 12; //offset of 15 degrees
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

        
    }

    private void CalculateStartVelocity()
    {
        // random angle between 0 and 2pi (radians)
        direction = Random.Range(0.0f, 2.0f * Mathf.PI);

        // can't be straight up or down (no movement towards either side)
        //"wiggle room" of 15 degrees -> pi/12
        while ((direction <= 0.5f * Mathf.PI + offset && direction >= 0.5f * Mathf.PI - offset) || (direction <= 1.5f * Mathf.PI + offset && direction >= 1.5f * Mathf.PI - offset))
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
        //score
        else
        {
            velocity.x = velocity.y = 0.0f;
            transform.position = velocity;
            gameStart = false;

            //which player
            if (transform.position.x < 0.0f)
            {
                //left
                collision.gameObject.GetComponentInChildren<Score>().IncrementScore(0);
            }
            else if (transform.position.x > 0.0f)
            {
                //right
                collision.gameObject.GetComponentInChildren<Score>().IncrementScore(0);
            }
        }
    }
}
