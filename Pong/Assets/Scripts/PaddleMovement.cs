using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    //movement
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public float speed = 0.1f;
    public float yBoundry = 3.4f;
    private Vector3 velocity = new Vector3(0, 0, 0);

    //black holes
    public KeyCode fire = KeyCode.D;
    public GameObject ObjectToFire = null;
    public float fireDirection = 0.0f;

    //timers for black hole cooldown
    public float cooldownTime = 10.0f;
    private float timeRemaining = 0.0f;

    // Start is called before the first frame update
    void Start()
    {


    }


    // Update is called once per frame
    void Update()
    {
        MovePaddles();

        //cooldown for blackholes
        timeRemaining -= Time.deltaTime;
        if(Input.GetKeyDown(fire) && timeRemaining <= 0.0f)
        {
            FireBlackHole();
            timeRemaining = cooldownTime;
        }

    }

    //move paddles up and down
    private void MovePaddles()
    {
        // Adjust speed for when the up or down key
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
            position.y = -yBoundry;
        }
        transform.position = position;
    }

    //fire a black hole
    private void FireBlackHole()
    {
        var position = transform.position;
        position.x += fireDirection;
        Instantiate(ObjectToFire, position, transform.rotation);
    }
}

