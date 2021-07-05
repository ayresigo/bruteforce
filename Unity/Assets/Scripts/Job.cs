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
    public int health = 250;
    public int mana = 100;
    public int manaspd = 20;
    public int attack = 10;
    public int magic = 10;
    public int defense = 10;
    public int attackspd = 10;
    public int critchance = 10;
    public int critdmg = 10;
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