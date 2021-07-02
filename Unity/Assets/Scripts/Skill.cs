using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bruteforce/Skills/New Skill", fileName = "New Skill")]
public class Skill : ScriptableObject
{
    public int id;
    public string name;
    public string description;
    public Type type;
    public enum Type
    {
        Active,
        Passive,
        Trigger
    }

}
