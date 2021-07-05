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

}
