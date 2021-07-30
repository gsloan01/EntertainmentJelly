using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    public bool detectedPlayer;

    public override State RunCurrentState()
    {
        if (detectedPlayer)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
