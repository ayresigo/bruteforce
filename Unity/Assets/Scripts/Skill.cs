using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string id = System.Guid.NewGuid().ToString();
    public string name;
    [Multiline] public string description;
    public Type type;

    public enum Type
    {
        BasicAttack,
        Active,
        Passive
    }

    public abstract void Cast(Character caster, Character[] target);

}



[CreateAssetMenu(menuName = "Fireball", fileName = "Fireball")]
public class Fireball : Skill
{
    public override void Cast(Character caster, Character[] target)
    {
        Debug.Log(caster.name + " did " + (caster.attack * (100 / (100 + target[0].defense))) + " damage to " + target[0].name + ".");
        target[0].health -= caster.attack * (100 / (100 + target[0].defense));
    }
}