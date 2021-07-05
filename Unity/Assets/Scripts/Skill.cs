using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    /*Option #2: All-in-One Spells
     *
     *Instead of making one class that does damage, 
     *and another that stuns, and a third that pushes, 
     *make a single class that does everything. Every single 
     *spell in the whole game has a field for how much 
     *damage it does, and a separate field for how long it stuns,
     *and a separate field for how far it pushes the target. 
     *Most of those fields may be "zero" on most spells, 
     *but they're always there if you need them.
     *
     *Obviously, this could get unwieldy if you have a large number of specialized effects. 
     *But there are many computer games where this could be totally practical. 
     *And a few wacky special cases that are only used by one spell (or one family of spells) 
     *can still be handled by separate classes as long as they don't need to be combined with 
     *all the other effects in the game.     */

    public string id = System.Guid.NewGuid().ToString();
    public SkillName uniqueName;
    public string name;
    [Multiline] public string description;
    public float reqMana;
    public TargetType targetType;
    public bool critable;
    public SkillName nextSkill;

    public enum SkillName
    {
        None,
        BasicMeleeAttack,
        BasicRangedAttack
    }
    public enum EffectType
    {
        None,
        Stun
    }
    public enum TargetType
    {
        Self,
        Caster,
        RandomAlly,
        FirstAlly,
        SecondAlly,
        ThirdAlly,
        RandomAllies,
        FirstTwoAllies,
        LastTwoAllies,
        EdgeAllies,
        AllAllies,
        LowestHealthAlly,
        HighestHealthAlly,
        LowestAttackAlly,
        HighestAttackAlly,
        LowestDefenseAlly,
        HighestDefenseAlly,
        RandomEnemy,
        FirstEnemy,
        SecondEnemy,
        ThirdEnemy,
        RandomEnemies,
        FirstTwoEnemies,
        LastTwoEnemies,
        EdgeEnemies,
        AllEnemies,
        MirrorSideEnemy,
        LowestHealthEnemy,
        HighestHealthEnemy,       
        LowestAttackEnemy,
        HighestAttackEnemy,
        LowestDefenseEnemy,
        HighestDefenseEnemy
    }
    public abstract void Cast(GameObject caster, GameObject[] target);

    public float calculateMeleeDamage(GameObject caster, GameObject target)
    {
        return (caster.GetComponent<Character>().attack * (100 / (100 + target.GetComponent<Character>().defense)));
    }
}

/*
public string id = System.Guid.NewGuid().ToString();
public SkillName name;
[Multiline] public string description;
public Type type;
public float reqMana;
public TargetType targetType;
public bool critable;
public SkillName nextSkill;
 */

public class BasicMeleeAttack : Skill
{
    private void Awake()
    {
        uniqueName = SkillName.BasicMeleeAttack;
        description = "Ataque basico melee";
        reqMana = 0;
        targetType = TargetType.FirstEnemy;
        critable = true;
        nextSkill = Skill.SkillName.None;
    }
    public override void Cast(GameObject caster, GameObject[] target)
    {

    }
}

public class BasicRangedAttack : Skill
{
    private void Awake()
    {
        uniqueName = SkillName.BasicRangedAttack;
        description = "Ataque basico ranged";
        reqMana = 0;
        targetType = TargetType.FirstEnemy;
        critable = true;
        nextSkill = Skill.SkillName.None;
    }
    public override void Cast(GameObject caster, GameObject[] target)
    {

    }
}

/*
#region Skills
[CreateAssetMenu(menuName = "Bruteforce/Skills/BasicAttack", fileName = "BasicAttack")]
public class BasicAttack : Skill
{
    private void Awake()
    {
        name = "Basic Attack";
        description = "Basic attack that deals (caster attack * (100 / (100 + target defense))";
        type = Type.BasicAttack;
        targetType = TargetType.FirstEnemy;
        critable = true;
    }

    public override void Cast(GameObject caster, GameObject[] target)
    {
        float damageAmount;
        for (int i = 0; i < target.Length; i++)
        {
            damageAmount = calculateMeleeDamage(caster, target[i]);
            target[i].GetComponent<CharacterController>().takeDamage(damageAmount);
        }
    }
}
[CreateAssetMenu(menuName = "Bruteforce/Skills/Punch", fileName = "Punch")]
public class Punch : Skill
{
    private void Awake()
    {
        name = "Punch";
        description = "Throws a punch to the closest enemy dealing (attack) damage. If the casters (health) is under 50%, inflicts 2x damage.";
        type = Type.BasicAttack;
        targetType = TargetType.FirstEnemy;
        critable = true;
    }

    public override void Cast(GameObject caster, GameObject[] target)
    {

    }
}
#endregion
*/