using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepTransform : MonoBehaviour
{
    Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = startScale;
    }
}
