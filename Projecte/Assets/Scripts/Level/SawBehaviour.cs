using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBehaviour : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f), -100f * Time.deltaTime);

    }
}
