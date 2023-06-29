using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : GodmodeController
{
    private bool jumped, doubleJumped, dead;
    public Vector3 boxSize;
    public float maxGroundDistance;
    public LayerMask layerMask;
    

    protected override void CheckIfDead()
    {
        if (!IsGrounded() && transform.position.y <= nextPlatePosition.y - 2)
        {
            dead = true;
            Debug.Log("DEAD");
        }
    }

    protected override void TryJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !canTurnRight && !canTurnLeft) 
        {
            Debug.Log("Jumping!");
            canJump = false;
            jumped = true;
            doubleJumped = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            jumpDust.Play();
            jumpSound.Play();

        } else if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && !doubleJumped)
        {
            doubleJumped = true;
            jumped = false;
            Debug.Log("Double jumping!");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    protected override void TryTurningRight()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canTurnRight && IsGrounded())
        {
            canTurnRight = false;
            Debug.Log("Turning Right!");
            transform.position = nextPlatePosition + new Vector3((float)0, (float)0.3, (float)0);
            transform.Rotate(Vector3.up, -transform.localRotation.eulerAngles.y);
            transform.Rotate(Vector3.up, 90f);
            score++;
            scoreUI.text = score.ToString();
            scorePS.text = score.ToString();
            scoreGO.text = score.ToString();
            turnSound.Play();
        }
    }

    protected override void TryTurningLeft()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canTurnLeft && IsGrounded())
        {
            canTurnLeft = false;
            Debug.Log("Turning Left!");
            transform.position = nextPlatePosition + new Vector3((float)0, (float)0.3, (float)0);
            transform.Rotate(Vector3.up, -transform.localRotation.eulerAngles.y);
            score++;
            scoreUI.text = score.ToString();
            scorePS.text = score.ToString();
            scoreGO.text = score.ToString();
            turnSound.Play();
        }
    }

    private bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxGroundDistance, layerMask);
    }
}
