using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFireballBehaviour : MonoBehaviour
{
    bool moving = false;
    public bool movingRight;
    public float timeStart;
    public float timeEnd;
    public float movSpeed;
    public ParticleSystem parts;

    void Start()
    {
        parts.Stop();
        Invoke("startMove", timeStart);
        Invoke("stopMove", timeEnd);
    }

    void Update()
    {
        if (moving) {
            if (movingRight)
            {
                transform.Translate(Vector3.forward * -movSpeed * Time.deltaTime);
            }
            else if (!movingRight)
            {
                transform.Translate(Vector3.right * -movSpeed * Time.deltaTime);
            }
        }
    }

    void startMove()
    {
        moving = true;
        parts.Play();
    }
    void stopMove()
    {
        moving = false;
        parts.Stop();
    }
}
