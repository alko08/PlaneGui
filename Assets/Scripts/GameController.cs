using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List <PlaneMove> planes;
    public Transform mainCamera, jetSpawn, propSpawn, heliSpawn;
    public bool lookAtPlane = false, followPlane = false;
    public GameObject jetPrefab, propPrefab, heliPrefab;
    public int curPlane = 0;
    


    // Start is called before the first frame update
    // void Start() {

    // }

    // Update is called once per frame
    // void FixedUpdate() {
       
    // }

    void Update() {
        if (lookAtPlane || followPlane) {
            if (followPlane) {
                mainCamera.transform.position = planes[curPlane].gameObject.transform.position + new Vector3(10, 10, 10);
            }

            mainCamera.LookAt(planes[curPlane].gameObject.transform);
        }
    }

    public void launchPlanes() {
        planes[curPlane].launch();
    }

    public void turnRightPlanes() {
        planes[curPlane].turnRight();
    }

    public void turnLeftPlanes() {
        planes[curPlane].turnLeft();
    }

    public void lookAtPlaneButton() {
        lookAtPlane = !lookAtPlane;
    }

    public void followPlaneButton() {
        followPlane = !followPlane;
    }

    public void createJet() {
        GameObject jet = Instantiate(jetPrefab, jetSpawn.position, jetSpawn.rotation);
        planes.Add(jet.GetComponent<PlaneMove>());
    }
    
    public void createProp() {
        GameObject prop = Instantiate(propPrefab, propSpawn.position, propSpawn.rotation);
        planes.Add(prop.GetComponent<PlaneMove>());
    }
    
    public void createHeli() {
        GameObject heli = Instantiate(heliPrefab, heliSpawn.position, heliSpawn.rotation);
        planes.Add(heli.GetComponent<PlaneMove>());
    }

    public void nextPlane() {
        curPlane++;
        if (curPlane >= planes.Count) {
            curPlane = 0;
        }
    }
}
