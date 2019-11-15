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
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.25f;

    //direction
    private float offset = Mathf.PI / 6; //offset of 15 degrees  
    private float direction = 0.0f; //angle in radians

    //checks if game is started
    private bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        //press space to start
        if (gameStart)
        {
            MoveBall();
            if(Input.GetKeyDown(KeyCode.R))
            {
                ResetBall();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateStartVelocity();
            gameStart = true;
        }
        //cap speed
        float velocity_magnitude = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
        if (velocity_magnitude >= maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        else if (velocity_magnitude <= minSpeed)
        {
            velocity = velocity.normalized * minSpeed;
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
        //make sure ball doesn't get tilted
        var tempRotation = transform.rotation;
        tempRotation.y = 0.0f;

        //collide with paddle
        if (collision.gameObject.tag == "Paddle")
        {
            velocity *= acceleration;
            velocity.x = -velocity.x;

            //change color and emit particles
            if (transform.position.x < 0.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().color = player1Color;
                tempRotation.y = 180.0f;
            }
            else if (transform.position.x > 0.0f)
            {
                gameObject.GetComponent<SpriteRenderer>().color = player2Color;
                tempRotation.y = 0.0f;
            }

            //apply particle effect
            transform.rotation = tempRotation;
            gameObject.GetComponent<ParticleSystem>().Play();
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
            ResetBall();
        }

        //reset rotation -> doesn't tilt
        tempRotation.x = tempRotation.z = tempRotation.w = 0.0f;
        transform.rotation = tempRotation;
    }

    private void ResetBall()
    {
        //reset rotation
        var tempRotation = transform.rotation;
        tempRotation.x = tempRotation.y = tempRotation.z = tempRotation.w = 0.0f;
        transform.rotation = tempRotation;

        //reset position
        velocity.x = velocity.y = 0.0f;
        transform.position = velocity;

        //reset game status and color
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        gameStart = false;
    }
}
