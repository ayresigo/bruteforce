using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int id;
    public string name;
    public string surname;

    public Race race;
    public Job job;

    public float health;
    public float mana;
    public float manaspd;
    public float attack;
    public float magic;
    public float defense;
    public float attackspd;
    public float critchance;
    public float critdmg;
    public float pureness;

    public ActiveSkill activeSkill;
    public PassiveSkill passiveSkill;
}
