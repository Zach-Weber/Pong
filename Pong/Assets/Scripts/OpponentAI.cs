using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    private GameObject Ball;
    private Vector3 ballDirection;

    private Vector3 velocity;
    private float deltaY = 0.0f;
    public float AISpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.FindWithTag("Ball");
        velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        PredictBallMovement();
        MovePaddle();
    }

    private void PredictBallMovement()
    {
        //get ball direction
        ballDirection = Ball.GetComponent<BallMovement>().velocity;
        //check if its moving towards us
        if(ballDirection.x < 0.0f)
        {
            //find deltaY (vertical difference)
            deltaY = Ball.transform.position.y - transform.position.y;
            //move up by deltaY in increments
            if (deltaY < -0.01f)
            {
                velocity.y = -AISpeed;
            }
            else if (deltaY > 0.01f)
            {
                velocity.y = AISpeed;
            }
        }
        else
        {
            //move back to start pos
            velocity.y = 0.0f;
        }
    }

    private void MovePaddle()
    {
        transform.position += velocity;

        // check boundary
        var tempPosition = transform.position;
        if (tempPosition.y > 3.4f)
        {
            tempPosition.y = 3.4f;
        }
        else if (tempPosition.y < -3.4f)
        {
            tempPosition.y = -3.4f;
        }
        transform.position = tempPosition;
    }
}
