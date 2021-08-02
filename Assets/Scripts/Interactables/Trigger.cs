using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    public bool active = false;
    private bool isEnabled = true;
    public float resetTimer = 6.0f;
    private float currentResetTimer = 0.0f;

    public AudioClip pickupSound;
    public GameObject displayText;

    public UnityEvent interactEvent;

    private void Update()
    {
        currentResetTimer += Time.deltaTime;

        if (currentResetTimer > resetTimer)
        {
            isEnabled = true;
        }

        
        if (active && Input.GetKeyDown(KeyCode.E) && isEnabled)
        {
            interactEvent?.Invoke();
            currentResetTimer = 0;
            isEnabled = false;
            Disable();
        }
    }

    private void Enable()
    {
        if (!isEnabled) return;
        active = true;
        displayText?.SetActive(true);
    }

    private void Disable()
    {
        active = false;
        displayText?.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Enable();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Disable();
    }

}
