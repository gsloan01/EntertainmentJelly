using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public SpawnEnemies[] spawns;
    public GameObject[] ragdolls;
    public GameObject spawnThrowable;
    Rigidbody rigidbody;
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

        if (timer <= 0 && enemiesAlive >= 2 && ragdollCount < 1)
        {
            attackPhase = true;
        }

        if (attackPhase)
        {
            ThrowEnemies();

            if (ragdollCount > 1)
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Attack", false);

                attackPhase = false;
            }
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
        int randomEnemy = Random.Range(0, ragdolls.Length);
        GameObject temp = Instantiate(ragdolls[randomEnemy], spawnThrowable.transform.position, spawnThrowable.transform.rotation);
        Vector3 direction = FindObjectOfType<FPSPlayer>().transform.position - spawnThrowable.transform.position;
        temp.GetComponent<Rigidbody>().AddForce(direction * 100000, ForceMode.Force);
        ragdollCount++;
    }
}
