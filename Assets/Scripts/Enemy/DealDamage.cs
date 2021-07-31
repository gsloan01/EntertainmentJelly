using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float damage = 10;
    Collider collider;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        collider.enabled = false;
    }

    void Update()
    {
        
    }

    public void StartAttack()
    {
        collider.enabled = true;
    }

    public void EndAttack()
    {
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("Dealt Damage");
            collider.gameObject.GetComponentInParent<Health>()?.SubtractHealth(damage);
            collider.enabled = false;
        }
    }

}
