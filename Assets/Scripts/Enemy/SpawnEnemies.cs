using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] spawnableEnemies;

    public void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, spawnableEnemies.Length);
        GameObject enemy = Instantiate(spawnableEnemies[randomEnemy], transform.position, transform.rotation);
        enemy.GetComponentInChildren<EnemyMovement>().currentTarget = FPSPlayer.Instance;
    }
}
