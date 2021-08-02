using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    public Limb[] childLimbs;
    public bool destroyable = false;

    public GameObject limb;
    public GameObject wound;
    public GameObject bloodSpurt;
    public GameObject secondaryEffect;
    public AudioSource audioSource;

    void Start()
    {
        if (wound != null) wound.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Break()
    {
        if (destroyable)
        {
            if (childLimbs.Length > 0)
            {
                for(int i = 0; i < childLimbs.Length; i++)
                {
                    if (childLimbs[i] != null) childLimbs[i].Break();
                }
            }

            GetComponent<Health>()?.Kill();
            if (wound != null) wound.SetActive(true);
            if (limb != null) Destroy(Instantiate(limb, transform.position, transform.rotation), 5);
            if (bloodSpurt != null) Destroy(Instantiate(bloodSpurt, transform.position, transform.rotation), 5);
            if (secondaryEffect != null) Destroy(Instantiate(secondaryEffect, transform.position, transform.rotation), 5);
            if (audioSource != null) audioSource.Play();

            transform.localScale = Vector3.zero;
            //Debug.Log(name + " is trying to destroy itself");
            Destroy(this);
        }
    }
}
