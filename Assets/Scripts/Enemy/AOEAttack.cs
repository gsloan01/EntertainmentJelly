using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttack : MonoBehaviour
{
    public float timer = 3;
    public GameObject attack;

    public Collider collider;

    public float dmg = 10;

    bool objectSpawned;

    void Start()
    {
        
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && !objectSpawned)
        {
            GameObject temp = Instantiate(attack, transform.position, transform.rotation);
            Destroy(temp, 1f);
            collider.enabled = true;
            objectSpawned = true;

            timer = 0.5f;
        }

        if (timer <= 0 && objectSpawned)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Health>().SubtractHealth(dmg);

            collider.enabled = false;
        }
    }
}
