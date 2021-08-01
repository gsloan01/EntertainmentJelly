using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public bool allowDuplicates = false;

    void Awake()
    {
        if (!allowDuplicates)
        {
            Object[] gameObjects = GameObject.FindObjectsOfType(typeof(DontDestroy));
            List<Object> matchingObjects = new List<Object>();

            foreach(Object gameObject in gameObjects)
            {
                if (gameObject.name == name) matchingObjects.Add(gameObject);
            }

            if (matchingObjects.Count > 1) return;
        }

        DontDestroyOnLoad(this);   
    }

    public void ResetDontDestroy()
    {
        DontDestroyOnLoad(this);
    }
}
