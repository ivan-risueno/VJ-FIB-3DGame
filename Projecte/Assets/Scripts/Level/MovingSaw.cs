using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    public bool movingForward;
    public bool movingRight;
    public float maxMove;
    public float movSpeed;
    public ParticleSystem right, left;
    double moved = 0;

    void Start()
    {
        invertSparks();
    }

    void Update()
    {
        moved += movSpeed * Time.deltaTime;

        if (moved >= maxMove || moved <= 0)
        {
            movingForward = !movingForward;
            moved = 0;
            invertSparks();
        }

        if (movingRight)
        {
            if (movingForward)
            {
                transform.Translate(0, 0, movSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(0, 0, -movSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (movingForward)
            {
                transform.Translate(movSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(-movSpeed * Time.deltaTime, 0, 0);
            }
        }

    }

    void invertSparks()
    {
        if (movingForward)
        {
            right.Stop();
            left.Play();
        }
        else
        {
            right.Play();
            left.Stop();
        }
    }
}
