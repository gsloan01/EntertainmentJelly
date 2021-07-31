using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth = 1;

    public Health parentHealth;
    public bool parentAddHealth;

    private bool hasDied = false;
    

    public UnityEvent deathEvent;

    void Start()
    {
        currentHealth = maxHealth;
    }

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
        parentHealth?.SubtractHealth(minus);
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
        hasDied = false;
    }

    public void Kill()
    {
        currentHealth = 0;
    }

    private void Update()
    {
        if (currentHealth <= 0 && !hasDied)
        {
            deathEvent?.Invoke();
            hasDied = true;
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Player Killed");
    }
}
