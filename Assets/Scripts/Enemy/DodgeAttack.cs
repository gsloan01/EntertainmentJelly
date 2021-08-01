using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeAttack : MonoBehaviour
{
    public GameObject attack;

    public Collider collider;

    public float dmg = 5;
    public float lifetime = 10;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        attack.SetActive(true);
        collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        collider.enabled = false;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().SubtractHealth(dmg);
            Destroy(gameObject, 1);

            Debug.Log("Dodge Attack Hit");
        }
    }
}
