using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);   
    }

    public void ResetDontDestroy()
    {
        //gameObject.transform.SetParent(GameObject.Find("DontDestroyOnLoad")?.transform);
        DontDestroyOnLoad(this);
    }
}
