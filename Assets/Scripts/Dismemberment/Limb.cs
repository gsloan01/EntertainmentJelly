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
                foreach(Limb limb in childLimbs)
                {
                    limb?.GetHit();
                }
            }

            transform.localScale = Vector3.zero;
            GetComponent<Collider>().enabled = false;
            Destroy(this);
            Debug.Log(name + "is trying to destroy itself");
        }
    }
}
