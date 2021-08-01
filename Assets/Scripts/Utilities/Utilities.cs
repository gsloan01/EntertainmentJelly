using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static T GetComponentInParents<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component) return component;

        if (gameObject.transform.parent)
        {
            return gameObject.transform.parent.gameObject.GetComponentInParents<T>();
        }

        return null;
    }
}
