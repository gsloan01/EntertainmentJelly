using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public bool inAttackRange;

    public override State RunCurrentState()
    {
        if (inAttackRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }
}
