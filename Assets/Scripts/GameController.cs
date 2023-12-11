using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List <PlaneMove> planes;
    public Transform mainCamera, jetSpawn, propSpawn, heliSpawn;
    public GameObject jetPrefab, propPrefab, heliPrefab;
    public TMPro.TextMeshProUGUI count, up, down;

    private bool lookAtPlane = true, followPlane = false, firstPerson = false;
    private int curPlane = 0;
    


    // Start is called before the first frame update
    // void Start() {

    // }

    // Update is called once per frame
    // void FixedUpdate() {
       
    // }

    void Update() {
        if (lookAtPlane || followPlane || firstPerson) {
            if (followPlane) {
                mainCamera.transform.position = planes[curPlane].gameObject.transform.GetChild(0).position;
            } else if (firstPerson) {
                Vector3 followAdjust = -10 * (planes[curPlane].transform.forward) + new Vector3(0f, 1f, 0f);
                mainCamera.transform.position = planes[curPlane].gameObject.transform.position + followAdjust;
            }

            mainCamera.LookAt(planes[curPlane].gameObject.transform);
        }

        count.text = "" + curPlane;

        if (planes[curPlane].isHeli) {
            up.text = "Forward";
            down.text = "Back";
        } else {
            up.text = "Up";
            down.text = "Down";
        }
    }

    public void speedUpPlane() {
        if (planes[curPlane].launched) {
            planes[curPlane].speed += 5;
        }
    }

    public void slowDownPlane() {
        if (planes[curPlane].launched) {
            planes[curPlane].speed -= 5;
            if (!planes[curPlane].isHeli && planes[curPlane].speed < 0) {
                planes[curPlane].speed = 0;
            }
        }
    }

    public void launchPlane() {
        planes[curPlane].launch();
    }

    public void turnRight() {
        planes[curPlane].turnRight();
    }

    public void turnLeft() {
        planes[curPlane].turnLeft();
    }

    public void lookAtPlaneButton() {
        lookAtPlane = !lookAtPlane;
        followPlane = false;
        firstPerson = false;
    }

    public void followPlaneButton() {
        followPlane = !followPlane;
        lookAtPlane = false;
        firstPerson = false;
    }

    public void firstPersonButton() {
        firstPerson = !firstPerson;
        lookAtPlane = false;
        followPlane = false;
    }

    public void createJet() {
        GameObject jet = Instantiate(jetPrefab, jetSpawn.position, jetSpawn.rotation);
        planes.Add(jet.GetComponent<PlaneMove>());
        curPlane = planes.Count - 1;
    }
    
    public void createProp() {
        GameObject prop = Instantiate(propPrefab, propSpawn.position, propSpawn.rotation);
        planes.Add(prop.GetComponent<PlaneMove>());
        curPlane = planes.Count - 1;
    }
    
    public void createHeli() {
        GameObject heli = Instantiate(heliPrefab, heliSpawn.position, heliSpawn.rotation);
        planes.Add(heli.GetComponent<PlaneMove>());
        curPlane = planes.Count - 1;
    }

    public void nextPlane() {
        curPlane++;
        if (curPlane >= planes.Count) {
            curPlane = 0;
        }
    }

    public void backPlane() {
        curPlane--;
        if (curPlane < 0) {
            curPlane = planes.Count -1;
        }
    }

    public void turnUp() {
        planes[curPlane].moveUp();
    }

    public void turnDown() {
        planes[curPlane].moveDown();
    }

    public void reset() {
        planes[curPlane].reset();
    }
}
