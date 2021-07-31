using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    public Limb[] childLimbs;
    public bool destroyable = false;

    void Start()
    {
        
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

            transform.localScale = Vector3.zero;
            Debug.Log(name + " is trying to destroy itself");
            Destroy(this);
        }
    }
}
