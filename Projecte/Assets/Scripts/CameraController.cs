using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player, firstBlock, lastBlock;
    private Vector3 distStartEnd;

    void Start()
    {
        transform.position = firstBlock.transform.position + new Vector3(16, 8, 16);
        distStartEnd = lastBlock.transform.position - firstBlock.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ObstacleCollision.levelOver)
        {
            //transform.position = player.transform.position + new Vector3(8, 8, 8);
            transform.position = player.transform.position + new Vector3(4, 4, 4);
            //transform.position = 
        }
    }
}
