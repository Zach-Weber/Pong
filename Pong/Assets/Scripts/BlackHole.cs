using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float mass = 1.0f;
    public float gConst = 0.01f;

    private GameObject Ball;
    private float force;
    private Vector3 dir;
    private float radius;

    private Vector3 speed = new Vector3(0.1f, 0.0f, 0.0f);
    private float targetPos = 0.0f;

    private const float maxTime = 5.0f;
    private float timeRemaining = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        Ball = GameObject.FindWithTag("Ball");
        targetPos = Random.Range(1.0f, 7.0f);
        timeRemaining = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //move to random x pos
        if (targetPos >= 0)
        {
            MoveBlackHole();
        }
        //start gravity once it stops moving
        else
        {
            ExertGravity();

            //disappear after a certain amount of time after reaching its target position
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ExertGravity()
    {
        //find vector between black hole and ball
        dir = transform.position - Ball.transform.position;
        //use this to find radius (magnitude of vector)
        radius = Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y);
        //make sure force doesn't get too big
        if(radius > 1.0f)
        {
            //Force = Gravitational constant * mass of object 1 * mass of object 2 / the distance between the two objects squared
            force = gConst * Ball.GetComponent<BallMovement>().mass * mass / (radius * radius);

            //find normalized vector of dist to get direction
            dir = dir.normalized;

            Ball.GetComponent<BallMovement>().velocity += dir * force;
        }

       
    }

    private void MoveBlackHole()
    {
        float fireDirection = 0.0f;
        if(transform.position.x < 0.0f)
        {
            fireDirection = 1.0f;
        }
        else if (transform.position.x > 0.0f)
        {
            fireDirection = -1.0f;
        }
        transform.position += fireDirection * speed;
        targetPos -= 0.1f;
    }

}
