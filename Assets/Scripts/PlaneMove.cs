using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public float speed = 30f, turnSpeed = 2f;
    public bool launched = false;

    private Rigidbody rb;
    private Quaternion target;
    private Transform trans;
    private int tick = 0;

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        trans = gameObject.transform;
        target = Quaternion.Euler(trans.eulerAngles.x, trans.eulerAngles.y, target.eulerAngles.z);
    }

    // FixedUpdate is called once per tick
    void FixedUpdate() {
        if (launched) {
            rb.AddForce(trans.forward * speed);
            if (100 < tick && tick < 300) {
                rb.AddForce(transform.up * 12);
                target = Quaternion.Euler(-15, target.eulerAngles.y, target.eulerAngles.z);
            } else {
                rb.AddForce(transform.up * 9.8f);
                target = Quaternion.Euler(0, target.eulerAngles.y, target.eulerAngles.z);
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
    }

     public void turnRight() {
        target = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y + 90, target.eulerAngles.z);
    }

    public void turnLeft() {
        target = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y - 90, target.eulerAngles.z);
    }
}
