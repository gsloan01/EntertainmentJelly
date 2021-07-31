using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    public enum ePickupType
    {
        Health,
        Ammo,
        Battery,
        Gun
    }

    public ePickupType type = ePickupType.Ammo;
    public int value = 16;
    public bool selected = false;

    public AudioClip pickupSound;
    public GameObject displayText;


    private void Update()
    {
        if (selected)
        {
            //Turn on highlight and text
            displayText.SetActive(true);
        } else
        {
            //Turn off highlight and text
            displayText.SetActive(false);
        }
    }

}
