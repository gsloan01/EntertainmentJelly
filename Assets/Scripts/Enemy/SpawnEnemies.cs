using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] spawnableEnemies;

    public void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, spawnableEnemies.Length);
        Instantiate(spawnableEnemies[randomEnemy], transform.position, transform.rotation);
    }
}
