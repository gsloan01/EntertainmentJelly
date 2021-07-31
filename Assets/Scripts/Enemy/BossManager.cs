using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public SpawnEnemies[] spawns;
    public GameObject projectile;
    public GameObject rotationPoint;
    Animator animator;
    List<SpawnEnemies> usedSpawns = new List<SpawnEnemies>();

    bool finishedSpawning;

    int randomSpawn = 0;
    int enemiesSpawned = 0;

    float moveTimer = 0;

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
                Debug.Log("I am in the cooldown phase");
                
                
                if (timer <= 0 && enemiesSpawned < 4)
                {
                    SpawnEnemy();
                    timer = enemySpawnRate;
                }

                if (enemiesSpawned == 4)
                {
                    finishedSpawning = true;
                }

                EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
                bool allEnemiesDead = true;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (!enemies[i].gameObject.GetComponentInParent<Health>().hasDied)
                    {
                        allEnemiesDead = false;
                        break;
                    }
                }

                if (allEnemiesDead && finishedSpawning)
                {
                    enemiesSpawned = 0;
                    usedSpawns.Clear();
                    int randomState = Random.Range(1, 2);
                    if (randomState == 0)
                    {
                        bStates = BossStates.Offense;
                    }
                    else
                    {
                        bStates = BossStates.Dodge;
                    }
                }
                break;
            case BossStates.Offense:
                
                phaseTimer -= Time.deltaTime;
                Debug.Log("Offense Phase: " + (int)phaseTimer);
                if (timer <= 0)
                {
                    AOEAttack();
                    timer = aoeAttackRate;
                }
                
                if (phaseTimer <= 0)
                {
                    Debug.Log("PhaseTimer is 0");
                    int randomState2 = Random.Range(0, 2);
                    if (randomState2 == 0)
                    {
                        bStates = BossStates.Cooldown;
                        finishedSpawning = false;
                        timer = enemySpawnRate;
                        usedSpawns.Clear();
                    }
                    else
                    {
                        bStates = BossStates.Dodge;
                    }
                }
                break;
            case BossStates.Dodge:
                moveTimer += Time.deltaTime;
                transform.RotateAround(rotationPoint.transform.position, Vector3.up, 10 * Time.deltaTime);
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
        } while (usedSpawns.Contains(spawns[randomSpawn]));

        spawns[randomSpawn].SpawnEnemy();
        usedSpawns.Add(spawns[randomSpawn]);
        enemiesSpawned++;
    }

    public void AOEAttack()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Idle", false);
        Instantiate(projectile, FindObjectOfType<FPSPlayer>().transform.position, FindObjectOfType<FPSPlayer>().transform.rotation);
    }
}
