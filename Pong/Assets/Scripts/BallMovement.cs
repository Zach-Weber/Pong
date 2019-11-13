using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float mass = 1.0f;
    public float speed = 0.15f;
    public float acceleration = 1.05f;
    private float offset = Mathf.PI / 6; //offset of 15 degrees
    public Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
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

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    private void MoveBall()
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
            velocity.x *= acceleration;
            if(velocity.x > 0.3f)
            {
                velocity.x = 0.3f;
            }
            velocity.x = -velocity.x;
        }
        //top & bottom walls
        else if (collision.gameObject.tag == "Wall")
        {
            velocity.y = -velocity.y;
        }
        //score
        else if (collision.gameObject.tag == "MainCamera")
        {
            Score PlayerScores = collision.gameObject.GetComponent<Score>();
            //which player
            if (transform.position.x < 0.0f)
            {
                //ball on left -> right scored
                PlayerScores.IncrementScore(1);
            }
            else if (transform.position.x > 0.0f)
            {
                //ball on right -> left scored
                PlayerScores.IncrementScore(0);
            }

            velocity.x = velocity.y = 0.0f;
            transform.position = velocity;
            gameStart = false; 
        }
    }
}
