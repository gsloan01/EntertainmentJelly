using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Elevator : MonoBehaviour
{
    public bool onFirstFloor = true;

    public void MoveElevator()
    {

    }



    public void MoveUp()
    {

    }

    public void MoveDown()
    {

    }

    //Make Player child
    private void OnTriggerEnter(Collider other)
    {
        
    }

    //Make player not child
    private void OnTriggerExit(Collider other)
    {
        
    }
}
