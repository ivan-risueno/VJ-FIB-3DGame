using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public ParticleSystem sparkles;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1f, 0f, 0f), rotationSpeed * Time.deltaTime); 
    }

    
    void OnDestroy()
    {
            sparkles.Play();
    }
}
