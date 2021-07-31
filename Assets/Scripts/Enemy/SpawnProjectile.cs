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
    }

    void Update()
    {
        
    }

    public void StartThrow(Vector3 direction)
    {
        rigidbody.AddForce(direction * speed, ForceMode.Impulse);
    }
}
