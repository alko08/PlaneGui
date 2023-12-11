using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool rotate;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rotate = false;
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate) {
            transform.Rotate(0f, 0f, speed * Time.deltaTime, Space.Self);
        }
        if (speed < 1000f) {
            speed += 2f;
        }
    }
}
