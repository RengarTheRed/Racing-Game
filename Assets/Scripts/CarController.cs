using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float SuspensionLength = .5f;
    float moveSpeed = 50f;
    float SuspensionTightness = 15f;
    float movement = 0;
    float rotate = 0;
    Rigidbody rb;

    public List<Transform> SuspensionPoints;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        movement = 0;
        rotate = 0;
        //Input WASD controls

        if(Input.GetKey(KeyCode.W))
        {
            movement += 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            movement -= 1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            rotate += 1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            rotate -= 1;
        }

        if(rotate!=0)
        {
            transform.Rotate(Vector3.up, rotate);
        }
        if(movement > 0)
        {
            rb.velocity += Vector3.forward * movement * Time.deltaTime * moveSpeed;
        }

        //Per Suspension, check if in ground and if so apply force based on distance and spring tightness
        foreach(Transform transform in rb.GetComponentsInChildren<Transform>())
        {
            RaycastHit hit;
            
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, SuspensionLength))
            {
                float forceToApply = (SuspensionLength - hit.distance) * SuspensionTightness;
                rb.AddForceAtPosition(new Vector3(0, forceToApply, 0), transform.position);
            }
        }
    }
}
