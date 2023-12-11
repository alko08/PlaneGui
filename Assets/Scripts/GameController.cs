using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlaneMove[] planes;
    public Transform mainCamera;
    public bool lookAtPlane = false, followPlane = false;


    // Start is called before the first frame update
    // void Start() {

    // }

    // Update is called once per frame
    // void FixedUpdate() {
       
    // }

    void Update() {
        if (lookAtPlane || followPlane) {
            if (followPlane) {
                mainCamera.transform.position = planes[0].gameObject.transform.position + new Vector3(10, 10, 10);
            }

            mainCamera.LookAt(planes[0].gameObject.transform);
        }
    }

    public void launchPlanes() {
        for (int i = 0; i < planes.Length; i++) {
            planes[i].launched = true;
        }
    }

    public void turnRightPlanes() {
        for (int i = 0; i < planes.Length; i++) {
            planes[i].turnRight();
        }
    }

    public void turnLeftPlanes() {
        for (int i = 0; i < planes.Length; i++) {
            planes[i].turnLeft();
        }
    }

    public void lookAtPlaneButton() {
        lookAtPlane = !lookAtPlane;
    }

    public void followPlaneButton() {
        followPlane = !followPlane;
    }

    public void createJet() {

    }
    
    public void createProp() {

    }
    
    public void createHeli() {

    }

}
