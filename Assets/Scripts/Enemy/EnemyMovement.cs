using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyManager enemyManager;

    LayerMask detectionLayer;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();   
    }

    public void HandleDetection()
    {
        //Create list of colliders
            //only searches certain objects that are tagged with the specific detection layer from the enemy pos
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            //FPSPlayer player = colliders[i].GetComponent<FPSPlayer>();
        }
    }
}
