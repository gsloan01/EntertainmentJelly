using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public SpawnEnemies[] spawns;
    public GameObject projectile;
    public GameObject rotationPoint;
    public GameObject DodgeAttackObject;

    public GameObject dodgeHandEffect;
    public GameObject AOEHandEffect;
    public GameObject coolDownHandEffect;
    Animator animator;
    List<SpawnEnemies> usedSpawns = new List<SpawnEnemies>();
    public GameObject attackSpawnPoint;

    public Healthbar healthbar;

    bool finishedSpawning;

    int randomSpawn = 0;
    int enemiesSpawned = 0;

    float moveTimer = 0;

    float timer = 5f;
    float phaseTimer = 20f;

    public float enemySpawnRate = 3f;
    public float aoeAttackRate = 4f;

    public Health health;

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

    private void Start()
    {

    }

    public void Update()
    {
        healthbar.SetHealth(health.GetHealth(), health.maxHealth);

        timer -= Time.deltaTime;

        if (health.hasDied && bStates != BossStates.Death)
        {
            //bStates = BossStates.Death;
            Destroy(gameObject, 3);
        }

        switch (bStates)
        {
            case BossStates.Cooldown:
                //Debug.Log("I am in the cooldown phase");

                coolDownHandEffect.SetActive(true);

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
                    int randomState = Random.Range(0, 2);
                    if (randomState == 0)
                    {
                        bStates = BossStates.Offense;
                        coolDownHandEffect.SetActive(false);
                    }
                    else
                    {
                        bStates = BossStates.Dodge;
                        coolDownHandEffect.SetActive(false);
                    }

                    phaseTimer = 20;
                    timer = aoeAttackRate;
                }
                break;
            case BossStates.Offense:

                AOEHandEffect.SetActive(true);

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

                        AOEHandEffect.SetActive(false);
                    }
                    else
                    {
                        bStates = BossStates.Dodge;

                        AOEHandEffect.SetActive(false);
                    }

                    phaseTimer = 20;
                    timer = aoeAttackRate;
                }

                break;
            case BossStates.Dodge:
                dodgeHandEffect.SetActive(true);

                phaseTimer -= Time.deltaTime;

                transform.RotateAround(rotationPoint.transform.position, Vector3.up, 10 * Time.deltaTime);

                if (timer <= 0)
                {
                    DodgeAttack();
                    timer = aoeAttackRate;
                }

                if (phaseTimer <= 0)
                {
                    int randomState3 = Random.Range(0, 2);
                    if (randomState3 == 0)
                    {
                        bStates = BossStates.Cooldown;
                        finishedSpawning = false;
                        timer = enemySpawnRate;
                        usedSpawns.Clear();

                        dodgeHandEffect.SetActive(false);
                    }
                    else
                    {
                        bStates = BossStates.Offense;
                        dodgeHandEffect.SetActive(false);
                    }

                    phaseTimer = 20;
                    timer = aoeAttackRate;
                }
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

    public void DodgeAttack()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Idle", false);
        Vector3 direction = FindObjectOfType<FPSPlayer>().transform.position - attackSpawnPoint.transform.position;
        GameObject temp = Instantiate(DodgeAttackObject, attackSpawnPoint.transform.position, attackSpawnPoint.transform.rotation);
        temp.GetComponent<Rigidbody>()?.AddForce(direction * 50, ForceMode.Force);
    }
}
