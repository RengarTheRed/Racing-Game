using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float turnSpeed = 150f;
    float carSpeed = 25f;

    float movement = 0;
    float rotate = 0;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = rb.position;
        movement = 0;
        rotate = 0;
        //Input WASD controls
        movement = Input.GetAxis("Vertical") * carSpeed;
        rotate = Input.GetAxis("Horizontal");

        
        float newRotate = rotate * turnSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Rotate(0, newRotate, 0, Space.World);
        rb.AddForce(transform.forward * movement, ForceMode.Acceleration);
    }
}
