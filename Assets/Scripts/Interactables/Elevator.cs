using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class Elevator : MonoBehaviour
{
    public bool onFirstFloor = true;

    Animator animator;

    private bool active = true;
    public float activeTimer = 6.0f;
    private float currentTimer = 0.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer > activeTimer)
        {
            active = true;
        }
    }

    public void MoveElevator()
    {
        Debug.Log("FFloor: " + onFirstFloor + " | Active: " + active);
        if (!active) return;
        
        if (onFirstFloor)
        {
            MoveDown();
            onFirstFloor = false;
        } else
        {
            MoveUp();
            onFirstFloor = true;
        }

        currentTimer = 0;
        active = false;
    }

    public void MoveDown()
    {
        animator.Play("GoDown", 0, 0);
    }

    public void MoveUp()
    {
        animator.Play("GoUp", 0, 0);
    }

    //Make Player child
    private void OnTriggerEnter(Collider other)
    {
        FPSPlayer player = other.GetComponent<FPSPlayer>();

        if (player)
        {
            player.transform.SetParent(transform);
            //Debug.Log("Added player as child");
        }
    }

    //Make player not child
    private void OnTriggerExit(Collider other)
    {
        FPSPlayer player = other.GetComponent<FPSPlayer>();

        if (player)
        {
            player.transform.SetParent(null);
            if (player.GetComponent<DontDestroy>()) player.GetComponent<DontDestroy>().ResetDontDestroy();
            //Debug.Log("Removed player as child");
        }
    }
}
