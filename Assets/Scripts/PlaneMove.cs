using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public float speed = 30f, turnSpeed = 2f;
    public bool isHeli = false, launched = false;
    public Vector3 targetLaunch;

    private Rigidbody rb;
    private Quaternion target;
    private Transform trans;
    private int tick = 0;
    private bool starting = false;
    private float targetX = 0f;

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        trans = transform;
        target = Quaternion.Euler(0, trans.eulerAngles.y, 0);        
    }

    // FixedUpdate is called once per tick
    void FixedUpdate() {
        if (launched) {
            if (!isHeli) {
                rb.AddForce(trans.forward * speed);
                if (100 < tick && tick < 300) {
                    rb.AddForce(transform.up * 12);
                    targetX = -15;
                    target = Quaternion.Euler(-15, target.eulerAngles.y, target.eulerAngles.z);
                } else if (tick == 300) {
                    targetX = 0;
                } else {
                    if (speed != 0) {
                        rb.AddForce(transform.up * 9.8f);
                    }
                    target = Quaternion.Euler(targetX, target.eulerAngles.y, target.eulerAngles.z);
                }
            } else {
                if (tick <= 100) {
                    rb.AddForce(transform.up * 9.8f);
                } else if (100 < tick && tick < 300) {
                    rb.AddForce(transform.up * speed);
                } else if (tick == 300) {
                    targetX = 30f;
                } else {
                    target = Quaternion.Euler(targetX, target.eulerAngles.y, target.eulerAngles.z);
                    rb.AddForce(transform.up * speed);
                }

                if (tick > 300) {
                    Rotate rotors = gameObject.transform.GetChild(1).GetChild(3).gameObject.GetComponent<Rotate>();
                    rotors.speed = 1000 * (speed / 15);
                }
            }

            tick++;
            // Debug.Log(tick);       
        }

        if (trans.position.x < -500 || trans.position.x > 500 || trans.position.z < -500 || trans.position.z > 500) {
            if (!isHeli) {
                Vector3 rotation = Quaternion.LookRotation(-trans.position, Vector3.up).eulerAngles;
                target = Quaternion.Euler(target.eulerAngles.x, rotation.y, target.eulerAngles.z);
            } else {
                Vector3 rotation = Quaternion.LookRotation(-trans.position, trans.up).eulerAngles;
                target = Quaternion.Euler(target.eulerAngles.x, rotation.y, target.eulerAngles.z);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (trans.position.y >= 10) {
            starting = false;
        }

        // Dampen towards the target rotation
        float zAngle = 0;
        if (trans.eulerAngles.y < target.eulerAngles.y - 10 && trans.position.y > 10) {
            zAngle = -45;
        } else if (trans.eulerAngles.y > target.eulerAngles.y + 10 && trans.position.y > 10) {
            zAngle = 45;
        }
        target = Quaternion.Euler(targetX, target.eulerAngles.y, zAngle);
        trans.rotation = Quaternion.Slerp(trans.rotation, target,  Time.deltaTime * turnSpeed);

        if (starting && trans.position.y < 10) {
            trans.position = Vector3.MoveTowards(trans.position, targetLaunch, Time.deltaTime * turnSpeed * 2);
            if (trans.position == targetLaunch) {
                // turnLeft();
                target = Quaternion.Euler(0, 0, 0);
                starting = false;
                StartCoroutine(readyToLaunch());
            }
        }
    }

    public void turnRight() {
        target = Quaternion.Euler(targetX, target.eulerAngles.y + 90, target.eulerAngles.z);
    }

    public void turnLeft() {
        target = Quaternion.Euler(targetX, target.eulerAngles.y - 90, target.eulerAngles.z);
    }

    public void launch() {
        if (isHeli) {
            StartCoroutine(startHeli());
        } else {
            Vector3 rotation = Quaternion.LookRotation(targetLaunch-trans.position, Vector3.up).eulerAngles;
            target = Quaternion.Euler(targetX, rotation.y, target.eulerAngles.z);
            starting = true;
        }
    }

    IEnumerator readyToLaunch() {
        yield return new WaitUntil(() => trans.rotation == target); 
        launched = true;
    }

    IEnumerator startHeli() {
        Rotate rotors = gameObject.transform.GetChild(1).GetChild(3).gameObject.GetComponent<Rotate>();
        rotors.rotate = true;
        yield return new WaitUntil(() => rotors.speed == 1000f); 
        launched = true;
    }

    public void moveUp() {
        if (launched) {
            if (isHeli) {
                targetX += 15;
                if (targetX > 90) {
                    targetX = 90;
                }
            } else {
                targetX -= 15;
                if (targetX < -90) {
                    targetX = -90;
                }
            }
        }
    }

    public void moveDown() {
        if (launched) {
            if (isHeli) {
                targetX -= 15;
                if (targetX < -90) {
                    targetX = -90;
                }
            } else {
                targetX += 15;
                if (targetX > 90) {
                    targetX = 90;
                }
            }
        }
    }

    public void reset() {
        tick = 0;
        targetX = 0;
        launched = false;
        starting = false;
        target = Quaternion.Euler(0f, 90f, 0f);
        if (isHeli) {
            target = Quaternion.Euler(0f, 0f, 0f);
            Rotate rotors = gameObject.transform.GetChild(1).GetChild(3).gameObject.GetComponent<Rotate>();
            rotors.speed = 0;
            rotors.rotate = false;
        }
        trans.rotation = target;
        trans.position = targetLaunch;
    }
}
