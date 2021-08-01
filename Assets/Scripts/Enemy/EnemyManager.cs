using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class EnemyManager : MonoBehaviour
{
    EnemyMovement enemyMovement;
    Animator animator;


    public float minBreathRate = 10f;
    public float maxBreathRate = 14f;
    private float currentRate = 0;
    private float breathTimer = 0;
    public List<AudioClip> breathNoises = new List<AudioClip>();
    public List<AudioClip> attackNoises = new List<AudioClip>();
    public AudioSource audio;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;



    public bool isPerformingAction;

    [Header("A.I Settings")]
    public float detectionRadius = 20;
    //The higher and lower angles set, the greater or lower FOV
    public float minimumDetectionAngle = -50;
    public float maximumDetectionAngle = 50;

    public float currentRecoveryTime = 0;

    public float damage = 10;

    

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();

        currentRate = Random.Range(minBreathRate, maxBreathRate);
    }

    private void Update()
    {
        Audio();
        HandleRecoveryTime();
        HandleCurrentAction();
    }


    private void Audio()
    {
        breathTimer += Time.deltaTime;

        if (breathTimer > currentRate)
        {
            breathTimer = 0;
            currentRate = Random.Range(minBreathRate, maxBreathRate);

            if (!audio.isPlaying)
            {
                audio.clip = breathNoises[Random.Range(0, breathNoises.Count)];
                audio.volume = 0.4f;
                audio.Play();
            }
        }
    }

    private void PlayAttackAudio()
    {
        audio.clip = attackNoises[Random.Range(0, attackNoises.Count)];
        audio.volume = 0.7f;
        audio.Play();
    }


    private void FixedUpdate()
    {
    }

    public void TurnOff()
    {
        this.enabled = false;
    }
    private void HandleCurrentAction()
    {
        if (enemyMovement.currentTarget == null)
        {
            enemyMovement.HandleDetection();
        } else
        {
            enemyMovement.HandleMoveToTarget();
        }
        if(enemyMovement.distanceFromTarget <= enemyMovement.stoppingDistance)
        {
            AttackTarget();
            PlayAttackAudio();
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
            animator.SetTrigger("isAttacking");
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
