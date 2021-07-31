using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismemberMaster : MonoBehaviour
{
    public GameObject bloodSpurtOverride;
    public GameObject secondaryEffectOverride;
    public AudioSource audioSourceOverride;

    Animator animator;

    List<Rigidbody> ragdollrbs;
    List<Limb> limbs;

    void Start()
    {
        animator = GetComponent<Animator>();

        ragdollrbs = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        limbs = new List<Limb>(GetComponentsInChildren<Limb>());
        ragdollrbs.Remove(GetComponent<Rigidbody>());

        DeactivateRagdoll();

        HandleOverrides();
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

    public void DeathEvent()
    {
        ActivateRagdoll();
    }

    void HandleOverrides()
    {
        if (limbs.Count > 0)
        {
            foreach(Limb limb in limbs)
            {
                if (bloodSpurtOverride != null) limb.bloodSpurt = bloodSpurtOverride;
                if (secondaryEffectOverride != null) limb.secondaryEffect = secondaryEffectOverride;
                if (audioSourceOverride != null) limb.audioSource = audioSourceOverride;
            }
            
        }
    }
}
