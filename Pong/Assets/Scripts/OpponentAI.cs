using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    private GameObject Ball;
    private Vector3 ballDirection;

    private Vector3 velocity;
    private float deltaY = 0.0f;

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
        ballDirection = Ball.GetComponent<BallMovement>().GetVelocity();
        //check if its moving towards us
        if(ballDirection.x < 0.0f)
        {
            //find deltaY (vertical difference)
            deltaY = Ball.transform.position.y - transform.position.y;
            //move up by deltaY in increments
            velocity.y = deltaY * Time.deltaTime;
        }
        else
        {
            velocity.y = 0.0f;
        }
    }

    private void MovePaddle()
    {
        transform.position += velocity;

        // check boundary
        var tempPosition = transform.position;
        if (tempPosition.y > 4.0f)
        {
            tempPosition.y = 4.0f;
        }
        else if (tempPosition.y < -4.0f)
        {
            tempPosition.y = -4.0f;
        }
        transform.position = tempPosition;
    }
}
