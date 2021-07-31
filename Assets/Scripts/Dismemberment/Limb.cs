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

    void Start()
    {
        if (wound != null) wound.SetActive(false);
    }

    void Update()
    {
        
    }

    public void GetHit()
    {
        if (destroyable)
        {
            if (childLimbs.Length > 0)
            {
                for(int i = 0; i < childLimbs.Length; i++)
                {
                    if (childLimbs[i] != null) childLimbs[i].GetHit();
                }
            }

            if (wound != null) wound.SetActive(true);
            if (limb != null) Instantiate(limb, transform.position, transform.rotation);
            if (bloodSpurt != null) Instantiate(bloodSpurt, transform.position, transform.rotation);

            transform.localScale = Vector3.zero;
            //Debug.Log(name + " is trying to destroy itself");
            Destroy(this);
        }
    }
}
