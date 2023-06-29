using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GiantRock : MonoBehaviour
{
    public float rockSize = 0.9f;
    public float rotationSpeed;
    float alreadyRotated = 0.0f;
    float rotationLimit = 90.0f;
    float rotationSum;
    Vector3 rotationPoint;
    Vector3 rotationAxis = Vector3.right;
    bool movingRight = true;
    float movSpeed;
    bool jumping = false;
    float jumped;
    float jumpSpeed;
    float jumpHeightDif;
    bool collided = false;


    void Start()
    {
        calcRotationPoint();
        rotationSum = rotationSpeed;
        movSpeed = rockSize / (rotationLimit / rotationSpeed);
    }

    void FixedUpdate()
    {
        if (!jumping)
        {
            if (alreadyRotated >= rotationLimit)
            {
                alreadyRotated = 0.0f;
                calcRotationPoint();
            }
            alreadyRotated += rotationSum * Time.deltaTime;
            transform.RotateAround(rotationPoint, rotationAxis, rotationSpeed * Time.deltaTime);
        }
        else
        {
            if(jumped >= Mathf.PI)
            {        
                if(jumpHeightDif <= 0)
                {
                    jumping = false;
                    collided = false;
                    jumped = 0.0f;
                    calcRotationPoint();
                }
                else
                {
                    if (movingRight)
                    {
                        transform.position += new Vector3(0f, -1 * Time.deltaTime, movSpeed * Time.deltaTime);
                    }
                    else
                    {
                        transform.position += new Vector3(movSpeed * Time.deltaTime, -1 * Time.deltaTime,0f);
                    }
                    
                    jumpHeightDif -= 1* Time.deltaTime;
                }
            }
            else
            {
                if (movingRight)
                {
                    transform.position += new Vector3(0f, Mathf.Cos(jumped) * Time.deltaTime, movSpeed * Time.deltaTime);
                    
                }
                else
                {
                    transform.position += new Vector3(movSpeed * Time.deltaTime, Mathf.Cos(jumped) * Time.deltaTime, 0f);
                    
                }
                jumped += jumpSpeed * Time.deltaTime;
            }
            transform.RotateAround(gameObject.transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }

    public void turn()
    {
        if (movingRight)
        {
            movingRight = false;
            rotationAxis = new Vector3(0f, 0f, 1f);
            rotationSpeed = -rotationSpeed;
        }
        else
        {
            movingRight = true;
            rotationAxis = new Vector3(1f, 0f, 0f);
            rotationSpeed = -rotationSpeed;
        }
    }

    public void jump(float jumpWidth, float jumpHeight, float heightDif)
    {
        jumping = true;
        jumped = 0.0f;

        jumpHeightDif = heightDif;

        jumpWidth -= movSpeed*(heightDif / 1);

        float jumpTime = jumpWidth / movSpeed;
        jumpSpeed = Mathf.PI / jumpTime;
    }

    public void calcRotationPoint()
    {
        rotationPoint = new Vector3(gameObject.transform.position.x + rockSize / 2, gameObject.transform.position.y - rockSize / 2,
        gameObject.transform.position.z + rockSize / 2);
        collided = false;
    }

    void OnTriggerStay(Collider col)
    {
        if (alreadyRotated + rotationSum * Time.deltaTime >= rotationLimit && !collided)
        {
            if (col.gameObject.tag == "jumpRock")
            {
                jump(1.8f, 10f, 0f);
                collided = true;
            }
            else if(col.gameObject.tag == "doubleJumpRock")
            {
                jump(2.7f, 10f, 0f);
                collided = true;
            }
            else if (col.gameObject.tag == "tripleJumpRock")
            {
                jump(4.5f, 10f, 0f);
                collided = true;
            }
            else if (col.gameObject.tag == "stairJumpRock")
            {
                jump(1.8f, 10f, 0.2335f);
                collided = true;
            }
            else if (col.gameObject.tag == "turnRock")
            {
                turn();
                collided = true;
            }
            else if(col.gameObject.tag == "fallRock")
            {
                jump(1.8f, 10f, 8f);
            }
        }
    }
}