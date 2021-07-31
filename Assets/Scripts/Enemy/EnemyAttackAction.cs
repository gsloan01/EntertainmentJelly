using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="A.I/Enemy Actions/ Attack Action")]
public class EnemyAttackAction : EnemyAction
{
    public int attackScore = 1;
    public float recoveryTime = 2;

    public float maximumAttackAngle = 50;
    public float minimumAttackAngle = -50;

    public float minimumDistanceNeeded = 1;
    public float maximumDistanceNeeded = 3;
}
