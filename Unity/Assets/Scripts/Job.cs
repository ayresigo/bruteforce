using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bruteforce/Jobs/New Job", fileName = "New Job")]
public class Job : ScriptableObject
{
    public string id = System.Guid.NewGuid().ToString();
    public string name;
    [Multiline]
    public string description;
    [Space]
    public Material material;
    public Color color;
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


/* 
 *  Mage,
 *  Thief,
 *  Archer,
 *  Warrior,
 *  Archmage,
 *  Knight,
 *  Legionare,
 *  Paladin,
 *  Rogue,
 *  Royal Guard,
 *  Rune Knight
 *  Scorcher,
 *  Storm Knight,
 *  Wanderer,
 *  Warlock,
 *  Battle Master,
 *  Centurion,
 *  Conjuror,
 *  Sorcere,
 *  Warlord,
 *  Druid,
 *  Battlemage
 */