using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public GameObject leftHit;
    public GameObject rightHit;

    public void DamageLeft()
    {
        leftHit.GetComponent<DealDamage>()?.StartAttack();
    }

    public void DamageRight()
    {
        rightHit.GetComponent<DealDamage>()?.StartAttack();
    }

    public void DamageBoth()
    {
        leftHit.GetComponent<DealDamage>()?.StartAttack();
        rightHit.GetComponent<DealDamage>()?.StartAttack();
    }

    public void EndAttack()
    {
        leftHit.GetComponent<DealDamage>()?.EndAttack();
        rightHit.GetComponent<DealDamage>()?.EndAttack();
    }
}
