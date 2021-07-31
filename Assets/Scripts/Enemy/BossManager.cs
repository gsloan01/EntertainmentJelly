using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public SpawnEnemies[] spawns;
    public GameObject projectile;
    Animator animator;

    bool attackPhase;

    int randomSpawn = 0;
    int lastSpawn = 0;
    int enemiesAlive = 0;
    int ragdollCount = 0;

    float timer = 5f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && enemiesAlive < 2)
        {
            SpawnEnemy();
            timer = 5;
        }

        if (timer <= 0 && enemiesAlive >= 2)
        {
            attackPhase = true;
        }

        if (attackPhase)
        {
            ThrowEnemies();

            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);

            attackPhase = false;
            timer = 5;
        }
    }

    public void SpawnEnemy()
    {
        //check if cooldown phase
        do
        {
            randomSpawn = Random.Range(0, spawns.Length);
        } while (lastSpawn == randomSpawn);

        spawns[randomSpawn].SpawnEnemy();
        enemiesAlive++;

        lastSpawn = randomSpawn;
    }

    public void ThrowEnemies()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Idle", false);
        //check if attack phase
        Instantiate(projectile, FindObjectOfType<FPSPlayer>().transform.position, FindObjectOfType<FPSPlayer>().transform.rotation);
    }
}
