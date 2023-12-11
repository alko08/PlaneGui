using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] planes;
    public Transform mainCamera;
    public bool lookAtPlane = false, followPlane = false, turnLeft = false;

    private bool launched = false;
    private int tick = 0;
    private float smooth = 2.0f;
    private List<Quaternion> targets;

    // Start is called before the first frame update
    void Start() {
        targets = new List<Quaternion>();
        for (int i = 0; i < planes.Length; i++) {
            targets.Add(planes[i].transform.rotation);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (launched) {
            foreach (GameObject plane in planes) {
                plane.GetComponent<Rigidbody>().AddForce(plane.transform.forward * 30);
                if (100 < tick && tick < 300) {
                    plane.GetComponent<Rigidbody>().AddForce(transform.up * 12);
                    targets[0] = Quaternion.Euler(-15, targets[0].eulerAngles.y, targets[0].eulerAngles.z);
                } else {
                    plane.GetComponent<Rigidbody>().AddForce(transform.up * 9.8f);
                    targets[0] = Quaternion.Euler(0, targets[0].eulerAngles.y, targets[0].eulerAngles.z);
                }

                
            }
            tick++;
            // Debug.Log(tick);       
        }

        if (lookAtPlane || followPlane) {
            mainCamera.LookAt(planes[0].transform);

            if (followPlane) {
                mainCamera.transform.position = planes[0].transform.position + new Vector3(10, 10, 10);
            }
        }
    }

    void Update() {
        // Dampen towards the target rotation
        for (int i = 0; i < planes.Length; i++) {
            float zAngle = 0;
            if (planes[i].transform.eulerAngles.y < targets[i].eulerAngles.y - 10 && planes[i].transform.position.y > 10) {
                zAngle = -45;
            } else if (planes[i].transform.eulerAngles.y > targets[i].eulerAngles.y + 10 && planes[i].transform.position.y > 10) {
                zAngle = 45;
            }
            targets[i] = Quaternion.Euler(targets[i].eulerAngles.x, targets[i].eulerAngles.y, zAngle);

            planes[i].transform.rotation = Quaternion.Slerp(planes[i].transform.rotation, targets[i],  Time.deltaTime * smooth);
        }
    }

    public void launchPlanes() {
        launched = true;
    }

    public void turnRightPlanes() {
        targets[0] = Quaternion.Euler(targets[0].eulerAngles.x, targets[0].eulerAngles.y + 90, targets[0].eulerAngles.z);
    }

    public void turnLeftPlanes() {
        targets[0] = Quaternion.Euler(targets[0].eulerAngles.x, targets[0].eulerAngles.y - 90, targets[0].eulerAngles.z);
    }

    public void lookAtPlaneButton() {
        lookAtPlane = !lookAtPlane;
    }

    public void followPlaneButton() {
        followPlane = !followPlane;
    }
}
