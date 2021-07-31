using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PickupManager : MonoBehaviour
{
    public Camera playerCamera;

    private Pickup currentPickup;
    public GameObject handgunObject;

    AudioSource audio;

    Health health;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        FindPickup();
        CheckPickup();
    }

    private void FindPickup()
    {
        if (currentPickup) currentPickup.selected = false;
        currentPickup = null;

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            currentPickup = hit.collider.GetComponent<Pickup>();

            if (currentPickup) currentPickup.selected = true;
        }

    }

    private void CheckPickup()
    {
        if (currentPickup && Input.GetKeyDown(KeyCode.E))
        {
            //Apply correct function
            switch (currentPickup.type)
            {
                case Pickup.ePickupType.Health:
                    HealthPickup();
                    break;
                case Pickup.ePickupType.Ammo:
                    AmmoPickup();
                    break;
                case Pickup.ePickupType.Battery:
                    BatteryPickup();
                    break;
                case Pickup.ePickupType.Gun:
                    GunPickup();
                    break;
                default:
                    break;
            }
        }
    }

    private void EndPickup()
    {
        audio.clip = currentPickup.pickupSound;
        audio.Play();
        Destroy(currentPickup.gameObject);
    }

    private void HealthPickup()
    {
        health.AddHealth(currentPickup.value);
        EndPickup();
    }

    private void AmmoPickup()
    {
        Handgun gun = handgunObject.GetComponent<Handgun>();

        if (gun)
        {
            gun.ammo += currentPickup.value;
            EndPickup();
        }
    }

    private void BatteryPickup()
    {

    }

    private void GunPickup()
    {
        //Enable arm & gun object
        handgunObject.SetActive(true);
        //Trigger its inspect weapon animation
        handgunObject.GetComponent<Animator>().Play("Inspect", 0, 0f);
    }
}
