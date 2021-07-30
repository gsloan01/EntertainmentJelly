using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismemberMaster : MonoBehaviour
{
    Animator animator;

    List<Rigidbody> ragdollrbs;

    void Start()
    {
        animator = GetComponent<Animator>();

        ragdollrbs = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        ragdollrbs.Remove(GetComponent<Rigidbody>());

        DeactivateRagdoll();
    }

    void Update()
    {
        
    }

    public void ActivateRagdoll()
    {
        animator.enabled = false;
        foreach(Rigidbody rb in ragdollrbs)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }

    public void DeactivateRagdoll()
    {
        animator.enabled = true;
        foreach (Rigidbody rb in ragdollrbs)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}
