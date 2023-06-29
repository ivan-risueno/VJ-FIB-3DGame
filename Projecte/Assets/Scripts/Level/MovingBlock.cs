using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{

    public bool movingForward;
    public bool movingRight;
    public float maxMove;
    public float movSpeed;
    double moved = 0;

    void Update()
    {
        moved += movSpeed * Time.deltaTime;

        if (moved >= maxMove || moved <= 0)
        {
            movingForward = !movingForward;
            moved = 0;
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
}
