using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bruteforce/Skills/New Active Skill", fileName = "New Active Skill")]
public class ActiveSkill : Skill
{
    public float reqmana;
    public GameObject target;
    private void Awake()
    {
        type = Type.Active;
    }
}
