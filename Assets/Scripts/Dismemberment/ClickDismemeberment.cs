using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDismemeberment : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.GetComponent<Limb>())
                {
                    Debug.Log("Hitting: " + hit.transform.gameObject.name);
                    Limb limb = hit.transform.GetComponent<Limb>();
                    limb?.GetHit();
                }
            }
        }
    }
}
