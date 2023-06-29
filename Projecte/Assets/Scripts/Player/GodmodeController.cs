using UnityEngine;
using System.Collections;
using TMPro;

public class GodmodeController: MonoBehaviour
{

    public float speed, jumpForce, rotationDelay;
    protected Rigidbody rb;
    private float velX, velY, velZ, alpha;
    private Transform transform;
    protected bool rotated, canJump, canTurnRight, canTurnLeft;
    protected Vector3 nextPlatePosition;
    public ParticleSystem jumpDust;
    public TextMeshProUGUI scoreUI, scorePS, scoreGO;
    public static int score = 0;
    public Animator animator;
    public AudioSource jumpSound, turnSound;
    


    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        canJump = false;
        canTurnRight = false;
        canTurnLeft = false;
        alpha = 1.5f;
    }

    void Update()
    {
        CheckIfDead();
        Move();
    }

    void FixedUpdate()
    {
    }

    void Move()
    {
        TryJumping();
        TryTurningLeft();
        TryTurningRight();


        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator DelayTheRotation()
    {
        rotated = true;
        yield return new WaitForSeconds(rotationDelay);
        rotated = false;
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

    protected virtual void CheckIfDead()
    {

    }

    protected virtual void TryJumping()
    {
        if (canJump)
        {
            if (IsCentered())
            {
                canJump = false;
                Debug.Log("Jumping!");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpDust.Play();
                animator.SetTrigger("Jump");
                jumpSound.Play();
            }
        }
    }

    protected virtual void TryTurningRight()
    {
        if (canTurnRight)
        {
            if (IsCentered())
            {
                canTurnRight = false;
                Debug.Log("Turning Right!");
                transform.position = nextPlatePosition + new Vector3((float)0, (float)0, (float)0);
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

    protected virtual void TryTurningLeft()
    {
        if (canTurnLeft)
        {
            if (IsCentered())
            {
                canTurnLeft = false;
                Debug.Log("Turning Left!");
                transform.position = nextPlatePosition + new Vector3((float)0, (float)0, (float)0);
                transform.Rotate(Vector3.up, -transform.localRotation.eulerAngles.y);
                score++;
                scoreUI.text = score.ToString();
                scorePS.text = score.ToString();
                scoreGO.text = score.ToString();
                turnSound.Play();
            }
        }
    }

    protected bool IsCentered()
    {
        //Debug.Log("Esta centrado? Posicion del player " + transform.position + ", posicion de la placa " + nextPlatePosition);
        return Vector3.Distance(transform.position, nextPlatePosition) <= alpha;
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

    public void win()
    {
        animator.SetTrigger("Dance");
        speed = 0;
    }
}