using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bruteforce/Shapes/New Shape", fileName = "New Shape")]
public class Shape : ScriptableObject
{
    public string id = System.Guid.NewGuid().ToString();
    public string name;
    [Multiline] public string description;
    [Space]
    public Mesh shapeMesh;
    [Space]
    public float health = 250;
    public float mana = 100;
    public float manaspd = 20;
    public float attack = 10;
    public float magic = 10;
    public float defense = 10;
    public float attackspd = 10;
    public float critchance = 10;
    public float critdmg = 10;
    [Space]
    public Skill[] basicAttacks;
}
