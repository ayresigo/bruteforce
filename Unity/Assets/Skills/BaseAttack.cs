using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : Skill
{    public override void Cast(GameObject caster, GameObject[] target)
    {
        float damageAmount;
        for (int i = 0; i < target.Length; i++)
        {
            damageAmount = calculateMeleeDamage(caster, target[i]);
            target[i].GetComponent<CharacterController>().takeDamage(damageAmount);
        }
    }
}
