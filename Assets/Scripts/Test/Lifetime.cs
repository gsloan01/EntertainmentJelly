using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime = 4;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
