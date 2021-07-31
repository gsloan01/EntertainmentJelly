using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public UnityEvent deathEvent;

    public void AddHealth(float add)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + add);
    }

    public void SetHealth(float value)
    {
        currentHealth = Mathf.Min(maxHealth, value);
    }

    public void SubtractHealth(float minus)
    {
        currentHealth -= minus;
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
    }

    public void Kill()
    {
        currentHealth = 0;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            deathEvent?.Invoke();
        }
    }
}
