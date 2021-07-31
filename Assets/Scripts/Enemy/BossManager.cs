using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public SpawnEnemies[] spawns;
    public GameObject projectile;
    Animator animator;

    bool attackPhase;
    bool finishedSpawning;

    int randomSpawn = 0;
    int lastSpawn = 0;
    int enemiesAlive = 0;
    int ragdollCount = 0;

    float timer = 5f;
    float phaseTimer = 20f;

    public float enemySpawnRate = 3f;
    public float aoeAttackRate = 4f;

    public enum BossStates
    {
        Cooldown,
        Offense,
        Dodge,
        Death
    }

    public BossStates bStates;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        switch (bStates)
        {
            case BossStates.Cooldown:
                if (timer <= 0 && enemiesAlive < 4)
                {
                    SpawnEnemy();
                    timer = enemySpawnRate;
                }

                if (enemiesAlive == 4)
                {
                    finishedSpawning = true;
                }

                if (enemiesAlive == 0 && finishedSpawning)
                {
                    int randomState = Random.Range(0, 2);
                    if (randomState == 0)
                    {
                        bStates = BossStates.Offense;
                    }
                    else
                    {
                        //bStates = BossStates.Dodge;
                    }
                }
                break;
            case BossStates.Offense:
                phaseTimer -= Time.deltaTime;
                Debug.Log("Offense Phase: " + phaseTimer);
                if (timer <= 0)
                {
                    AOEAttack();
                    timer = aoeAttackRate;
                }

                if (phaseTimer <= 0)
                {
                    int randomState = Random.Range(0, 2);
                    if (randomState == 0)
                    {
                        bStates = BossStates.Cooldown;
                    }
                    else
                    {
                        //bStates = BossStates.Dodge;
                    }
                }
                break;
            case BossStates.Dodge:
                break;
            case BossStates.Death:
                break;
            default:
                break;
        }   
    }

    public void SpawnEnemy()
    {
        do
        {
            randomSpawn = Random.Range(0, spawns.Length);
        } while (lastSpawn == randomSpawn);

        spawns[randomSpawn].SpawnEnemy();
        enemiesAlive++;

        lastSpawn = randomSpawn;
    }

    public void AOEAttack()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Idle", false);
        Instantiate(projectile, FindObjectOfType<FPSPlayer>().transform.position, FindObjectOfType<FPSPlayer>().transform.rotation);
    }
}
