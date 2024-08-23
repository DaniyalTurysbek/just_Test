using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class en : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    private Rigidbody rb; 
    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("play").transform;
    } 

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            rb.AddForce(direction * moveSpeed);
        }
    }
}
