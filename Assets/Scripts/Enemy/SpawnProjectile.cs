using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    Rigidbody rigidbody;
    float speed = 1000;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    void Update()
    {
        
    }
}
