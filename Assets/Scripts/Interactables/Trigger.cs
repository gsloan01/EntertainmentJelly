using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    public bool active = false;

    public AudioClip pickupSound;
    public GameObject displayText;
    public GameObject displayLight;

    public UnityEvent interactEvent;

    private void Update()
    {
        if (active && Input.GetKeyDown(KeyCode.E))
        {
            interactEvent?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        active = true;
        displayText?.SetActive(true);
        displayLight?.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        active = false;
        displayText?.SetActive(false);
        displayLight?.SetActive(false);
    }

}
