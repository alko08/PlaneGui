using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] planes;
    public Transform mainCamera;
    public bool lookAtPlane = false, followPlane = false;

    private bool launched = false;
    private int tick = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (launched) {
            foreach (GameObject plane in planes) {
                plane.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
                if (tick > 100) {
                    plane.GetComponent<Rigidbody>().AddForce(transform.up * 1f);
                }
            }
            tick++;
            if (lookAtPlane || followPlane) {
                mainCamera.LookAt(planes[0].transform);

                if (followPlane) {
                    mainCamera.transform.position = planes[0].transform.position + new Vector3(10, 10, 10);
                }
            }            
        }
    }

    public void launchPlanes() {
        launched = true;
    }

    public void lookAtPlaneButton() {
        lookAtPlane = !lookAtPlane;
    }

    public void followPlaneButton() {
        followPlane = !followPlane;
    }
}
