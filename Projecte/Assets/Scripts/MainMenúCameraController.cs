using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenúCameraController : MonoBehaviour
{
    private float maxMoved = 130f;
    private float moved = 0f;
    public float speed;
    private bool movingForward = true;
    private bool wantsCenter = false;
    public MainMenuBehaviour mmb;

    public void center()
    {
        speed = speed * 20;
        movingForward = false;
        wantsCenter = true;
    }

    void Update()
    {
        if(moved >= maxMoved)
        {
            movingForward = !movingForward;
            moved = 0;
        }
        if (movingForward)
        {
            transform.Translate(new Vector3(0f, 0f, 1f) * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(0f, 0f, -1f) * speed * Time.deltaTime);
        }
        moved += speed * Time.deltaTime;
        if (transform.position.z <= 2.330297f && wantsCenter)
        {
            speed = 0;
            mmb.playGame();
        } 
    }
}
