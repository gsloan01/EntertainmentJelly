using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    EnemyMovement enemyMovement;
    Animator animator;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    public bool isPerformingAction;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    //The higher and lower angles set, the greater or lower FOV
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;

    public float currentRecoveryTime = 0;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleRecoveryTime();
    }

    private void FixedUpdate()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if (enemyMovement.currentTarget != null)
        {
            enemyMovement.distanceFromTarget = Vector3.Distance(enemyMovement.currentTarget.transform.position, transform.position);
        }

        if (enemyMovement.currentTarget == null)
        {
            enemyMovement.HandleDetection();
        }
        else if(enemyMovement.distanceFromTarget > enemyMovement.stoppingDistance)
        {
            enemyMovement.HandleMoveToTarget();
        }
        else if(enemyMovement.distanceFromTarget <= enemyMovement.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void HandleRecoveryTime()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
                animator.SetBool("isAttacking", false);
            }
        }
    }

    #region Attack
    private void AttackTarget()
    {
        if (isPerformingAction) return;

        if (currentAttack == null)
        {
            GetNewAttack();
            Debug.Log(currentAttack);
        }
        else
        {
            isPerformingAction = true;
            animator.SetBool("isAttacking", true);
            currentRecoveryTime = currentAttack.recoveryTime;
            //currentAttack = null;
        }
    }

    private void GetNewAttack()
    {
        Vector3 targetsDirection = enemyMovement.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);

        enemyMovement.distanceFromTarget = Vector3.Distance(enemyMovement.currentTarget.transform.position, transform.position);

        int maxScore = 0;
        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyMovement.distanceFromTarget <= enemyAttackAction.maximumDistanceNeeded 
                && enemyMovement.distanceFromTarget >= enemyAttackAction.minimumDistanceNeeded)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        Debug.Log(viewableAngle);

        int randomVal = Random.Range(0, maxScore);
        int tempScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyMovement.distanceFromTarget <= enemyAttackAction.maximumDistanceNeeded
                && enemyMovement.distanceFromTarget >= enemyAttackAction.minimumDistanceNeeded)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (currentAttack != null) return;

                    tempScore += enemyAttackAction.attackScore;

                    currentAttack = enemyAttackAction;

                    if (tempScore > randomVal)
                    {
                        currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //replace red with whatever color you prefer
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
