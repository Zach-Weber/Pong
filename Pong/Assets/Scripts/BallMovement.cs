using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //color
    public Color player1Color = Color.blue;
    public Color player2Color = Color.red;

    //speed
    public float mass = 1.0f;
    public float speed = 0.15f;
    public float acceleration = 1.05f;
    public Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

    //direction
    private float offset = Mathf.PI / 6; //offset of 15 degrees  
    private float direction = 0.0f; //angle in radians

    //checks if game is started
    private bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //cap speed
        float velocity_magnitude = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
        if (velocity_magnitude >= 0.2f)
        {
            velocity = velocity.normalized * 0.2f;
        }
        else if (velocity_magnitude <= 0.1f)
        {
            velocity = velocity.normalized * 0.1f;
        }

        //press space to start
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
        //collide with paddle
        if (collision.gameObject.tag == "Paddle")
        {
            velocity *= acceleration;
            velocity.x = -velocity.x;

            //change color
            if (transform.position.x < 0.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().color = player1Color;
            }
            else if (transform.position.x > 0.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().color = player2Color;
            }

        }
        //collide with top or bottom wall
        else if (collision.gameObject.tag == "Wall")
        {
            velocity.y = -velocity.y;
        }
        //collide with left/right edge to score
        else if (collision.gameObject.tag == "MainCamera")
        {
            Score PlayerScores = collision.gameObject.GetComponent<Score>();

            //check which player scored
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
