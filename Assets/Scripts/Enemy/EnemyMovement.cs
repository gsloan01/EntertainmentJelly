using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    EnemyManager enemyManager;
    NavMeshAgent navMeshAgent;
    public FPSPlayer currentTarget;
    public LayerMask detectionLayer;
    Rigidbody rigidbody;
    Animator animator;

    public float speed = 2f;

    public float distanceFromTarget;
    public float stoppingDistance = 1f;
    public float detectionRange = 10f;

    public float rotationSpeed = 15f;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        navMeshAgent.enabled = false;
        rigidbody.isKinematic = false;
    }

    public void TurnOff()
    {
        this.enabled = false;
    }

    public void HandleDetection()
    {
        //Create list of colliders
            //only searches certain objects that are tagged with the specific detection layer from the enemy pos
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            FPSPlayer player = colliders[i].GetComponent<FPSPlayer>();

            if (player != null)
            {
                //Get Distance from player and enemy
                Vector3 targetDirection = player.transform.position - transform.position;

                //Gets angle from front of player and targetDirection
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                //Checks if ray can hit (not obstructed)
                if (Physics.Raycast(transform.position, targetDirection, out RaycastHit hit))
                {
                    float hitDistance = (hit.point - transform.position).magnitude;

                    if (hitDistance < targetDirection.magnitude) return;
                }

                //checks if angle is in range of min and max detection range
                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    currentTarget = player;
                }
                else
                {
                    currentTarget = null;
                }
            }
        }
    }

    public void HandleMoveToTarget()
    {
        //if (enemyManager.isPerformingAction) return;

        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        targetDirection.y = 0;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);

        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        //if we are performing action (attack, etc.) then stop movement
        if (enemyManager.isPerformingAction)
        {
            navMeshAgent.enabled = false;
        }
        else
        {
            //Debug.Log(distanceFromTarget + ": " + stoppingDistance);
            if (distanceFromTarget > stoppingDistance)
            {
                animator.SetFloat("Speed", speed);
                navMeshAgent.enabled = true;
            } else
            {
                animator.SetFloat("Speed", 0);
                navMeshAgent.enabled = false;
            }


            /*if (distanceFromTarget > detectionRange)
            {
                animator.SetFloat("Speed", 0);
                navMeshAgent.enabled = false;
            }
            else if (distanceFromTarget >= stoppingDistance)
            {
                animator.SetFloat("Speed", speed);
                navMeshAgent.enabled = true;
            }*/
        }

        HandleRotateTowardsTarget();

        //navMeshAgent.transform.localPosition = Vector3.zero;
        //navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleRotateTowardsTarget()
    {
        
        
        Vector3 direction = currentTarget.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        float angle = Vector3.Angle(transform.forward, direction);

        angle = angle * Mathf.Sign(direction.x - transform.forward.x) * -1;

        

        animator.SetFloat("Angle", angle);

        if (direction == Vector3.zero)
        {
            direction = transform.forward;
            
        }
        if (Mathf.Abs(angle) <= 70.0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
        } else
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime / 3);
        }

    }
}
