using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDetector : MonoBehaviour
{
    private List<GameObject> plates;
    public float distance;
    private GameObject target;
    private bool godMode;
    public Image godModeIcon;
    public AudioSource godModeSound, normalModeSound;

    // Start is called before the first frame update
    void Start()
    {
        godMode = false;
        getPlates();
        godModeIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = !godMode;
            this.gameObject.GetComponent<PlayerController>().godMode = godMode;
            if (godMode) this.gameObject.GetComponent<PlayerController>().startGodMode();
            else this.gameObject.GetComponent<PlayerController>().jumpForce = 17.5f;
            if (godMode)
            {
                godModeIcon.enabled = true;
                godModeSound.Play();
            }
            else
            {
                godModeIcon.enabled = false;
                normalModeSound.Play();
            }

        }

        GetClosestObject(plates, this.gameObject);

        if (Vector3.Distance(transform.position, target.transform.position) <= distance)
        {
            if (godMode && target.tag == "JumpPlate" && !target.GetComponent<PlateScript>().seen && !this.gameObject.GetComponent<PlayerController>().CanJump())
            {
                this.gameObject.GetComponent<PlayerController>().jumpForce = target.GetComponent<PlateScript>().jumpForce;
                target.GetComponent<PlateScript>().seen = true;
                this.gameObject.GetComponent<PlayerController>().AllowJump(target.transform);
            }
            else if (target.tag == "TurnRightPlate" && !target.GetComponent<PlateScript>().seen && !this.gameObject.GetComponent<PlayerController>().CanTurnRight())
            {
                target.GetComponent<PlateScript>().seen = true;
                this.gameObject.GetComponent<PlayerController>().AllowTurnRight(target.transform);
            }
            else if (target.tag == "TurnLeftPlate" && !target.GetComponent<PlateScript>().seen && !this.gameObject.GetComponent<PlayerController>().CanTurnLeft())
            {
                target.GetComponent<PlateScript>().seen = true;
                this.gameObject.GetComponent<PlayerController>().AllowTurnLeft(target.transform);
            }
            else if (target.tag == "FinalBlockPlate" && !target.GetComponent<PlateScript>().seen)
            {
                target.GetComponent<PlateScript>().seen = true;
                this.gameObject.GetComponent<PlayerController>().AllowDancing(target.transform);
            }
        }
    }


    public void GetClosestObject(List<GameObject> Object, GameObject fromThis)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.transform.position;
        foreach (GameObject potentialTarget in Object)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        target = bestTarget;
    }

    void getPlates()
    {
        plates = new List<GameObject>();
        GameObject[] plates2 = GameObject.FindGameObjectsWithTag("TurnRightPlate");
        foreach (var plate in plates2)
        {
            plates.Add(plate);
        }

        plates2 = GameObject.FindGameObjectsWithTag("TurnLeftPlate");
        foreach (var plate in plates2)
        {
            plates.Add(plate);
        }

        plates2 = GameObject.FindGameObjectsWithTag("JumpPlate");
        foreach (var plate in plates2)
        {
            plates.Add(plate);
        }

        plates2 = GameObject.FindGameObjectsWithTag("FinalBlockPlate");
        foreach (var plate in plates2)
        {
            plates.Add(plate);
        }
    }
}
