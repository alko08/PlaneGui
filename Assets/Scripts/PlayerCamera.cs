using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    //Animator anim;
    Rigidbody rb;
    public Transform cam;

    public float speed = 12f;

    void Start(){
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }

    void Update () {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        Vector3 direct = new Vector3(horiz, 0f, vert).normalized;

        if (direct.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
        }

        if (Input.GetButton("Jump") && !Input.GetButton("Down"))
        {
            rb.MovePosition(transform.position + Vector3.up * speed * Time.deltaTime);
        } else if (!Input.GetButton("Jump") && Input.GetButton("Down"))
        {
            rb.MovePosition(transform.position + Vector3.down * speed * Time.deltaTime);
        }
    }
}