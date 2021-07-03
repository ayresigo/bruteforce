using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Indentity")]
    public string id;
    public string name;
    public string surname;

    [Header("Inheritment")]
    public Shape shape;
    public Job job;

    [Header("Attributes")]
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

    [Header("Skillset")]
    public Skill basicAttack;

    [Header("Renderer")]
    public Mesh shapeMesh;
    public Material shapeMaterial;
    public Color shapeColor;

}
