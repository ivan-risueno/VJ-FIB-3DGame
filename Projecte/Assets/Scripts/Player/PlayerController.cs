using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public bool godMode;
    public float speed, jumpForce;
    private Rigidbody rb;
    private float alpha, GMalpha;
    private Transform transform;
    private bool canJump, canTurnRight, canTurnLeft, canDance, stopMoving;
    private Vector3 nextPlatePosition;
    public ParticleSystem jumpDustPM;
    private bool jumped, doubleJumped, dead;
    public Vector3 boxSize;
    public float maxGroundDistance;
    public LayerMask layerMask;
    public Animator animator;
    public ParticleSystem jumpDust;
    public TextMeshProUGUI scoreUI, scorePS, scoreGO;
    public AudioSource jumpSound, turnSound;
    public static int score;

    // Start is called before the first frame update
    void Start()
    {
        godMode = false;
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        canJump = true;
        canTurnRight = false;
        canTurnLeft = false;
        canDance = false;
        stopMoving = false;
        alpha = 0.5f;
        GMalpha = 0.1f;
        score = 0;
    }

    public void startGodMode()
    {
        canJump = false;
        canTurnRight = false;
        canTurnLeft = false;
        canDance = false;
        stopMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
        Move();
    }

    void Move()
    {
        TryTurningLeft();
        TryTurningRight();
        TryJumping();
        TryDancing();


        if (!stopMoving) transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void AllowJump(Transform targetTransform)
    {
        canJump = true;
        nextPlatePosition = targetTransform.position;
    }

    public void AllowTurnRight(Transform targetTransform)
    {
        canTurnRight = true;
        nextPlatePosition = targetTransform.position;
    }

    public void AllowTurnLeft(Transform targetTransform)
    {
        canTurnLeft = true;
        nextPlatePosition = targetTransform.position;
    }

    public void AllowDancing(Transform targetTransform)
    {
        canDance = true;
        nextPlatePosition = targetTransform.position;
    }

    void CheckIfDead()
    {
        if (!dead && !IsGrounded() && transform.position.y <= nextPlatePosition.y - 2)
        {
            dead = true;
            stopMoving = true;
            animator.SetTrigger("Die");
            //Debug.Log("DEAD");
        }
    }

    void TryJumping()
    {
        if (!godMode)
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && canJump)
            {
                Debug.Log("Jumping!");
                canJump = false;
                jumped = true;
                doubleJumped = false;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetTrigger("Jump");
                jumpDustPM.Play();
                jumpSound.Play();

            }
            else if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && jumped && !doubleJumped)
            {
                doubleJumped = true;
                jumped = false;
                Debug.Log("Double jumping!");
                animator.SetTrigger("Jump");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else canJump = true;
        }
        else
        {
            if (canJump)
            {
                if (IsCentered())
                {
                    canJump = false;
                    Debug.Log("Jumping!");
                    animator.SetTrigger("Jump");
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    jumpDustPM.Play();
                    jumpSound.Play();
                }
            }
        }

    }

    void TryTurningRight()
    {
        if (!godMode)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canTurnRight && IsGrounded())
            {
                canTurnRight = false;
                canJump = false;
                Debug.Log("Turning Right!");
                transform.position = nextPlatePosition;
                transform.Rotate(Vector3.up, -transform.localRotation.eulerAngles.y);
                transform.Rotate(Vector3.up, 90f);
                score++;
                scoreUI.text = score.ToString();
                scorePS.text = score.ToString();
                scoreGO.text = score.ToString();
                turnSound.Play();
            }
        }
        else
        {
            if (canTurnRight)
            {
                if (IsCentered())
                {
                    canTurnRight = false;
                    Debug.Log("Turning Right!");
                    transform.position = nextPlatePosition;
                    transform.Rotate(Vector3.up, -transform.localRotation.eulerAngles.y);
                    transform.Rotate(Vector3.up, 90f);
                    score++;
                    scoreUI.text = score.ToString();
                    scorePS.text = score.ToString();
                    scoreGO.text = score.ToString();
                    turnSound.Play();
                }
            }
        }
    }

    void TryTurningLeft()
    {
        if (!godMode)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canTurnLeft && IsGrounded())
            {
                canTurnLeft = false;
                canJump = false;
                Debug.Log("Turning Left!");
                transform.position = nextPlatePosition;
                transform.Rotate(Vector3.up, -transform.localRotation.eulerAngles.y);
                score++;
                scoreUI.text = score.ToString();
                scorePS.text = score.ToString();
                scoreGO.text = score.ToString();
                turnSound.Play();
            }
        }
        else
        {
            if (canTurnLeft)
            {
                if (IsCentered())
                {
                    canTurnLeft = false;
                    Debug.Log("Turning Left!");
                    transform.position = nextPlatePosition;
                    transform.Rotate(Vector3.up, -transform.localRotation.eulerAngles.y);
                    score++;
                    scoreUI.text = score.ToString();
                    scorePS.text = score.ToString();
                    scoreGO.text = score.ToString();
                    turnSound.Play();
                }
            }
        }
    }

    IEnumerator DelayTheJump()
    {
        canJump = false;
        yield return new WaitForSeconds(0.5f);
        canJump = true;
    }

    void TryDancing()
    {
        if (canDance)
        {
            if (IsCentered())
            {
                stopMoving = true;
                transform.position = nextPlatePosition;
                animator.SetTrigger("Dance");
            }
        }
    }

    bool IsGrounded()
    {
        //get the radius of the players capsule collider, and make it a tiny bit smaller than that
        float radius = GetComponent<CapsuleCollider>().radius * 0.9f;
        //get the position (assuming its right at the bottom) and move it up by almost the whole radius
        Vector3 pos = transform.position + Vector3.up * (radius * 0.9f);
        //returns true if the sphere touches something on that layer
        bool isGrounded = Physics.CheckSphere(pos, radius, layerMask);
        //Debug.Log("El player esta grounded?" + isGrounded);
        return isGrounded;

        //return Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxGroundDistance, layerMask);
    }

    bool IsCentered()
    {
        //Debug.Log("Esta centrado? Posicion del player " + transform.position + ", posicion de la placa " + nextPlatePosition);
        return Vector3.Distance(transform.position, nextPlatePosition) <= (godMode ? GMalpha : alpha);
    }

    public bool CanJump()
    {
        return canJump;
    }

    public bool CanTurnLeft()
    {
        return canTurnLeft;
    }

    public bool CanTurnRight()
    {
        return canTurnRight;
    }

    public bool CanDance()
    {
        return canDance;
    }
}
