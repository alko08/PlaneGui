using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List <PlaneMove> planes;
    public Transform mainCamera, jetSpawn, propSpawn, heliSpawn;
    public bool lookAtPlane = false, followPlane = false;
    public GameObject jetPrefab, propPrefab, heliPrefab;
    


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
        for (int i = 0; i < planes.Count; i++) {
            planes[i].launched = true;
        }
    }

    public void turnRightPlanes() {
        for (int i = 0; i < planes.Count; i++) {
            planes[i].turnRight();
        }
    }

    public void turnLeftPlanes() {
        for (int i = 0; i < planes.Count; i++) {
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

}
