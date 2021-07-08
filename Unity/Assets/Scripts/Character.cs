using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Indentity")]
    public int id;
    public int ownerId;
    public string name;
    public string surname;
    public string creationDate;
    public Rarity rarity;

    [Header("Inheritment")]
    public Shape.ShapeName shape;
    public Job.JobName job;

    [Header("Attributes")]
    public int health;
    public int attack;
    public int magic;
    public int defense;
    public int resistance;
    public int critchance;
    public int accuracy;
    public int evade;
    public int critdmg;
    public int pureness;

    [Header("Skillset")]
    public Skill.SkillName basicAttack;
    public Skill.SkillName activeSkill;

    [Header("Renderer")]
    public Mesh shapeMesh;
    public Material shapeMaterial;
    public Color shapeColor;

    public enum Rarity
    {
        common,
        uncommon,
        rare,
        epic,
        legendary,
        mythic
    }

    public void passData(Character target)
    {
        target.id = id;
        target.ownerId = ownerId;
        target.name = name;
        target.surname = surname;
        target.creationDate = creationDate;
        target.rarity = rarity;
        target.shape = shape;
        target.job = job;
        target.health = health;
        target.attack = attack;
        target.defense = defense;
        target.resistance = resistance;
        target.critchance = critchance;
        target.accuracy = accuracy;
        target.evade = evade;
        target.critdmg = critdmg;
        target.pureness = pureness;
        target.basicAttack = basicAttack;
        target.activeSkill = activeSkill;
        target.shapeMesh = shapeMesh;
        target.shapeMaterial = shapeMaterial;
        target.shapeColor = shapeColor;
    }
}
