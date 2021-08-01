using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{

    [Serializable]
    public class PropGroup
    {
        public string name;
        public List<GameObject> objectGroups = new List<GameObject>();
    }

    public List<PropGroup> propGroups = new List<PropGroup>();

    public PropGroup GetPropGroup(string name)
    {
        for (int i = 0; i < propGroups.Count; i++)
        {
            if (propGroups[i].name == name) return propGroups[i];
        }
        return null;
    }

    public void ActivateGroup(string groupName)
    {
        ToggleGroup(groupName, true);
    }

    public void DeactivateGroup(string groupName)
    {
        ToggleGroup(groupName, false);
    }

    public void ToggleGroup(string groupName, bool toggle)
    {
        foreach (GameObject prop in GetPropGroup(groupName).objectGroups)
        {
            prop.SetActive(toggle);
        }
    }
}
