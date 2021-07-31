using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    //please
    public Transform lookTransform;

    void Update()
    {
        if (!lookTransform)
        {
            lookTransform = Camera.main.transform;
        }

        Vector3 eulers = (transform.position - lookTransform.position).normalized;

        transform.rotation = Quaternion.LookRotation(eulers, Vector3.up);
    }
}
