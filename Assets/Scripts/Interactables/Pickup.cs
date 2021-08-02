using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    public enum ePickupType
    {
        Health,
        Ammo,
        Battery,
        Gun,
        Flashlight
    }

    public ePickupType type = ePickupType.Ammo;
    public int value = 16;
    public bool selected = false;

    public AudioClip pickupSound;
    public GameObject displayText;
    public GameObject displayLight;

    public UnityEvent pickupEvent;

    public void Invoke()
    {
        pickupEvent?.Invoke();
    }


    private void Update()
    {
        if (selected)
        {
            //Turn on highlight and text
            displayText.SetActive(true);
            displayLight.SetActive(true);
        } else
        {
            //Turn off highlight and text
            displayText.SetActive(false);
            displayLight.SetActive(false);
        }
    }

}
