using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bruteforce/Skills/New Passive Skill", fileName = "New Passive Skill")]
public class PassiveSkill : Skill
{
    public GameObject target;
    private void Awake()
    {
        type = Type.Passive;
    }
}
