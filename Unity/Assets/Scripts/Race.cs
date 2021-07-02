using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bruteforce/Races/New Race", fileName = "New Race")]
public class Race : ScriptableObject
{
    public int id;
    public string name;
    [Multiline]
    public string description;
    public float health;
    public float mana;
    public float manaspd;
    public float attack;
    public float magic;
    public float defense;
    public float attackspd;
    public float critchance;
    public float critdmg;

    public Skill[] skills;
}
