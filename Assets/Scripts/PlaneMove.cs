using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public float speed = 30f, turnSpeed = 2f;
    public bool isHeli = false;
    public Vector3 targetLaunch;

    private Rigidbody rb;
    private Quaternion target;
    private Transform trans;
    private int tick = 0;
    private bool launched = false, starting = false;

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        trans = transform;
        target = Quaternion.Euler(trans.eulerAngles.x, trans.eulerAngles.y, target.eulerAngles.z);
    }

    // FixedUpdate is called once per tick
    void FixedUpdate() {
        if (launched) {
            if (!isHeli) {
                rb.AddForce(trans.forward * speed);
                if (100 < tick && tick < 300) {
                    rb.AddForce(transform.up * 12);
                    target = Quaternion.Euler(-15, target.eulerAngles.y, target.eulerAngles.z);
                } else {
                    rb.AddForce(transform.up * 9.8f);
                    target = Quaternion.Euler(0, target.eulerAngles.y, target.eulerAngles.z);
                }
            } else {
                if (100 < tick && tick < 300) {
                    rb.AddForce(transform.up * speed);
                } else {
                    rb.AddForce(transform.up * 9.8f);
                }
            }

            tick++;
            // Debug.Log(tick);       
        }
    }

    // Update is called once per frame
    void Update() {
        // Dampen towards the target rotation
        float zAngle = 0;
        if (trans.eulerAngles.y < target.eulerAngles.y - 10 && trans.position.y > 10) {
            zAngle = -45;
        } else if (trans.eulerAngles.y > target.eulerAngles.y + 10 && trans.position.y > 10) {
            zAngle = 45;
        }
        target = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y, zAngle);
        trans.rotation = Quaternion.Slerp(trans.rotation, target,  Time.deltaTime * turnSpeed);

        if (starting) {
            trans.position = Vector3.MoveTowards(trans.position, targetLaunch, Time.deltaTime * turnSpeed * 2);
            if (trans.position == targetLaunch) {
                turnLeft();
                starting = false;
                StartCoroutine(readyToLaunch());
            }
        }
    }

     public void turnRight() {
        target = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y + 90, target.eulerAngles.z);
    }

    public void turnLeft() {
        target = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y - 90, target.eulerAngles.z);
    }

    public void launch() {
        if (isHeli) {
            StartCoroutine(startHeli());
        } else {
            starting = true;
        }
    }

    IEnumerator readyToLaunch() {
        yield return new WaitUntil(() => trans.rotation == target); 
        launched = true;
    }

    IEnumerator startHeli() {
        Rotate rotors = gameObject.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Rotate>();
        rotors.rotate = true;
        yield return new WaitUntil(() => rotors.speed == 1000f); 
        launched = true;
    }
}
